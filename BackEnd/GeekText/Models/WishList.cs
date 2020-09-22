using System;
using System.Collections.Generic;

namespace reactiveFormWeb.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<WishListDetail> Details { get; set; }
    }
}
