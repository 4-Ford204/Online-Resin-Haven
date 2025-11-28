using MediatR;
using Microsoft.AspNetCore.Mvc;
using ORH.Application.UseCase.Product.Queries;
using Product.API.Abstraction.Controllers;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        public ProductController(ILogger<BaseController> logger, IMediator mediator) : base(logger, mediator) { }

        [HttpGet, Route("get-all-products")]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new GetProductsQuery());
            return HandleResultAsync(query);
        }
    }
}
