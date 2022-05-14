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
    public class NiveauScolairesController : SuperController<NiveauScolaire>
    {
        public NiveauScolairesController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{nom}/{nomAr}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string nom, string nomAr)
        {
            var q = _context.NiveauScolaires
                .Where(e => nom == "*" ? true : e.Nom.ToLower().Contains(nom.ToLower()))
                .Where(e => nomAr == "*" ? true : e.NomAr.ToLower().Contains(nomAr.ToLower()))
                // .Where(e => idCycle == 0 ? true : e.IdCycle == idCycle)
                // .Where(e => coursLigneGroupe == 0 ? true : e.CoursLigneGroupe == coursLigneGroupe)
                // .Where(e => coursLigneIndividuel == 0 ? true : e.CoursLigneIndividuel == coursLigneIndividuel)
                // .Where(e => coursDomicileGroupe == 0 ? true : e.CoursDomicileGroupe == coursDomicileGroupe)
                // .Where(e => coursDomicileIndividuel == 0 ? true : e.CoursDomicileIndividuel == coursDomicileIndividuel)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<NiveauScolaire>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    nomAr = e.NomAr,
                    idCycle = e.IdCycle,
                    coursLigneGroupe = e.CoursLigneGroupe,
                    coursLigneIndividuel = e.CoursLigneIndividuel,
                    coursDomicileGroupe = e.CoursDomicileGroupe,
                    coursDomicileIndividuel = e.CoursDomicileIndividuel,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }
    }
}