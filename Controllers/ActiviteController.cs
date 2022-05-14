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
    public class ActivitesController : SuperController<Activite>
    {
        public ActivitesController(MyContext context ) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{nom}/{nomAr}/{idTypeActivite}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string nom, string nomAr, int idTypeActivite)
        {
            var q = _context.Activites
                .Where(e => nom == "*" ? true : e.Nom.ToLower().Contains(nom.ToLower()))
.Where(e => nomAr == "*" ? true : e.NomAr.ToLower().Contains(nomAr.ToLower()))
.Where(e => idTypeActivite == 0 ? true : e.IdTypeActivite == idTypeActivite)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Activite>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
nom = e.Nom,
nomAr = e.NomAr,
imageUrl = e.ImageUrl,
typeActivite = e.TypeActivite.Nom,
idTypeActivite = e.IdTypeActivite,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}