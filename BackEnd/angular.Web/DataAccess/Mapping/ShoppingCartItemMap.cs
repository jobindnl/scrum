using angular.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class ShoppingCartItemMap
    {
        public ShoppingCartItemMap(EntityTypeBuilder<ShoppingCartItem> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Quantity)
                .IsRequired();

            entityBuilder.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x=> x.BookId);

            entityBuilder.HasOne(x => x.ApplicationUser)
                .WithMany(x=> x.ShoppingCartItems)
                .HasForeignKey(x => x.UserId);
        }
    }
}
