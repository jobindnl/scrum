using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class ApplicationUserMap
    {
        public ApplicationUserMap(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);
             
            entityBuilder.Property(x => x.NickName)
                .HasMaxLength(150);

        }
    }
}
