using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

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
