using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Genre")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public GenreController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // GET: api/Personas
        [HttpGet]
        public IEnumerable<Genre> GetGenre()
        {
            return dbContext.Genre;
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Genre([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await dbContext.Genre.SingleOrDefaultAsync(m => m.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }
    }
}