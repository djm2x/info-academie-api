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
    public class MatiersController : SuperController<Matier>
    {
        public MatiersController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{name}/{nameAr}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string name, string nameAr)
        {
            var q = _context.Matiers
                .Where(e => name == "*" ? true : e.Name.ToLower().Contains(name.ToLower()))
                .Where(e => nameAr == "*" ? true : e.NameAr.ToLower().Contains(nameAr.ToLower()))
                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Matier>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .Select(e => new
                {
                    id = e.Id,
                    name = e.Name,
                    nameAr = e.NameAr,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}