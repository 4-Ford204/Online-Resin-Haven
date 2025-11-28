using MediatR;
using Microsoft.AspNetCore.Mvc;
using ORH.Application.UseCase.Studio.Queries;
using Product.API.Abstraction.Controllers;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudioController : BaseController
    {
        public StudioController(ILogger<BaseController> logger, IMediator mediator) : base(logger, mediator) { }

        [HttpGet, Route("get-all-studios")]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new GetStudiosQuery());
            return HandleResultAsync(query);
        }
    }
}
