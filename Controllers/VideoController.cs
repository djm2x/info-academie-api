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
    public class VideosController : SuperController<Video>
    {
        public VideosController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{title}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string title)
        {
            var q = _context.Videos
                .Where(e => title == "*" ? true : e.Title.ToLower().Contains(title.ToLower()))
                // .Where(e => order == 0 ? true : e.Order == order)
                // .Where(e => urlVideo == "*" ? true : e.UrlVideo.ToLower().Contains(urlVideo.ToLower()))

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Video>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    title = e.Title,
                    order = e.Order,
                    description = e.Description,
                    date = e.Date,
                    urlVideo = e.UrlVideo,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}