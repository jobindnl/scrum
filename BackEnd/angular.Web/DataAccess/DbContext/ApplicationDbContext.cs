using angular.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using reactiveFormWeb.DataAccess.Mapping;
using System;
using System.Collections.Generic;

namespace reactiveFormWeb.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookRating> BookRating { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<SaveForLaterItem> SaveForLaterItem { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<WishListDetail> WishListDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            
            new ApplicationUserMap(modelBuilder.Entity<ApplicationUser>());
            new AddressMap(modelBuilder.Entity<Address>());
            new AuthorMap(modelBuilder.Entity<Author>());
            new BookMap(modelBuilder.Entity<Book>());
            new BookRatingMap(modelBuilder.Entity<BookRating>());
            new CreditCardMap(modelBuilder.Entity<CreditCard>());
            new GenreMap(modelBuilder.Entity<Genre>());
            new SaveForLaterItemMap(modelBuilder.Entity<SaveForLaterItem>());
            new ShoppingCartItemMap(modelBuilder.Entity<ShoppingCartItem>());
            new WishListDetailMap(modelBuilder.Entity<WishListDetail>());
            new WishListMap(modelBuilder.Entity<WishList>());
        }

    }
}


