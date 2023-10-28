using fiap.api.ActionFilters;
using fiap.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiap.api.Controllers
{
    //[ApiVersion("1.0")]
    //[Route("api/{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    //[CustomAuthorize]
    [Authorize]
    public class MusicasController : Controller
    {
        private MusicaContext _context;

        public MusicasController(MusicaContext context)
        {
            _context = context;
        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var musicas = new List<Musica>() { new Musica(), new Musica() };

        //    return Ok( musicas);
        //}



        //[HttpGet]
        //public ActionResult<List<Musica>> Get()
        //{
        //    var musicas = new List<Musica>() { new Musica(), new Musica() };

        //    return Ok(musicas);
        //}



        //[HttpGet]
        //public List<Musica> Get()
        //{
        //    var musicas = new List<Musica>() { new Musica(), new Musica() };

        //    //if (!musicas.Any())
        //    //{
        //    //    return NotFound();
        //    //}

        //    return musicas;
        //}


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Musica>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Musica>>> Get()
        {
            return Ok(await _context.Musicas.ToListAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Musica>> Get(int id)
        {
            var musica = await _context.Musicas.FirstOrDefaultAsync(a => a.Id == id);
            if (musica == null)
                return NotFound();

            return Ok(musica);
        }

        [HttpPost]
        //public async Task<ActionResult<Musica>> Post([FromBody]Musica model)
        public async Task<ActionResult<Musica>> Post(Musica model)
        {
            //if (ModelState.IsValid)
            //{
            _context.Musicas.Add(model);
            await _context.SaveChangesAsync();

            return Created($"/api/musicas/{model.Id}", model);
            //}

            //return BadRequest(ModelState);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Musica>> Put(Musica model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            
            return model;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var musica = await _context.Musicas.FirstOrDefaultAsync(a => a.Id == id);
            if (musica == null)
                return NotFound();

            _context.Remove(musica);
            //_context.Musicas.Remove(musica);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
