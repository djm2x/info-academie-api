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
    public class StudentsController : SuperController<Student>
    {
        public StudentsController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{ecole}/{niveau}/{branche}/{nomParent}/{prenomParent}/{tel1Parent}/{tel2Parent}/{idUser}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string ecole, int niveau, int branche, string nomParent, string prenomParent, string tel1Parent, string tel2Parent, int idUser)
        {
            var q = _context.Students
                .Where(e => ecole == "*" ? true : e.Ecole.ToLower().Contains(ecole.ToLower()))
                .Where(e => niveau == 0 ? true : e.Niveau == niveau)
                .Where(e => branche == 0 ? true : e.Branche == branche)
                .Where(e => nomParent == "*" ? true : e.NomParent.ToLower().Contains(nomParent.ToLower()))
                .Where(e => prenomParent == "*" ? true : e.PrenomParent.ToLower().Contains(prenomParent.ToLower()))
                .Where(e => tel1Parent == "*" ? true : e.Tel1Parent.ToLower().Contains(tel1Parent.ToLower()))
                .Where(e => tel2Parent == "*" ? true : e.Tel2Parent.ToLower().Contains(tel2Parent.ToLower()))
                .Where(e => idUser == 0 ? true : e.IdUser == idUser)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Student>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    ecole = e.Ecole,
                    niveau = e.Niveau,
                    branche = e.Branche,
                    nomParent = e.NomParent,
                    prenomParent = e.PrenomParent,
                    tel1Parent = e.Tel1Parent,
                    tel2Parent = e.Tel2Parent,
                    user = e.User.Nom,
                    idUser = e.IdUser,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var model = await _context.Students.Where(e => e.IdUser == id).FirstOrDefaultAsync();

            return Ok(model);
        }
    }
}