using angular.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class BookMap
    {
        public BookMap(EntityTypeBuilder<Book> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);

            entityBuilder.Property(x => x.SalesPrice)
                .HasColumnType("decimal(5,3)");

            entityBuilder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            entityBuilder.Property(x => x.PublishingInfo)
                .IsRequired()
                .HasMaxLength(1000);

            entityBuilder.HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId);

            entityBuilder.Ignore(x => x.RatingAverage);
        }
    }
}
