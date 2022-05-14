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
    public class CoursController : SuperController<Cours>
    {
        public CoursController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{nom}/{nomAr}/{idNiveauScolaire}/{idBranche}/{idMatier}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string nom, string nomAr, int idNiveauScolaire, int idBranche, int idMatier)
        {
            var q = _context.Courses
                .Where(e => nom == "*" ? true : e.Nom.ToLower().Contains(nom.ToLower()))
                .Where(e => nomAr == "*" ? true : e.NomAr.ToLower().Contains(nomAr.ToLower()))
                // .Where(e => filesUrl == "*" ? true : e.FilesUrl.ToLower().Contains(filesUrl.ToLower()))
                // .Where(e => videosUrl == "*" ? true : e.VideosUrl.ToLower().Contains(videosUrl.ToLower()))
                // .Where(e => semester == 0 ? true : e.Semester == semester)
                .Where(e => idBranche == 0 ? true : e.IdBranche == idBranche)
                .Where(e => idNiveauScolaire == 0 ? true : e.IdNiveauScolaire == idNiveauScolaire)
                .Where(e => idMatier == 0 ? true : e.IdMatier == idMatier)
                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Cours>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    nomAr = e.NomAr,
                    filesUrl = e.FilesUrl,
                    videosUrl = e.VideosUrl,
                    semester = e.Semester,
                    branche = e.Branche.Nom,
                    Matier = e.Matier.Name,
                    IdMatier = e.IdMatier,
                    idBranche = e.IdBranche,
                    CreationDate = e.CreationDate,
                    niveauScolaire = e.NiveauScolaire.Nom,
                    idNiveauScolaire = e.IdNiveauScolaire,

                })
                .ToListAsync()
                ;

            return Ok(new { list = list, count = count });
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(int id)
        {
            var list = await _context.Courses.Where(e => e.Id == id)
                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    nomAr = e.NomAr,
                    semester = e.Semester,
                    branche = e.Branche.Nom,
                    idBranche = e.IdBranche,
                    Matier = e.Matier.Name,
                    IdMatier = e.IdMatier,
                    CreationDate = e.CreationDate,
                    Content = e.Content,
                    niveauScolaire = e.NiveauScolaire.Nom,
                    idNiveauScolaire = e.IdNiveauScolaire,
                })
                .SingleOrDefaultAsync()
                ;

            return Ok(list);
        }

        [HttpGet("{idNiveauScolaire}/{idBranche}")]
        public async Task<IActionResult> GetByNiveauAndBranche(int idNiveauScolaire, int idBranche)
        {
            var list = await _context.Courses.Where(e => idNiveauScolaire == 0 ? true : e.IdNiveauScolaire == idNiveauScolaire)
                .Where(e => idBranche == 0 ? true : e.IdBranche == idBranche)
                .Select(e => new
                {
                    id = e.Id,
                    nom = e.Nom,
                    nomAr = e.NomAr,
                    semester = e.Semester,
                    branche = e.Branche.Nom,
                    Matier = e.Matier.Name,
                    IdMatier = e.IdMatier,
                    CreationDate = e.CreationDate,
                    idBranche = e.IdBranche,
                    niveauScolaire = e.NiveauScolaire.Nom,
                    idNiveauScolaire = e.IdNiveauScolaire,
                })
                .ToListAsync()
                ;

            return Ok(list);
        }
    }
}