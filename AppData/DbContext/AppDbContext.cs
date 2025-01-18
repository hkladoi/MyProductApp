using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductDetail>()
                .HasKey(ppc => new { ppc.ProductId, ppc.ProductCategoryId });

            modelBuilder.Entity<ProductDetail>()
                .HasOne(ppc => ppc.Product)
                .WithMany(p => p.ProductDetails)
                .HasForeignKey(ppc => ppc.ProductId);

            modelBuilder.Entity<ProductDetail>()
                .HasOne(ppc => ppc.ProductCategory)
                .WithMany(pc => pc.ProductDetails)
                .HasForeignKey(ppc => ppc.ProductCategoryId);
            modelBuilder.
               ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=HKLADOI\SQLEXPRESS;Initial Catalog=ProductApp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

        }
    }
}
