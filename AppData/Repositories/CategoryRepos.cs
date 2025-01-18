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
    public class CategoryRepos : ICategoryRepos
    {
        private readonly AppDbContext _context;

        public CategoryRepos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetCategoriesAsync(string? keyword)
        {
            try
            {
                List<CategoryDto> categories = new List<CategoryDto>();
                var query = _context.Categories.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(p => p.Name.Contains(keyword));
                }
                var data = await query.ToListAsync();
                foreach (var item in data)
                {
                    var productCount = await _context.ProductDetails.Where(p => p.ProductCategoryId == item.Id).CountAsync();
                    categories.Add(new CategoryDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ImportDate = item.ImportDate,
                        ProductCount = productCount
                    });
                }
                return new ApiResponse
                {
                    Data = categories,
                    Status = 200,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Data = null,
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }

        public async Task<ApiResponse> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                List<Category> categories = new List<Category>();
                if (category != null)
                {
                    categories.Add(category);
                    return new ApiResponse
                    {
                        Data = categories,
                        Status = 200,
                        Message = "Success"
                    };
                }
                return new ApiResponse
                {
                    Data = null,
                    Status = 404,
                    Message = "Category not found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Data = null,
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }

        public async Task<ApiResponse> AddCategoryAsync(Category category)
        {
            try
            {
                var checkCategory = await _context.Categories.FirstOrDefaultAsync(p => p.Name == category.Name);
                if (checkCategory != null)
                {
                    return new ApiResponse
                    {
                        Status = 400,
                        Message = "Category already exists"
                    };
                }
                category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = category.Name,
                    ImportDate = category.ImportDate,
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new ApiResponse
                {
                    Status = 200,
                    Message = "Add category successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }

        public async Task<ApiResponse> UpdateCategoryAsync(Category category)
        {
            try
            {
                var checkCategory = await _context.Categories.FirstOrDefaultAsync(p => p.Id == category.Id);
                if (checkCategory == null)
                {
                    return new ApiResponse
                    {
                        Status = 400,
                        Message = "Category not found"
                    };
                }
                checkCategory.Id = category.Id;
                checkCategory.Name = category.Name;
                checkCategory.ImportDate = category.ImportDate;
                _context.Categories.Update(checkCategory);
                await _context.SaveChangesAsync();
                return new ApiResponse
                {
                    Status = 200,
                    Message = "Update category successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse> DeleteCategoryAsync(Guid id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                var productCount = await _context.ProductDetails.Where(p => p.ProductCategoryId == id).CountAsync();
                if (productCount > 0)
                {
                    return new ApiResponse
                    {
                        Status = 400,
                        Message = "Category has product"
                    };
                }
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    return new ApiResponse
                    {
                        Status = 200,
                        Message = "Delete category successfully"
                    };
                }
                return new ApiResponse
                {
                    Status = 404,
                    Message = "Category not found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }
    }
}
