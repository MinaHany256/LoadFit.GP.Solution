using LoadFit.APIs.Errors;
using LoadFit.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoadFit.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Vehicles.Find(100);
            if(product == null) 
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Vehicles.Find(100);
            var productDto = product.ToString();
          
            return Ok(productDto);
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]   // api/Buggy/badrequest/five
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }


    }
}
