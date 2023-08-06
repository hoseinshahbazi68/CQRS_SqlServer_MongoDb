
using CQRS.Entities.Write;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<BrandEntityModel> Brand { get; set; }
        public DbSet<CatgoryEntityModel> Catgory { get; set; }
        public DbSet<ProductEntityModel> Product { get; set; }

    }
}