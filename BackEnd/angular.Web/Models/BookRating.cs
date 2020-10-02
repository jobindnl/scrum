namespace angular.Web.Models
{
    public class BookRating : EntityBase
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Rate { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsDisplayedAnonymous { get; set; }
        public string Comments { get; set; }
    }
}
