using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ImportDate { get; set; }
        public int ProductCount { get; set; }
    }
}
