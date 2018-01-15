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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
