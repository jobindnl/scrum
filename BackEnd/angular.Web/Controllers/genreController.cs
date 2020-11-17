using angular.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using reactiveFormWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Genre")]
    public class GenreController: CRUDController<Book, GenreFilter>
    {
        public GenreController(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager) : base(context, configuration)
        {


        }

        protected override IQueryable<Book> ApplyFilter(GenreFilter filter)
        {
            var query = Repository.AsNoTracking();

            if (filter.Id != null)
            {
                query = query.Where(x => x.GenreId == filter.Id.Value);
            }

            return query;
        }
    }
}
