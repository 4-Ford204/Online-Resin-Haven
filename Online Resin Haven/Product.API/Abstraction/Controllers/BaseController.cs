using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Product.API.Abstraction.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly IMediator _mediator;

        public BaseController(ILogger<BaseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected IActionResult HandleResultAsync<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return result.Status switch
            {
                ResultStatus.Forbidden => Forbid(),
                ResultStatus.Unauthorized => Unauthorized(),
                ResultStatus.Invalid => BadRequest(new
                {
                    Errors = result.ValidationErrors.Select(error => new
                    {
                        Field = error.Identifier ?? string.Empty,
                        Message = error.ErrorMessage
                    })
                }),
                ResultStatus.NotFound => NotFound(),
                _ => StatusCode(500, new { result.Errors })
            };
        }
    }
}
