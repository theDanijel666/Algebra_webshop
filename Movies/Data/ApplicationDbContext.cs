using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(200)]
        public string? City { get; set; }

        [StringLength(10)]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }


        [ForeignKey("UserId")]
        public virtual ICollection<Order> Orders { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product { 
                Id=50,
                Title="Rambo",
                Description="Rambo is a 2008 American action film directed by Sylvester Stallone, who co-wrote it based on the character John Rambo created by author David Morrell for his novel First Blood. A sequel to Rambo III (1988) and the fourth installment in the Rambo franchise, it co-stars Julie Benz, Paul Schulze, Matthew Marsden, Graham McTavish, Rey Gallegos, Tim Kang, Jake La Botz, Maung Maung Khin, and Ken Howard. In the film, Rambo leads a group of mercenaries into Burma to rescue Christian missionaries, who have been kidnapped by a local infantry unit.",
                Price=10.99m,
                Active=true,
                Quantity=10
            });
            modelBuilder.Entity<ProductImage>().HasData(new ProductImage
            {
                Id=50,
                ProductId=50,
                IsMainImage=true,
                Title="Rambo",
                FileName="/images/products/50/rambo.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 51,
                Title ="Rambo 2",
                Description="Rambo: First Blood Part II (also known as Rambo II or First Blood II) is a 1985 American action film directed by George P. Cosmatos and starring Sylvester Stallone, who reprises his role as Vietnam veteran John Rambo. It is the sequel to the 1982 film First Blood, and the second installment in the Rambo franchise. Picking up where the first film left, the sequel is set in the context of the Vietnam War POW/MIA issue; it sees Rambo released from prison by federal order to document the possible existence of POWs in Vietnam, under the belief that he will find nothing, thus enabling the government to sweep the issue under the rug.",
                Price=10.99m,
                Active=true,
                Quantity=10
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 52,
                Title ="Rambo 3",
                Description="Rambo III is a 1988 American action film directed by Peter MacDonald and co-written by Sylvester Stallone, who also reprises his role as Vietnam War veteran John Rambo. A sequel to Rambo: First Blood Part II (1985), it is the third installment in the Rambo franchise, followed by Rambo. It was in turn followed by Rambo: Last Blood (2019).",
                Price=10.99m,
                Active=true,
                Quantity=10
            });
                

            base.OnModelCreating(modelBuilder);
        }
    }

    
}