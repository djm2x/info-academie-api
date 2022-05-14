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
    public class BranchesController : SuperController<Branche>
    {
        public BranchesController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{nom}/{nomAr}/{idNiveauScolaire}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string nom, string nomAr, int idNiveauScolaire)
        {
            var q = _context.Branches
                .Where(e => nom == "*" ? true : e.Nom.ToLower().Contains(nom.ToLower()))
                .Where(e => nomAr == "*" ? true : e.NomAr.ToLower().Contains(nomAr.ToLower()))
                .Where(e => idNiveauScolaire == 0 ? true : e.IdNiveauScolaire == idNiveauScolaire)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Branche>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    nomAr = e.NomAr,
                    niveauScolaire = e.NiveauScolaire.Nom,
                    idNiveauScolaire = e.IdNiveauScolaire,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{propertyName}/{id}")]
        public async Task<IActionResult> GetByForeignkey(string propertyName, int id)
        {
            var list = await _context.Branches.Where(e => e.IdNiveauScolaire == id).ToListAsync();

            return Ok(list);
        }
    }
}