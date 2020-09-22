using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class AuthorMap
    {
        public AuthorMap(EntityTypeBuilder<Author> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
