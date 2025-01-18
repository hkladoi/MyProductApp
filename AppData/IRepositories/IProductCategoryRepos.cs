using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepositories
{
    public interface IProductCategoryRepos
    {
        Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
        Task<ProductCategory?> GetCategoryByIdAsync(Guid id);
        Task AddCategoryAsync(ProductCategory category);
        Task UpdateCategoryAsync(ProductCategory category);
        Task DeleteCategoryAsync(Guid id);

    }
}
