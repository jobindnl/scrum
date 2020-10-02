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
    [Route("api/Address")]
    public class AddressController : CRUDController<Address, AddressFilter>
    {
        public AddressController(ApplicationDbContext context, IConfiguration configuration) : base(context, configuration)
        {

        }

        [HttpPost]
        public override async Task<IActionResult> PostEntity([FromBody] Address newEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var query = Context.Address.AsNoTracking();
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

        protected override IQueryable<Address> ApplyFilter(AddressFilter filter)
        {
            var query = Repository.AsNoTracking();

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