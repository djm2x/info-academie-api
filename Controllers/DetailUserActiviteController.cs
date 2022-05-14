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
    public class DetailUserActivitesController : SuperController<DetailUserActivite>
    {
        public DetailUserActivitesController(MyContext context ) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{idUser}/{idActivite}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, int idUser, int idActivite)
        {
            var q = _context.DetailUserActivites
                .Where(e => idUser == 0 ? true : e.IdUser == idUser)
.Where(e => idActivite == 0 ? true : e.IdActivite == idActivite)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<DetailUserActivite>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
date = e.Date,
user = e.User.Nom,
idUser = e.IdUser,
activite = e.Activite.Nom,
idActivite = e.IdActivite,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}