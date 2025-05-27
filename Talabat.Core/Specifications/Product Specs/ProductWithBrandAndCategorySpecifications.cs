using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications:BaseSpecifications<Product>
    {


        //this constructor will be used for creating object that will be used to Get All Products
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) :base(P =>
              (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
          (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value)&&
        (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)
        )
        {
            addIncludes();
            if( !string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                        case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
                
            }
            else 
                AddOrderBy(P => P.Name);

            // totalProducts =18
            // pageSize = 5
            // pageIndex = 3

            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        }


        //this constructor will be used for creating object that will be used to Get  Product By ID 

        public ProductWithBrandAndCategorySpecifications(int id):base(P => P.ID == id)
        {
            addIncludes();
        }
        private void addIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
