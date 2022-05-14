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
    public class QuestionsController : SuperController<Question>
    {
        public QuestionsController(MyContext context ) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{value}/{responses}/{choices}/{time}/{idQuiz}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string value, string responses, string choices, int time, int idQuiz)
        {
            var q = _context.Questions
                .Where(e => value == "*" ? true : e.Value.ToLower().Contains(value.ToLower()))
.Where(e => responses == "*" ? true : e.ResponsesString.ToLower().Contains(responses.ToLower()))
.Where(e => choices == "*" ? true : e.Choices.ToLower().Contains(choices.ToLower()))
.Where(e => time == 0 ? true : e.Time == time)
.Where(e => idQuiz == 0 ? true : e.IdQuiz == idQuiz)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Question>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
value = e.Value,
responses = e.ResponsesString,
choices = e.Choices,
isMultiChoises = e.IsMultiChoises,
time = e.Time,
quiz = e.Quiz.Title,
idQuiz = e.IdQuiz,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}