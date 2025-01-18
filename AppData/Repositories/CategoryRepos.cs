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

        public async Task<ApiResponse> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
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
                _context.Categories.Update(category);
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
