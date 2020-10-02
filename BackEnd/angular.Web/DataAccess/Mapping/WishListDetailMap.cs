using angular.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class WishListDetailMap
    {
        public WishListDetailMap(EntityTypeBuilder<WishListDetail> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Quantity)
                .IsRequired();

            entityBuilder.HasOne(x => x.WishList)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.WishListId);

            entityBuilder.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x => x.BookId);

        }
    }
}
