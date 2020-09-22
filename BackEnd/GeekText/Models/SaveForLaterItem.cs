using System;

namespace reactiveFormWeb.Models
{
    public class SaveForLaterItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
