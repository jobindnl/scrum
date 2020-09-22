using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class PersonMap
    {
        public PersonMap(EntityTypeBuilder<Person> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Id)
                .IsRequired();
            
            entityBuilder.Property(x => x.DateOFBirth)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
