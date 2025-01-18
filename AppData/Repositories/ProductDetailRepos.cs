using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.DbContexts;
using AppData.Dto;
using AppData.IRepositories;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppData.Repositories
{
    public class ProductDetailRepos : IProductDetailsRepos
    {
        private readonly AppDbContext _context;

        public ProductDetailRepos(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> AddProductDetailAsync(ProductDetailDto productDetail)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Name == productDetail.product_name);
                if (product == null)
                {
                    product = new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = productDetail.product_name,
                        Price = productDetail.product_price,
                        ImportDate = productDetail.import_day
                    };
                    _context.Products.Add(product);
                    _context.SaveChanges();
                }
                else
                {
                    return new ApiResponse
                    {
                        Status = 400,
                        Message = "Product is already exist"
                    };
                }
                var categories = productDetail.category.Split(", ");
                foreach (var category in categories)
                {
                    var categoryEntity = _context.Categories.FirstOrDefault(c => c.Name == category);
                    if (categoryEntity == null)
                    {
                        categoryEntity = new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = category
                        };
                        _context.Categories.Add(categoryEntity);
                        _context.SaveChanges();
                    }
                    var productDetailEntity = new ProductDetail
                    {
                        ProductId = product.Id,
                        ProductCategoryId = categoryEntity.Id
                    };
                    _context.ProductDetails.Add(productDetailEntity);
                    _context.SaveChanges();
                }
                return new ApiResponse
                {
                    Status = 200,
                    Message = "Add product detail successfully"
                };
            }
            catch (Exception)
            {
                return new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }

        public async Task<ApiResponse> DeleteProductDetailAsync(Guid productId, Guid categoryId)
        {
            try
            {
                var productDetail = await _context.ProductDetails.FirstOrDefaultAsync(x => x.ProductId == productId && x.ProductCategoryId == categoryId);
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                if (productDetail != null && product !=null)
                {
                    _context.Products.Remove(product);
                    _context.ProductDetails.Remove(productDetail);
                    await _context.SaveChangesAsync();
                    return new ApiResponse
                    {
                        Status = 200,
                        Message = "Delete product successfully"
                    };
                }
                return new ApiResponse
                {
                    Status = 404,
                    Message = "Product not found"
                };
            }
            catch (Exception)
            {
                return new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }

        public async Task<ApiResponse> GetAllProject(string? keyword, string? category)
        {
            try
            {
                var query = _context.ProductDetails.AsQueryable();

                // Lọc theo từ khóa (keyword) trong tên sản phẩm
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(pd => pd.Product.Name.Contains(keyword));
                }

                // Lọc theo danh mục (category)
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(pd => pd.Category.Name.Contains(category));
                }

                var productDetails = await query.ToListAsync();
                List<ProductDetailDto> response = new List<ProductDetailDto>();

                foreach (var productDetail in productDetails)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productDetail.ProductId);
                    var categories = await _context.Categories
                        .Where(x => x.ProductDetails.Any(pd => pd.ProductId == productDetail.ProductId))
                        .ToListAsync();

                    var categoryNames = categories.Select(c => c.Name).ToList();

                    var productDetailResponse = new ProductDetailDto
                    {
                        product_id = productDetail.ProductId,
                        category_id = productDetail.ProductCategoryId,
                        product_name = product.Name,
                        product_price = product.Price,
                        import_day = product.ImportDate,
                        category = string.Join(", ", categoryNames)
                    };

                    response.Add(productDetailResponse);
                }

                return new ApiResponse
                {
                    Status = 200,
                    Message = "Get all product detail successfully",
                    Data = response
                };
            }
            catch (Exception)
            {
                return new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                };
            }
        }

        public Task<ApiResponse> GetProductDetailByIdAsync(Guid productId, Guid categoryId)
        {
            try
            {
                var productDetail = _context.ProductDetails.FirstOrDefault(x => x.ProductId == productId && x.ProductCategoryId == categoryId);
                if (productDetail == null)
                {
                    return Task.FromResult(new ApiResponse
                    {
                        Status = 404,
                        Message = "Product not found"
                    });
                }
                var product = _context.Products.FirstOrDefault(x => x.Id == productId);
                var categories = _context.Categories
                    .Where(x => x.ProductDetails.Any(pd => pd.ProductId == productId))
                    .ToList();
                var categoryNames = categories.Select(c => c.Name).ToList();
                List<ProductDetailDto> productDetailDtos = new List<ProductDetailDto>();
                var productDetailResponse = new ProductDetailDto
                {
                    product_id = productDetail.ProductId,
                    category_id = productDetail.ProductCategoryId,
                    product_name = product.Name,
                    product_price = product.Price,
                    import_day = product.ImportDate,
                    category = string.Join(", ", categoryNames)
                };
                productDetailDtos.Add(productDetailResponse);
                return Task.FromResult(new ApiResponse
                {
                    Status = 200,
                    Message = "Get product detail successfully",
                    Data = productDetailDtos
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                });
            }
        }

        public Task<ApiResponse> UpdateProductDetailAsync(ProductDetailDto productDetail)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == productDetail.product_id);
                if (product == null)
                {
                    return Task.FromResult(new ApiResponse
                    {
                        Status = 404,
                        Message = "Product not found"
                    });
                }
                product.Name = productDetail.product_name;
                product.Price = productDetail.product_price;
                product.ImportDate = productDetail.import_day;
                _context.Products.Update(product);
                _context.SaveChanges();
                var categories = productDetail.category.Split(", ");
                foreach (var category in categories)
                {
                    var categoryEntity = _context.Categories.FirstOrDefault(c => c.Name == category);
                    if (categoryEntity == null)
                    {
                        return Task.FromResult(new ApiResponse
                        {
                            Status = 404,
                            Message = "Category not found"
                        });
                    }
                    var productDetailEntity = _context.ProductDetails.FirstOrDefault(pd => pd.ProductId == product.Id && pd.ProductCategoryId == categoryEntity.Id);
                    if (productDetailEntity == null)
                    {
                        productDetailEntity = new ProductDetail
                        {
                            ProductId = product.Id,
                            ProductCategoryId = categoryEntity.Id
                        };
                        _context.ProductDetails.Add(productDetailEntity);
                        _context.SaveChanges();
                    }
                }
                return Task.FromResult(new ApiResponse
                {
                    Status = 200,
                    Message = "Update product detail successfully"
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new ApiResponse
                {
                    Status = 500,
                    Message = "Internal server error"
                });
            }
        }
    }
}
