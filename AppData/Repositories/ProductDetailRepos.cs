using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.DbContexts;
using AppData.IRepositories;
using AppData.Models;
using Microsoft.EntityFrameworkCore;

namespace AppData.Repositories
{
    public class ProductDetailRepos : IProductDetails
    {
        private readonly AppDbContext _context;

        public ProductDetailRepos(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProductDetailAsync(ProductDetail productDetail)
        {
            _context.ProductDetails.Add(productDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductDetailAsync(Guid productId, Guid categoryId)
        {
            var producDetail = await _context.ProductDetails
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ProductCategoryId == categoryId);

            if (producDetail != null)
            {
                _context.ProductDetails.Remove(producDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
