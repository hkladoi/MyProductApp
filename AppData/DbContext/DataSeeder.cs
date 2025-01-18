using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DbContexts
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.ProductCategories.Any())
            {
                var categories = Enumerable.Range(1, 20).Select(i => new ProductCategory
                {
                    Name = $"Category {i}",
                    ImportDate = DateTime.Now
                }).ToList();
                context.ProductCategories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var random = new Random();
                var categories = context.ProductCategories.ToList();

                var products = Enumerable.Range(1, 10000).Select(i => new Product
                {
                    Name = $"Product {i}",
                    Price = random.Next(1, 1000),
                    ImportDate = DateTime.Now,
                    ProductDetails = new List<ProductDetail>
                {
                    new ProductDetail
                    {
                        ProductCategoryId = categories[random.Next(categories.Count)].Id
                    }
                }
                }).ToList();

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
