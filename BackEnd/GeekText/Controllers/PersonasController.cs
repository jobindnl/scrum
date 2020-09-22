using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Personas")]
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public IEnumerable<Person> GetPersonas()
        {
            return _context.Person;
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona([FromRoute] int id, bool incluirDirecciones = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person persona;

            if (incluirDirecciones)
            {
                persona = await _context.Person.Include(x => x.Direcciones).SingleOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                persona = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            }

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        private async Task CrearOEditarDirecciones(List<Address> direcciones)
        {
            List<Address> direccionesACrear = direcciones.Where(x => x.Id == 0).ToList();
            List<Address> direccionesAEditar = direcciones.Where(x => x.Id != 0).ToList();

            if (direccionesACrear.Any())
            {
                await _context.AddRangeAsync(direccionesACrear);
            }

            if (direccionesAEditar.Any())
            {
                _context.UpdateRange(direccionesAEditar);
            }

        }

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona([FromRoute] int id, [FromBody] Person persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await CrearOEditarDirecciones(persona.Direcciones);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personas
        [HttpPost]
        public async Task<IActionResult> PostPersona([FromBody] Person persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Person.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = await _context.Person.SingleOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Person.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok(persona);
        }

        private bool PersonaExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}