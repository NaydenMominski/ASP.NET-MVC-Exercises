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
        public DbSet<Order> Orders { get; set; }


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
            .Entity<Product>()
            .HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(p => p.ProductId);

            builder
            .Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

            builder
            .Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(o => o.OrderId);


            base.OnModelCreating(builder);

        }
    }
}
