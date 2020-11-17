using angular.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular.Web.DataAccess.DAO
{
    interface GenreDAO
    {
        public List<Book> retrieveBooks(int genreId);
        public List<Genre> retrieveGenres();
    }
}
