using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brand;
        private readonly IGenericRepository<ProductCategory> _category;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brand,
            IGenericRepository<ProductCategory> category,
            IMapper mapper  )
        {
        _productRepo = productRepo;
            this._brand = brand;
            this._category = category;
            this.mapper = mapper;
        }

        // /api/Products
        [HttpGet]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
             
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams);


            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var CountSpec = new ProductWithFilterationForCountSpecification(specParams);
            var count = await _productRepo.GetCountAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize,count, data) );
        }
        // /api/Products/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _productRepo.GetWithSpecAsync(spec);

            if (product == null)
                return NotFound(new ApiResponse(404));


            return Ok(mapper.Map<Product, ProductToReturnDto>(product));

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _brand.GetAllAsync());
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            return Ok(await _category.GetAllAsync());
        }

    }
}
