using angular.Web.Models;
using angular.Web.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using reactiveFormWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace angular.Web.Controllers
{
    [ApiController]
    public abstract class CRUDController<TEntity, TFilter> : Controller where TEntity : class, EntityBase
    {
        public const string QueryLimitKey = "QueryLimit";
        public ApplicationDbContext Context { get; set; }
        public DbSet<TEntity> Repository { get; set; }
        public int Limit { get; set; }

        public CRUDController(ApplicationDbContext context, IConfiguration configuration)
        {
            Context = context;
            Repository = context.Set<TEntity>();
            Limit = configuration.GetValue(QueryLimitKey, 50);
        }

        [HttpGet]
        public virtual async Task<IEnumerable<TEntity>> Get([FromQuery] TFilter filter, [FromQuery] QueryOptions options)
        {
            if (options.Top <= 0 || options.Top > Limit)
                options.Top = Limit;

            if (string.IsNullOrEmpty(options.OrderBy))
                options.OrderBy = DefaultOrderBy();

            var query = ApplyFilter(filter);


            if (!string.IsNullOrEmpty(options.OrderBy))
            {
                Expression<Func<TEntity, object>> orderExpression = x => options.OrderBy;
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

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetEntity([FromRoute] int id)
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
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutEntity([FromRoute] int id, [FromBody] TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        [HttpPost]
        public virtual async Task<IActionResult> PostEntity([FromBody] TEntity entity)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Repository.Add(entity);
            await Context.SaveChangesAsync();
            //Something like first elemnt by generic
            return CreatedAtAction("PostEntity", new { id = entity.Id }, entity);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int id)
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
            Repository.Remove(entity);
            await Context.SaveChangesAsync();
            return Ok(entity);
        }

        protected virtual string DefaultOrderBy()
        {
            return "Id";
        }
        protected abstract IQueryable<TEntity> ApplyFilter(TFilter filter);

        protected virtual bool EntityExists(TEntity entity)
        {
            return Repository.Any(e => entity != null && e.Id == entity.Id);
        }


    }
}
