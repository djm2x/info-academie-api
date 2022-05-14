using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Api.Providers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : SuperController<User>
    {
        public UsersController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{nom}/{prenom}/{email}/{tel1}/{adresse}/{cin}/{role}/{idVille}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir
        , string nom, string prenom, string email, string tel1, string adresse, string cin, string role, int idVille)
        {
            var q = _context.Users
                .Where(e => nom == "*" ? true : e.Nom.ToLower().Contains(nom.ToLower()))
                .Where(e => prenom == "*" ? true : e.Prenom.ToLower().Contains(prenom.ToLower()))
                .Where(e => tel1 == "*" ? true : e.Tel1.ToLower().Contains(tel1.ToLower()))
                // .Where(e => tel2 == "*" ? true : e.Tel2.ToLower().Contains(tel2.ToLower()))
                .Where(e => email == "*" ? true : e.Email.ToLower().Contains(email.ToLower()))
                .Where(e => adresse == "*" ? true : e.Adresse.ToLower().Contains(adresse.ToLower()))
                .Where(e => cin == "*" ? true : e.Cin.ToLower().Contains(cin.ToLower()))
                .Where(e => role == "*" ? true : e.Role.ToLower().Contains(role.ToLower()))
                .Where(e => idVille == 0 ? true : e.IdVille == idVille)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<User>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    prenom = e.Prenom,
                    tel1 = e.Tel1,
                    tel2 = e.Tel2,
                    email = e.Email,
                    password = e.Password,
                    isActive = e.IsActive,
                    date = e.Date,
                    adresse = e.Adresse,
                    imageUrl = e.ImageUrl,
                    cin = e.Cin,
                    role = e.Role,
                    ville = e.Ville.Nom,
                    idVille = e.IdVille,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Put([FromRoute] int id, [FromBody] User model)
        {
            _context.Entry(model).State = EntityState.Modified;

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            model.Password = user.Password;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }
    }
}