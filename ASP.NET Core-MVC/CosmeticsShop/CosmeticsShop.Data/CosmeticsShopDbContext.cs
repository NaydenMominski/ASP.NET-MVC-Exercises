namespace CosmeticsShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using CosmeticsShop.Data.Models;

    public class CosmeticsShopDbContext : IdentityDbContext<User>
    {
        public CosmeticsShopDbContext(DbContextOptions<CosmeticsShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manifacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

            builder
            .Entity<Product>()
            .HasOne(p => p.Manifacturer)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.ManifacturerId);

            builder
            .Entity<Image>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId);


            base.OnModelCreating(builder);

        }
    }
}
