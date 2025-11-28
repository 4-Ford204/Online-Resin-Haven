using MediatR;
using Microsoft.AspNetCore.Mvc;
using ORH.Application.UseCase.Line.Queries;
using Product.API.Abstraction.Controllers;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineController : BaseController
    {
        public LineController(ILogger<BaseController> logger, IMediator mediator) : base(logger, mediator) { }

        [HttpGet, Route("get-all-lines")]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new GetLinesQuery());
            return HandleResultAsync(query);
        }
    }
}
