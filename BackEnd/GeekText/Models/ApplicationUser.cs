using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace reactiveFormWeb.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public int HomeAddressId { get; set; }
        public List<Address> ShippingAddresses { get; set; }
        public List<CreditCard> CreditCards { get; set; }
        public List<SaveForLaterItem> SaveForLaterItems { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public List<WishList> WishLists { get; set; }
    }
}
