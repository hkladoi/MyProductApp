using AppData.Dto;
using AppData.IRepositories;
using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailsRepos _productCategoryRepository;

        public ProductDetailsController(IProductDetailsRepos productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        [HttpPost]
        public async Task<object> AddProductDetail([FromBody] ProductDetailDto productDetail)
        {
            var respone = await _productCategoryRepository.AddProductDetailAsync(productDetail);
            return respone;
        }

        [HttpDelete]
        public async Task<object> DeleteProductDetail(Guid productId, Guid categoryId)
        {
            var respone = await _productCategoryRepository.DeleteProductDetailAsync(productId, categoryId);
            return respone;
        }

        [HttpGet]
        public async Task<object> GetAllProject(string? keyword, string? category)
        {
            var respone = await _productCategoryRepository.GetAllProject(keyword, category);
            return respone;
        }

        [HttpPut]
        public async Task<object> UpdateProductDetail([FromBody] ProductDetailDto productDetail)
        {
            var respone = await _productCategoryRepository.UpdateProductDetailAsync(productDetail);
            return respone;
        }

        [HttpGet("{productId}/{categoryId}")]
        public async Task<object> GetProductDetailById(Guid productId, Guid categoryId)
        {
            var respone = await _productCategoryRepository.GetProductDetailByIdAsync(productId, categoryId);
            return respone;
        }
    }
}
