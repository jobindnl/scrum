using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class CreditCardMap
    {
        public CreditCardMap(EntityTypeBuilder<CreditCard> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

            entityBuilder.Property(x => x.Number)
            .IsRequired()
            .HasMaxLength(16);

            entityBuilder.Property(x => x.ExpMonth)
            .IsRequired();

            entityBuilder.Property(x => x.ExpYear)
            .IsRequired();

            entityBuilder.Property(x => x.CVV)
            .IsRequired()
            .HasMaxLength(3);

            entityBuilder.Property(x => x.UserId)
                .IsRequired();

            entityBuilder.HasOne(x => x.ApplicationUser)
                .WithMany(x=> x.CreditCards)
                .HasForeignKey(x => x.UserId);

        }
    }
}
