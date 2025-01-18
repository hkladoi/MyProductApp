using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class ProductDetail
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
    }
}
