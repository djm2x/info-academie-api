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
    public class ContactUsController : SuperController<ContactUs>
    {
        public ContactUsController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{obj}/{msg}/{idUser}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string obj, string msg, int idUser)
        {
            var q = _context.ContactUss
                .Where(e => obj == "*" ? true : e.Object.ToLower().Contains(obj.ToLower()))
                .Where(e => msg == "*" ? true : e.Msg.ToLower().Contains(msg.ToLower()))
                .Where(e => idUser == 0 ? true : e.IdUser == idUser)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<ContactUs>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    Object = e.Object,
                    msg = e.Msg,
                    date = e.Date,
                    user = e.User.Nom,
                    idUser = e.IdUser,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}