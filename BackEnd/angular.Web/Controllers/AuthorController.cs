using angular.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using reactiveFormWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace angular.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Author")]
    public class AuthorController : CRUDController<Book, AuthorFilter>
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager) : base(context, configuration)
        {
            _context = context;
        }

        [HttpGet("all")]
        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Author;
        }

        protected override IQueryable<Book> ApplyFilter(AuthorFilter filter)
        {
            var query = Repository.AsNoTracking();

            if (filter.Id != null)
            {
                query = query.Where(x => x.AuthorId == filter.Id.Value);
            }

            return query;
        }
    }
}
