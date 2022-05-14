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
    public class ProfsController : SuperController<Prof>
    {
        public ProfsController(MyContext context) : base(context)
        { }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}/{lien}/{experience}/{approche}/{intro}/{videoUrl}/{cvUrl}/{note}/{prixHrWeb}/{prixHrHome}/{prixHrWebGroupe}/{prixHrHomeGroupe}/{idsTypeActivites}/{idsActivites}/{idsTypeCours}/{idsLieuCours}/{idsNiveauScolaires}/{idUser}")]
        public async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir, string lien, string experience, string approche, string intro, string videoUrl, string cvUrl, int note, int prixHrWeb, int prixHrHome, int prixHrWebGroupe, int prixHrHomeGroupe, string idsTypeActivites, string idsActivites, string idsTypeCours, string idsLieuCours, string idsNiveauScolaires, int idUser)
        {
            var q = _context.Profs
                .Where(e => lien == "*" ? true : e.Lien.ToLower().Contains(lien.ToLower()))
                .Where(e => experience == "*" ? true : e.Experience.ToLower().Contains(experience.ToLower()))
                .Where(e => approche == "*" ? true : e.Approche.ToLower().Contains(approche.ToLower()))
                .Where(e => intro == "*" ? true : e.Intro.ToLower().Contains(intro.ToLower()))
                .Where(e => videoUrl == "*" ? true : e.VideoUrl.ToLower().Contains(videoUrl.ToLower()))
                .Where(e => cvUrl == "*" ? true : e.CvUrl.ToLower().Contains(cvUrl.ToLower()))
                .Where(e => note == 0 ? true : e.Note == note)
                .Where(e => prixHrWeb == 0 ? true : e.PrixHrWeb == prixHrWeb)
                .Where(e => prixHrHome == 0 ? true : e.PrixHrHome == prixHrHome)
                .Where(e => prixHrWebGroupe == 0 ? true : e.PrixHrWebGroupe == prixHrWebGroupe)
                .Where(e => prixHrHomeGroupe == 0 ? true : e.PrixHrHomeGroupe == prixHrHomeGroupe)
                .Where(e => idsTypeActivites == "*" ? true : e.IdsTypeActivites.ToLower().Contains(idsTypeActivites.ToLower()))
                .Where(e => idsActivites == "*" ? true : e.IdsActivites.ToLower().Contains(idsActivites.ToLower()))
                .Where(e => idsTypeCours == "*" ? true : e.IdsTypeCours.ToLower().Contains(idsTypeCours.ToLower()))
                .Where(e => idsLieuCours == "*" ? true : e.IdsLieuCours.ToLower().Contains(idsLieuCours.ToLower()))
                .Where(e => idsNiveauScolaires == "*" ? true : e.IdsNiveauScolaires.ToLower().Contains(idsNiveauScolaires.ToLower()))
                .Where(e => idUser == 0 ? true : e.IdUser == idUser)

                ;

            int count = await q.CountAsync();

            var list = await q.OrderByName<Prof>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)

                .Select(e => new
                {
                    id = e.Id,
                    lien = e.Lien,
                    description = e.Description,
                    experience = e.Experience,
                    approche = e.Approche,
                    intro = e.Intro,
                    videoUrl = e.VideoUrl,
                    cvUrl = e.CvUrl,
                    note = e.Note,
                    prixHrWeb = e.PrixHrWeb,
                    prixHrHome = e.PrixHrHome,
                    prixHrWebGroupe = e.PrixHrWebGroupe,
                    prixHrHomeGroupe = e.PrixHrHomeGroupe,
                    IdsTypeActivites = e.IdsTypeActivites,
                    IdsActivites = e.IdsActivites,
                    IdsTypeCours = e.IdsTypeCours,
                    IdsLieuCours = e.IdsLieuCours,
                    IdsNiveauScolaires = e.IdsNiveauScolaires,
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
            var model = await _context.Profs.Where(e => e.IdUser == id)
            .FirstOrDefaultAsync()
            ;

            return Ok(model);
        }
    }
}