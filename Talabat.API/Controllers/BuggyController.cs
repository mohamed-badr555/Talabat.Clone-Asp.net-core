using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly StoreDbContext _dbContext;

        public BuggyController(StoreDbContext dbContext)
        {
         _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var product = _dbContext.Products.Find(100);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var productToReturn = product.ToString();
            return Ok(productToReturn);
        }

    }
}
