namespace angular.Web.Models
{
    public class CreditCard : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string CVV { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
