using System.Collections.Generic;

namespace angular.Web.Models
{
    public class Author: EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public List<Book> Books { get; set; }
    }
}
