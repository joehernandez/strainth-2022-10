
// TODO: DELETE THIS CONTROLLER
namespace Strainth.Api.Controllers
{
    public class BuggyController : StrainthApiBaseController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFoundRequest()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            var newProbDetail = new ProblemDetails { Title = "This was not a good request - shame on you." };
            return BadRequest(newProbDetail);
        }

        [HttpGet("not-authorized")]
        public ActionResult GetNotAuthorizedRequest()
        {
            return Unauthorized();
        }

        [HttpGet("validation-error")]
        public ActionResult GetValidationErrorRequest()
        {
            ModelState.AddModelError("Problem1", "This is the first error");
            ModelState.AddModelError("Problem2", "This is the second error");

            return ValidationProblem();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
    }
}