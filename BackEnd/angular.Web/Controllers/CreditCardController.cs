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
    [Route("api/CreditCard")]
    public class CreditCardController : CRUDController<CreditCard, CreditCardFilter>
    {
        UserManager<ApplicationUser> _userManager;

        public CreditCardController(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager) : base(context, configuration)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public override async Task<IEnumerable<CreditCard>> Get([FromQuery] CreditCardFilter filter, [FromQuery] QueryOptions options)
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
                    Expression<Func<CreditCard, object>> orderExpression = x => options.OrderBy;
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
            else
                return null;
        }

        [HttpPost]
        public override async Task<IActionResult> PostEntity([FromBody] CreditCard newEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                newEntity.UserId = int.Parse(currentUserId);
                var query = Context.CreditCard.AsNoTracking();
                query = query.Where(x => x.UserId == newEntity.UserId);
                var oldCreditCardes = await query.ToListAsync();
                var existingCreditCard = oldCreditCardes.FirstOrDefault(x => x.IsAlikeTo(
                    newEntity.Name,
                    newEntity.Number,
                    newEntity.ExpMonth,
                    newEntity.ExpYear,
                    newEntity.CVV));

                if (existingCreditCard != null)
                    newEntity = existingCreditCard;

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
        public override async Task<IActionResult> PutEntity([FromRoute] int id, [FromBody] CreditCard entity)
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
                if (currentUser?.DefaultCreditCardId == id)
                {
                    var payload = new { errors = new string[]{"This credit card cannot be deleted because it is set as default"}};
                    return Conflict(payload);
                }
                Repository.Remove(entity);
                await Context.SaveChangesAsync();
                return Ok(entity);
            }
            else
            {
                return NotFound();
            }

        }

        protected override IQueryable<CreditCard> ApplyFilter(CreditCardFilter filter)
        {
            var query = Repository.AsNoTracking().Where(x => x.UserId == filter.UserId);

            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }

            query = query.Where(x => x.UserId == filter.UserId);

            return query;
        }
    }
}