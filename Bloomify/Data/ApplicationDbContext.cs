using Bloomify.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

     
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<OrderProduct>()
                .HasKey(os => new { os.OrderId, os.ProductId });

            builder.Entity<CartProduct>()
                          .HasKey(os => new { os.CartId, os.ProductId });


        }

        public DbSet<Bloomify.Models.Cart>? Cart { get; set; }

        public DbSet<Bloomify.Models.CartProduct>? CartProduct { get; set; }

        public DbSet<Bloomify.Models.Category>? Category { get; set; }

        public DbSet<Bloomify.Models.Order>? Order { get; set; }

        public DbSet<Bloomify.Models.OrderProduct>? OrderProduct { get; set; }

        public DbSet<Bloomify.Models.Product>? Product { get; set; }

        public DbSet<Bloomify.Models.ProductImage>? ProductImage { get; set; }

        public DbSet<Bloomify.Models.Review>? Review { get; set; }

        public DbSet<Bloomify.Models.Testimonial>? Testimonial { get; set; }
    }
}
