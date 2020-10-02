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
    [Route("api/Book")]
    public class BookController : CRUDController<Book, BookFilter>
    {
        public BookController(ApplicationDbContext context, IConfiguration configuration) : base(context, configuration)
        {

        }

        protected override IQueryable<Book> ApplyFilter(BookFilter filter)
        {
            var query = Repository.AsNoTracking();

            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(x => x.PublishingInfo.Contains(filter.SearchString) || x.Description.Contains(filter.SearchString) || x.Title.Contains(filter.SearchString));
            }
            if (!string.IsNullOrEmpty(filter.GenreIds))
            {
                var ids = filter.GenreIds.Split(",").Select(int.Parse).ToArray();
                query = query.Where(x => ids.Contains(x.GenreId));
            }
            return query;
        }
    }
}