using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class ErrorController :BaseController
    {

        [HttpGet("NotFound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }


        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("this is Bad Request");
        }


        [HttpGet("UnAuthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized();
        }


        [HttpGet("ServerError")]
        public IActionResult GetServerError()
        {
            throw new Exception("this is server error");
        }
    }
}
