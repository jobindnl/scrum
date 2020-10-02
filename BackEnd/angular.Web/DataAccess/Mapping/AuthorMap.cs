using angular.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
