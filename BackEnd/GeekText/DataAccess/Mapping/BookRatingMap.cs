using Microsoft.EntityFrameworkCore.Metadata.Builders;
using reactiveFormWeb.Models;

namespace reactiveFormWeb.DataAccess.Mapping
{
    public class BookRatingMap
    {
        public BookRatingMap(EntityTypeBuilder<BookRating> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.BookId)
                .IsRequired();

            entityBuilder.HasOne(x => x.ApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            entityBuilder.HasOne(x => x.Book)
            .WithMany(x => x.BookRatings)
            .HasForeignKey(x => x.BookId);

        }
    }
}
