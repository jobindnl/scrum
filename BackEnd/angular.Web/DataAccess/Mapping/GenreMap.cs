using angular.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class GenreMap
    {
        public GenreMap(EntityTypeBuilder<Genre> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
