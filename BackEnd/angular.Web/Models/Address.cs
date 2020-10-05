namespace angular.Web.Models
{

    public class AddressFilter
    {
        public int? Id { get; set; }
        public string SearchString { get; set; }
    }

    public class Address : EntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
