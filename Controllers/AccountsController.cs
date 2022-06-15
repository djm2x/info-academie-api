using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using Models;
using Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using Providers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Api.Providers;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : SuperController<User>
    {
        private readonly TokkenHandler _tokkenHandler;
        private readonly EmailService _emailService;
        private readonly Crypto _crypto;
        private readonly ILogger _logger;
        public AccountsController(MyContext context
        , EmailService emailService
        , AccountService accountService
        , TokkenHandler tokkenHandler
        , HtmlService htmlService
        , Crypto crypto
        , ILogger<AccountsController> logger
        )
        : base(context)
        {
            _emailService = emailService;
            _tokkenHandler = tokkenHandler;
            _crypto = crypto;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            var emailExiste = await _context.Users.FirstOrDefaultAsync(e => e.Email == model.Email);

            if (emailExiste != null)
            {
                return Ok(new { code = -1, message = "Email already taking" });
            }

            model.Password = _crypto.HashPassword(model.Password);

            await _context.Users.AddAsync(model);

            try
            {
                await _context.SaveChangesAsync();

                if (model.Role.ToLower().Equals("student"))
                {
                    await _context.Students.AddAsync(new Student { IdUser = model.Id });
                }
                else if (model.Role.ToLower().Equals("prof"))
                {
                    await _context.Profs.AddAsync(new Prof { IdUser = model.Id });
                }

                await _context.SaveChangesAsync();

                var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.Id.ToString()),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Role, model.Role),
                    };

                model.Password = "";
                var token = _tokkenHandler.GenerateTokken(claims);

                try
                {
                    _emailService.ValidateAccount(new EmailOption { CustomerName = model.Nom, CustomerEmail = model.Email });
                }
                catch (System.Exception e)
                {
                    _logger.LogError(e.Message);
                    //  return Ok(new { code = -1, message = e.Message });
                }


                return Ok(new { code = 1, message = "Register Successful" });

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Ok(new { code = -2, message = ex.Message });
            }

        }


        [AllowAnonymous]
        [HttpPost("{email}")]
        public async Task<ActionResult> ForgotPassword([FromRoute] string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains('@') || !email.Contains('.'))
            {
                return Ok(new { message = "Format email not satisfied", code = -4 });
            }

            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == email);

            if (user == null)
            {
                return Ok(new { message = "Email Incorrect", code = -1 });
            }

            try
            {
                await _emailService.ForgotPassword(new EmailOption {CustomerEmail = email});
                return Ok(new { code = 1, message = "Success" });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message, code = -5 });
            }
        }

        [AllowAnonymous]
        [HttpPost("{token}")]
        public async Task<ActionResult> ChangePassword([FromRoute] string token, [FromBody] UserDTO model)
        {
            if (token == null || string.IsNullOrEmpty(token))
            {
                return Ok(new { message = "Format token not satisfied", code = -4 });
            }

            String email = "";
            DateTime? creation = null;

            try
            {
                email = HttpContext.GetClaimfromToken("email", token);
                creation = DateTime.Parse(HttpContext.GetClaimfromToken("creation", token));
            }
            catch (System.Exception)
            {
                return Ok(new { message = "Token not valid", code = -1 });
            }

            if (DateTime.Compare(DateTime.Now, creation.Value.AddDays(1)) >= 0)
            {
                return Ok(new { message = "Token expired", code = -1 });
            }

            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == email);
            if (user == null)
            {
                return Ok(new { message = "Email Incorrect", code = -1 });
            }

            user.Password = _crypto.HashPassword(model.Password);
            user.IsActive = true;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { code = 1, message = "password changed" });
            }
            catch (Exception ex)
            {
                return Ok(new { code = -1, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(UserDTO model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return Ok(new { message = "Email | password required", code = -4 });
            }

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return Ok(new { message = "Error Email", code = -3 });
            }

            if (user.Password != model.Password)
            {
                return Ok(new { message = "Error Password", code = -1 });
            }

            if (user.IsActive == false)
            {
                return Ok(new { message = "Not Activated yet", code = -1 });
            }


            // remove password before returning
            user.Password = "";
            //  await _context.Entry(model).Reference(e => e.Role).LoadAsync();

            object child = null;
            NiveauScolaire niveau = null;
            Branche branche = null;

            if (user.Role == "student")
            {
                child = await _context.Students.Where(e => e.IdUser == user.Id).FirstOrDefaultAsync() as Student;

                if (child != null)
                {

                    niveau = await _context.NiveauScolaires.Where(e => e.Id == ((Student)child).Niveau).FirstOrDefaultAsync();

                    if (((Student)child).Branche != 0)
                    {
                        branche = await _context.Branches.Where(e => e.Id == ((Student)child).Branche).FirstOrDefaultAsync();
                    }
                }
            }
            else
            {
                child = await _context.Profs.Where(e => e.IdUser == user.Id).FirstOrDefaultAsync() as Prof;
            }

            var claims = new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                };

            var token = _tokkenHandler.GenerateTokken(claims);

            return Ok(new { code = 1, child, niveau, branche, user, token, message = "Successful" });

        }
    }

    public class TokenDTO
    {
        public int? IdUser { get; set; }
        public int? IdPlace { get; set; }
        public int? IdRulePlace { get; set; }
        public string Email { get; set; }
        public string UserProfil { get; set; }
    }

    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}