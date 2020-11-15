using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using angular.Web.Models;
using reactiveFormWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using angular.Web.Validations;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace angular.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class WishlistController : Controller
    {
        public const string QueryLimitKey = "QueryLimit";
        private readonly ApplicationDbContext _context;
        public int Limit { get; set; }

        public WishlistController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Limit = configuration.GetValue(QueryLimitKey, 50);
        }

        // GET: api/Wishlist
        [HttpGet]
        public async Task<IEnumerable<WishList>> Get([FromQuery] WishListFilter filter, [FromQuery] QueryOptions options)
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
                    Expression<Func<WishList, object>> orderExpression = x => options.OrderBy;
                    if (options.SortOrder > 0)
                        query = query.OrderBy(orderExpression);
                    else
                        query = query.OrderByDescending(orderExpression);
                }

                query = query.Skip(Math.Max(0, options.Skip))
                             .Take(Math.Max(0, Math.Min(Limit, options.Top)));
                try
                {
                    var result = await query.ToListAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
                return null;
        }

        // GET: api/Wishlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishList>> GetWishList(int id, bool  includeDetails = false)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = _context.WishList.AsNoTracking();
            if(includeDetails)
            {
                query = query
                    .Include(x => x.Details).ThenInclude(y => y.Book);
            }

            try
            {
                var wishlist = await query.SingleOrDefaultAsync(i => i.Id == id);
                if (wishlist == null)
                {
                    return NotFound();
                }

                return Ok(wishlist);
            }
            catch(Exception ex)
            {
                throw;
            }



        }

        // PUT: api/Wishlist/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishList(int id, WishList newEntity)
        {
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var oldentity = await _context.WishList.FirstOrDefaultAsync(x => x.Id == id);
                if (oldentity != null)
                {
                    oldentity.Name = newEntity.Name;
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

        // POST: api/Wishlist
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WishList>> PostWishList(WishList wishList)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                wishList.UserId = int.Parse(currentUserId);
                var query = _context.WishList.AsNoTracking();
                query = query.Where(x => x.UserId == wishList.UserId);
                var existingwishList = await query.ToListAsync();
                if (existingwishList != null && existingwishList.Count == 3)
                {
                    var payload = new { errors = new string[] { "You canot have more than 3 wish Lists " } };
                    return Conflict(payload);
                }
                _context.WishList.Add(wishList);
                await _context.SaveChangesAsync();

                //Something like first elemnt by generic
                return CreatedAtAction("PostEntity", new { id = wishList.Id }, wishList);
            }
            else
                return NotFound("Invalid user");
        }

        // DELETE: api/Wishlist/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WishList>> DeleteWishList(int id)
        {
            var wishList = await _context.WishList.FindAsync(id);
            if (wishList == null)
            {
                return NotFound();
            }

            _context.WishList.Remove(wishList);
            await _context.SaveChangesAsync();

            return wishList;
        }

        private bool EntityExists(WishList entity)
        {
            if (entity == null)
                return false;
            return _context.WishList.Any(e => e.Id == entity.Id);
        }

        protected virtual string DefaultOrderBy()
        {
            return "Id";
        }

        protected IQueryable<WishList> ApplyFilter(WishListFilter filter)
        {
            var query = _context.WishList.AsNoTracking().Where(x => x.UserId == filter.UserId);

            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }

            query = query.Where(x => x.UserId == filter.UserId);

            return query;
        }


    }
}
