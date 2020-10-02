using System;

namespace angular.Web.Models
{
    public class SaveForLaterItem : EntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
