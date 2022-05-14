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
    public class DiscussionsController : SuperController<Discussion>
    {
        public DiscussionsController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{unReaded}/{idMe}/{idOtherUser}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, int unReaded, int idMe, int idOtherUser)
        {
            var q = _context.Discussions
                .Where(e => unReaded == 0 ? true : e.UnReaded == unReaded)
                .Where(e => idMe == 0 ? true : e.IdMe == idMe)
                .Where(e => idOtherUser == 0 ? true : e.IdOtherUser == idOtherUser)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Discussion>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    unReaded = e.UnReaded,
                    date = e.Date,
                    me = e.Me.Nom,
                    idMe = e.IdMe,
                    otherUser = e.OtherUser.Nom,
                    idOtherUser = e.IdOtherUser,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacts(int id)
        {
            var list = await _context.Discussions.Where(e => e.IdMe == id)
            .Include(e => e.Me)
            .Include(e => e.OtherUser)
            .ToListAsync()
            ;

            return Ok(list);
        }
    }
}