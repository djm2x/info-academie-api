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
    public class OffreProfsController : SuperController<OffreProf>
    {
        public OffreProfsController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public override async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            var q = _context.OffreProfs
                // .Where(e => interval == "*" ? true : e.Interval.ToLower().Contains(interval.ToLower()))
                // .Where(e => value == 0 ? true : e.Value == value)
                // .Where(e => idTypeCours == 0 ? true : e.IdTypeCours == idTypeCours)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<OffreProf>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    interval = e.Interval,
                    value = e.Value,
                    typeCours = e.TypeCours.Nom,
                    idTypeCours = e.IdTypeCours,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll2()
        {
            var list = await _context.OffreProfs.OrderBy(e => e.Id)
            .Include(e => e.TypeCours)
            .ToListAsync()
            ;

            return Ok(list);
        }
    }
}