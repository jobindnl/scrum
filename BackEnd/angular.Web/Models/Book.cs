using System.Collections.Generic;

namespace angular.Web.Models
{
    public class BookFilter
    {
        public int? Id { get; set; }
        public string SearchString { get; set; }
        public string GenreIds { get; set; }
        public int? TopSellers { get; set; }
        public int? RatingAverage { get; set; }
    }

    public class Book : EntityBase
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public decimal SalesPrice { get; set; }
        public string Description { get; set; }
        public string PublishingInfo { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public decimal RatingAverage { get; set; }
        public List<BookRating> BookRatings { get; set; }
    }
}
