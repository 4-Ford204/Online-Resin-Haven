using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.API.Abstraction.Controllers;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : BaseController
    {
        public CharacterController(ILogger<BaseController> logger, IMediator mediator) : base(logger, mediator) { }

        [HttpGet, Route("get-all-characters")]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new ORH.Application.UseCase.Character.Queries.GetCharactersQuery());
            return HandleResultAsync(query);
        }
    }
}
