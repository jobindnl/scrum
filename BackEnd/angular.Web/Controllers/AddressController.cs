using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using angular.Web.Controllers;
using angular.Web.Models;
using angular.Web.Validations;
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
    [Route("api/Address")]
    public class AddressController : CRUDController<Address, AddressFilter>
    {
        UserManager<ApplicationUser> _userManager;
        public AddressController(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager) : base(context, configuration)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public override async Task<IEnumerable<Address>> Get([FromQuery] AddressFilter filter, [FromQuery] QueryOptions options)
        {
            
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                if (options.Top <= 0 || options.Top > Limit)
                    options.Top = Limit;

                if (string.IsNullOrEmpty(options.OrderBy))
                    options.OrderBy = DefaultOrderBy();

                filter.UserId = int.Parse(currentUserId);
                var query = ApplyFilter(filter);

                if (!string.IsNullOrEmpty(options.OrderBy))
                {
                    Expression<Func<Address, object>> orderExpression = x => options.OrderBy;
                    if (options.SortOrder > 0)
                        query = query.OrderBy(orderExpression);
                    else
                        query = query.OrderByDescending(orderExpression);
                }

                query = query.Skip(Math.Max(0, options.Skip))
                             .Take(Math.Max(0, Math.Min(Limit, options.Top)));

                var result = await query.ToListAsync();

                return result;

            }
            else return null;
        }

        [HttpPost]
        public override async Task<IActionResult> PostEntity([FromBody] Address newEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var query = Context.Address.AsNoTracking();
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                newEntity.UserId = int.Parse(currentUserId);
                query = query.Where(x => x.UserId == newEntity.UserId);
                var oldAddresses = await query.ToListAsync();
                var existingAddress = oldAddresses.FirstOrDefault(x => x.IsAlikeTo(
                    newEntity.StreetAddress,
                    newEntity.City,
                    newEntity.State,
                    newEntity.ZipCode,
                    newEntity.Country));
                if (existingAddress != null)
                    newEntity = existingAddress;
                else
                {
                    Repository.Add(newEntity);
                    await Context.SaveChangesAsync();
                }
                //Something like first elemnt by generic
                return CreatedAtAction("PostEntity", new { id = newEntity.Id }, newEntity);
            }
            else
                return NotFound("Invalid user");

        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> PutEntity([FromRoute] int id, [FromBody] Address entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                entity.UserId = int.Parse(currentUserId);
                Context.Entry(entity).State = EntityState.Modified;

                try
                {
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(entity))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction("PutEntity", new { id = entity.Id }, entity);
            }
            else
                return NotFound("Invalid user");
        }

        // DELETE: api/CreditCard/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await Repository.SingleOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var currentUser = await Context.ApplicationUser.AsNoTracking().FirstOrDefaultAsync(x => x.Id == int.Parse(currentUserId));
                if (currentUser?.HomeAddressId == id)
                {
                    var payload = new { errors = new string[] { "This address cannot be deleted because it is set as home address" } };
                    return Conflict(payload);
                }
                Repository.Remove(entity);
                try
                {
                    await Context.SaveChangesAsync();
                    return Ok(entity);
                }
                catch(Exception ex)
                {
                    throw;
                }
                
                
            }
            else
            {
                return NotFound();
            }

        }

        protected override IQueryable<Address> ApplyFilter(AddressFilter filter)
        {
            var query = Repository.AsNoTracking().Where(x=> x.UserId == filter.UserId);

            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(x => x.StreetAddress.Contains(filter.SearchString) || x.City.Contains(filter.SearchString) || x.State.Contains(filter.SearchString) || x.ZipCode.Contains(filter.SearchString) || x.Country.Contains(filter.SearchString));
            }
            return query;
        }
    }
}