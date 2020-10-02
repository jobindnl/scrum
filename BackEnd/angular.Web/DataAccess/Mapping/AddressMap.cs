using angular.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class AddressMap
    {
        public AddressMap(EntityTypeBuilder<Address> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.StreetAddress)
                .IsRequired()
                .HasMaxLength(200);

            entityBuilder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(150);

            entityBuilder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(150);

            entityBuilder.Property(x => x.ZipCode)
                .IsRequired()
                .HasMaxLength(150);

            entityBuilder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(150);

            entityBuilder.HasOne(x => x.ApplicationUser)
            .WithMany(x => x.ShippingAddresses)
            .HasForeignKey(x => x.UserId);


        }
    }
}
