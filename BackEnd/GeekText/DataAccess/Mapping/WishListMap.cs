using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class WishListMap
    {
        public WishListMap(EntityTypeBuilder<WishList> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);


            entityBuilder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.WishLists)
                .HasForeignKey(x => x.UserId);
        }
    }
}
