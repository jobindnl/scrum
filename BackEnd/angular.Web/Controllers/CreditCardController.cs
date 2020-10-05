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
    [Route("api/CreditCard")]
    public class CreditCardController : CRUDController<CreditCard, CreditCardFilter>
    {
        public CreditCardController(ApplicationDbContext context, IConfiguration configuration) : base(context, configuration)
        {

        }

        [HttpPost]
        public override async Task<IActionResult> PostEntity([FromBody] CreditCard newEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        protected override IQueryable<CreditCard> ApplyFilter(CreditCardFilter filter)
        {
            var query = Repository.AsNoTracking();

            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            
           query = query.Where(x => x.UserId == filter.UserId);
            
            return query;
        }
    }
}