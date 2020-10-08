using angular.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class ApplicationUserMap
    {
        public ApplicationUserMap(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
             
            entityBuilder.Property(x => x.NickName)
                .HasMaxLength(150);

            entityBuilder.Property(x => x.Name)
                .HasMaxLength(150);

            entityBuilder.HasOne(x => x.HomeAddress)
                .WithMany()
                .HasForeignKey(x => x.HomeAddressId);

            entityBuilder.HasOne(x => x.DefaultCreditCard)
               .WithMany()
               .HasForeignKey(x => x.DefaultCreditCardId);
        }
    }
}
