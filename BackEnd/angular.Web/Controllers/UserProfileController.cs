using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular.Web.Controllers;
using angular.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/UserProfile")]
    public class UserProfileController : Controller
    {
        private ApplicationDbContext _context;

        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var entity = await _context.ApplicationUser
                    .Include(x=> x.ShippingAddresses)
                    .Include(x => x.HomeAddress)
                    .Include(x => x.CreditCards)
                    .SingleOrDefaultAsync(m => m.Id == int.Parse(currentUserId)
                    );
                if (entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            else return null;
        }

        [HttpPut("{id}")]
        public  async Task<IActionResult> PutEntity([FromRoute] int id, [FromBody] ApplicationUser newEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var oldentity = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.Id == int.Parse(currentUserId));
                if (oldentity != null)
                {
                    oldentity.Name = newEntity.Name;
                    oldentity.NickName = newEntity.NickName;
                    oldentity.Email = newEntity.Email;
                    oldentity.HomeAddressId = newEntity.HomeAddressId;
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!EntityExists(oldentity))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction("PutEntity", new { id = oldentity.Id }, oldentity);
            }
            else
            {
                return NotFound();
            }
        }

        private bool EntityExists(ApplicationUser entity)
        {
            return _context.ApplicationUser.Any(e => entity != null && e.Id == entity.Id);
        }
    }
}