using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_BookStore.Models;

namespace Project_BookStore.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>()
                .HasData(
                new Product
                {
                    Id = 1,
                    Title = "World in a Bottle",
                    Description = "World in a Bottle is a captivating science fiction novella authored by using Allen Kim Lang.\r\n" +
                              " The story immerses readers in a world of clinical marvel and moral dilemmas.\r\n" +
                              "Set in a futuristic society the narrative follows Dr. Martin Hale a notable scientist with a imaginative and prescient\r\n " +
                              "to create a self-contained miniature universe inside a tumbler bottle.",
                    Author = "Allen Kim Lang",
                    Price = 99,
                    CategoryId = 1,
                    ImageURL = ""
                },

                new Product
                {
                    Id = 2,
                    Title = "Myths And Marvels Of\r\nAstronomy",
                    Description = "Myths and Marvels of Astronomy is a fascinating paintings authored with the aid of Richard A.\r\n" +
                    " Proctor a prominent 19th-century British astronomer and writer. This ebook takes readers on an enlightening journey thru the fascinating world \r\n" +
                    "of astronomy debunking myths even as revealing the awe-inspiring marvels of the universe. ",
                    Author = "Richard A. Proctor",
                    Price = 299,
                    CategoryId = 2,
                    ImageURL =""
                }
                );
        }

    }
}
