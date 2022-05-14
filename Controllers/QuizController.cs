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
    public class QuizzesController : SuperController<Quiz>
    {
        public QuizzesController(MyContext context ) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{title}/{idContext}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string title, int idContext)
        {
            var q = _context.Quizs
                .Where(e => title == "*" ? true : e.Title.ToLower().Contains(title.ToLower()))
                .Where(e => idContext == 0 ? true : e.IdContext == idContext)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Quiz>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
                {
                id = e.Id,
                title = e.Title,
                description = e.Description,
                enableTime = e.EnableTime,
                date = e.Date,
                isActive = e.IsActive,
                context = e.Context.Nom,
                idContext = e.IdContext,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}