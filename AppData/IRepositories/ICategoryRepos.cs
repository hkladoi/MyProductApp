using AppData.Dto;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepositories
{
    public interface ICategoryRepos
    {
        Task<ApiResponse> GetCategoriesAsync(string? keyword);
        Task<ApiResponse> GetCategoryByIdAsync(Guid id);
        Task<ApiResponse> AddCategoryAsync(Category category);
        Task<ApiResponse> UpdateCategoryAsync(Category category);
        Task<ApiResponse> DeleteCategoryAsync(Guid id);

    }
}
