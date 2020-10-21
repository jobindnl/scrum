using System;
using System.Collections.Generic;

namespace angular.Web.Models
{
    public class WishList : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<WishListDetail> Details { get; set; }
    }
}
