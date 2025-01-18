using AppData.DbContexts;
using AppData.Dto;
using AppData.IRepositories;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public class ProductRepos : IProductRepos
    {
        private readonly AppDbContext _context;
        public ProductRepos(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetProductsAsync()
        {
            return await _context.Products
                .Select(p => new ProductRespone
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Category_Name = string.Join(", ", p.ProductDetails.Select(pc => pc.Category.Name)),
                    ImportDate = p.ImportDate
                })
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.ProductDetails)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
