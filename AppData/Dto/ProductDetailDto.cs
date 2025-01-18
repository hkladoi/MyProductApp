using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Dto
{
    public class ProductDetailDto
    {
        public Guid? product_id { get; set; }
        public Guid? category_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public DateTime import_day { get; set; }
        public string category { get; set; }
    }
}
