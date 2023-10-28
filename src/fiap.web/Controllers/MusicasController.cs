using fiap.domain.Models;
using fiap.persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace fiap.web.Controllers
{

    [Authorize]
    public class MusicasController : Controller
    {
        private MusicaContext _context;

        public MusicasController(MusicaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teste = User;
            var musicas = _context.Musicas.ToList();

            return View(musicas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Musica model)
        {
            if (ModelState.IsValid)
            {
                //salva no banco
                _context.Musicas.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var musica = _context.Musicas.Find(Id);

            return View(musica);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Musica model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            return View(_context.Musicas.First(a => a.Id == Id));
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            #region exemplos tasks
            //var musica = _context.Musicas.Single(a => a.Id == Id);
            //Select top 2 * from musicas where id =1

            //var musica1 = _context.Musicas.SingleOrDefaultAsync(a => a.Id == Id + 1);
            //var musica2 = _context.Musicas.SingleOrDefaultAsync(a => a.Id == Id + 2);
            //var musica3 = _context.Musicas.SingleOrDefaultAsync(a => a.Id == Id + 3);

            //List<Task> tasks = new List<Task>() { musica, musica1, musica2, musica3 };

            //Task.WaitAll(tasks.ToArray());
            //Task.WaitAny(tasks.ToArray());
            ////null

            //var musica = _context.Musicas.First(a => a.Id == Id);
            ////select top 1

            //var musica = _context.Musicas.FirstOrDefault(a => a.Id == Id);
            ////null
            #endregion

            var musica = await _context.Musicas.SingleOrDefaultAsync(a => a.Id == Id);
            if (musica != null)
            {
                _context.Musicas.Remove(musica);
                // _context.SaveChanges();
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }


            return new NotFoundResult();
        }
    }
}
