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
    public class ResponsesController : SuperController<Response>
    {
        public ResponsesController(MyContext context ) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{trueResponse}/{userResponse}/{note}/{idQuestion}/{idUser}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string trueResponse, string userResponse, int note, int idQuestion, int idUser)
        {
            var q = _context.Responses
                .Where(e => trueResponse == "*" ? true : e.TrueResponse.ToLower().Contains(trueResponse.ToLower()))
.Where(e => userResponse == "*" ? true : e.UserResponse.ToLower().Contains(userResponse.ToLower()))
.Where(e => note == 0 ? true : e.Note == note)
.Where(e => idQuestion == 0 ? true : e.IdQuestion == idQuestion)
.Where(e => idUser == 0 ? true : e.IdUser == idUser)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Response>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
trueResponse = e.TrueResponse,
userResponse = e.UserResponse,
date = e.Date,
note = e.Note,
question = e.Question.Value,
idQuestion = e.IdQuestion,
user = e.User.Nom,
idUser = e.IdUser,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}