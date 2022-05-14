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
    public class MessagesController : SuperController<Message>
    {
        public MessagesController(MyContext context ) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{object}/{message}/{idCours}/{otherUserName}/{otherUserImage}/{idMe}/{idOtherUser}/{idDiscussion}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string _object, string message, int idCours, string otherUserName, string otherUserImage, int idMe, int idOtherUser, int idDiscussion)
        {
            var q = _context.Messages
                .Where(e => _object == "*" ? true : e.Object.ToLower().Contains(_object.ToLower()))
.Where(e => message == "*" ? true : e.Content.ToLower().Contains(message.ToLower()))
.Where(e => idCours == 0 ? true : e.IdCours == idCours)
.Where(e => otherUserName == "*" ? true : e.OtherUserName.ToLower().Contains(otherUserName.ToLower()))
.Where(e => otherUserImage == "*" ? true : e.OtherUserImage.ToLower().Contains(otherUserImage.ToLower()))
.Where(e => idMe == 0 ? true : e.IdMe == idMe)
.Where(e => idOtherUser == 0 ? true : e.IdOtherUser == idOtherUser)
.Where(e => idDiscussion == 0 ? true : e.IdDiscussion == idDiscussion)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Message>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                
                .Select(e => new 
{
id = e.Id,
_object = e.Object,
message = e.Content,
vu = e.Vu,
date = e.Date,
idCours = e.IdCours,
otherUserName = e.OtherUserName,
otherUserImage = e.OtherUserImage,
me = e.Me.Nom,
idMe = e.IdMe,
otherUser = e.OtherUser.Nom,
idOtherUser = e.IdOtherUser,
discussion = e.Discussion.UnReaded,
idDiscussion = e.IdDiscussion,

})
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}