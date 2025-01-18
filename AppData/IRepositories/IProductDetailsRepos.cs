using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Dto;
using AppData.Models;

namespace AppData.IRepositories
{
    public interface IProductDetailsRepos
    {
        Task<ApiResponse> AddProductDetailAsync(ProductDetailDto productDetail);
        Task<ApiResponse> DeleteProductDetailAsync(Guid productId, Guid categoryId);
        Task<ApiResponse> UpdateProductDetailAsync(ProductDetailDto productDetail);
        Task<ApiResponse> GetAllProject(string? keyword, string? category);
        Task<ApiResponse> GetProductDetailByIdAsync(Guid productId, Guid categoryId);
    }
}
