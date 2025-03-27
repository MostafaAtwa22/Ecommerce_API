using Ecommerce.API.Errors;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.API.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly ApplicationDbContext _context;

        public BuggyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(4000);

            if (thing is null)
                return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("testAuth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "Secret Stuff";
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(4000);

            var thingToReturn = thing!.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetNotBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
    }
}
