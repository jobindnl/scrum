namespace angular.Web.Models
{
    public class GenreFilter
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public class Genre : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
