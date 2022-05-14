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
    public class TypeActivitesController : SuperController<TypeActivite>
    {
        public TypeActivitesController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{nom}/{nomAr}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string nom, string nomAr)
        {
            var q = _context.TypeActivites
                .Where(e => nom == "*" ? true : e.Nom.ToLower().Contains(nom.ToLower()))
.Where(e => nomAr == "*" ? true : e.NomAr.ToLower().Contains(nomAr.ToLower()))

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<TypeActivite>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    nomAr = e.NomAr,
                    imageUrl = e.ImageUrl,
                    active = e.Active,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithActivites()
        {
            var list = await _context.TypeActivites//.OrderBy(e => e.Id)
            .Include(e => e.Activites)
            .ToListAsync()
            ;

            return Ok(list);
        }
    }
}