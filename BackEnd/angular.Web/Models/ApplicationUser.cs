using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace angular.Web.Models
{
    public class ApplicationUser: IdentityUser<int>, EntityBase
    {
        public string NickName { get; set; }
        public string Name { get; set; }
        public int? HomeAddressId { get; set; }
        public Address HomeAddress { get; set; }
        public List<Address> ShippingAddresses { get; set; }
        public List<CreditCard> CreditCards { get; set; }
        public List<SaveForLaterItem> SaveForLaterItems { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public List<WishList> WishLists { get; set; }
    }
}
