using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //[ForeignKey(nameof(Product.Brand))] //Foreign Key
        public int BrandId { get; set; } //Foreign Key => ProductBrand

        public ProductBrand Brand  { get; set; } //Navigational Property One
        public int CategoryId { get; set; } //Foreign Key => ProductCategory
        public ProductCategory Category { get; set; } //Navigational Property One

    }
}
