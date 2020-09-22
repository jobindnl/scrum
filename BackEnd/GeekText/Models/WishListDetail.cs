namespace reactiveFormWeb.Models
{
    public class WishListDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int WishListId { get; set; }
        public WishList WishList { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}
