using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular.Web.Controllers;
using angular.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.Controllers
{
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ApplicationUser")]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
        }

        [HttpGet("{email}")]
        public virtual async Task<IActionResult> GetEntity([FromRoute] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.UserName == email);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> PutEntity([FromRoute] string email, [FromBody] ApplicationUser newEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldentity = await _context.ApplicationUser
                .Include(x => x.HomeAddress)
                .Include(x => x.ShippingAddresses)
                .SingleOrDefaultAsync(m => m.UserName == email);
            if (oldentity == null)
            {
                return NotFound();
            }
            else
            {
                if (newEntity.HomeAddress != null)
                {

                    if (oldentity.ShippingAddresses != null && oldentity.ShippingAddresses.Count > 0)
                    {
                        var existingAddress = oldentity.ShippingAddresses.FirstOrDefault(x => x.IsAlikeTo(
                            newEntity.HomeAddress.StreetAddress,
                            newEntity.HomeAddress.City,
                            newEntity.HomeAddress.State,
                            newEntity.HomeAddress.ZipCode,
                            newEntity.HomeAddress.Country));
                        if (existingAddress != null)
                        {
                            oldentity.HomeAddressId = existingAddress.Id;
                        }
                        else
                        {
                            oldentity.ShippingAddresses.Add(newEntity.HomeAddress);
                            oldentity.HomeAddress = newEntity.HomeAddress;
                        }
                    }
                    else
                    {
                        oldentity.ShippingAddresses.Add(newEntity.HomeAddress);
                        oldentity.HomeAddress = newEntity.HomeAddress;
                    }
                }
                oldentity.NickName = newEntity.NickName;
                oldentity.Email = newEntity.Email;
                oldentity.PhoneNumber = newEntity.PhoneNumber;
            }
            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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

        private bool EntityExists(ApplicationUser entity)
        {
            return _context.ApplicationUser.Any(e => entity != null && e.Id == entity.Id);
        }

    }
}