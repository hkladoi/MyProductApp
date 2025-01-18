using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Models;

namespace AppData.IRepositories
{
    public interface IProductDetails
    {
        Task AddProductDetailAsync(ProductDetail productDetail);
        Task DeleteProductDetailAsync(Guid productId, Guid categoryId);
    }
}
