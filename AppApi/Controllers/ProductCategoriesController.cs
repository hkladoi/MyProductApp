using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData.DbContexts;
using AppData.Models;
using AppData.IRepositories;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ICategoryRepos _categoryRepository;

        public ProductCategoriesController(ICategoryRepos categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<object> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return categories;
        }

        [HttpGet("{id}")]
        public async Task<object> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return category;
        }

        [HttpPost]
        public async Task<object> AddCategory([FromBody] Category category)
        {
            var response = await _categoryRepository.AddCategoryAsync(category);
            return response;
        }

        [HttpPut]
        public async Task<object> UpdateCategory([FromBody] Category category)
        {
            var response = await _categoryRepository.UpdateCategoryAsync(category);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteCategory(Guid id)
        {
            var response = await _categoryRepository.DeleteCategoryAsync(id);
            return response;
        }
    }
}
