using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryAplication.Activities;
using sosialClone;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= 
            HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult result<T>(result<T> data)
        {
            if (data == null) { return NotFound(); }
            if (data.data != null && data.success) { return Ok(data.data); }
            if (data.success && data.data == null) { return NotFound(); }
            return BadRequest(data.error);
        }                                                                                                    
    }
}
