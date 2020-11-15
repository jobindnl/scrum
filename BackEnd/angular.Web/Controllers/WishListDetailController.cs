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
    public class WishListDetailController : Controller
    {
        public const string QueryLimitKey = "QueryLimit";
        private readonly ApplicationDbContext _context;
        public int Limit { get; set; }

        public WishListDetailController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Limit = configuration.GetValue(QueryLimitKey, 50);
        }

        // GET: api/WishListDetail
        [HttpGet]
        public async Task<IEnumerable<WishListDetail>> Get([FromQuery] WishListDetailFilter filter, [FromQuery] QueryOptions options)
        {
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                if (options.Top <= 0 || options.Top > Limit)
                    options.Top = Limit;

                if (string.IsNullOrEmpty(options.OrderBy))
                    options.OrderBy = DefaultOrderBy();


                var query = ApplyFilter(filter);
                query = query
                    .Include(x => x.Book).ThenInclude(x => x.Author)
                    .Include(x => x.Book).ThenInclude(x=> x.Genre);
                query = query.Where(x => x.WishListId == filter.WishListId);

                if (!string.IsNullOrEmpty(options.OrderBy))
                {
                    Expression<Func<WishListDetail, object>> orderExpression = x => options.OrderBy;
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

        // GET: api/WishListDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishListDetail>> GetWishListDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WishListDetail wishlistDetail;

            wishlistDetail = await _context.WishListDetail
               .FindAsync(id);

            if (wishlistDetail == null)
            {
                return NotFound();
            }

            return wishlistDetail;
        }

        // PUT: api/WishListDetail/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishListDetail(int id, WishListDetail WishListDetail)
        {
            if (id != WishListDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(WishListDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListDetailExists(id))
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

        // POST: api/WishListDetail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WishListDetail>> PostWishListDetail(WishListDetail WishListDetail)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUserId = User.FindFirst("Id").Value;
            if (!string.IsNullOrEmpty(currentUserId))
            {
                _context.WishListDetail.Add(WishListDetail);
                await _context.SaveChangesAsync();

                //Something like first elemnt by generic
                return CreatedAtAction("PostEntity", new { id = WishListDetail.Id }, WishListDetail);
            }
            else
                return NotFound("Invalid user");
        }

        // DELETE: api/WishListDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WishListDetail>> DeleteWishListDetail(int id)
        {
            var WishListDetail = await _context.WishListDetail.FindAsync(id);
            if (WishListDetail == null)
            {
                return NotFound();
            }

            _context.WishListDetail.Remove(WishListDetail);
            await _context.SaveChangesAsync();

            return WishListDetail;
        }

        private bool WishListDetailExists(int id)
        {
            return _context.WishListDetail.Any(e => e.Id == id);
        }

        protected virtual string DefaultOrderBy()
        {
            return "Id";
        }

        protected IQueryable<WishListDetail> ApplyFilter(WishListDetailFilter filter)
        {
            var query = _context.WishListDetail.AsNoTracking();

            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(x => x.Book.Description.Contains(filter.SearchString) || x.Book.PublishingInfo.Contains(filter.SearchString) || x.Book.Author.Name.Contains(filter.SearchString));
            }


            return query;
        }


    }
}
