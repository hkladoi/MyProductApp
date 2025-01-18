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
        private readonly IProductDetails _productCategoryRepository;

        public ProductDetailsController(IProductDetails productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductDetail([FromBody] ProductDetail productDetail)
        {
            await _productCategoryRepository.AddProductDetailAsync(productDetail);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(Guid productId, Guid categoryId)
        {
            await _productCategoryRepository.DeleteProductDetailAsync(productId, categoryId);
            return NoContent();
        }
    }
}
