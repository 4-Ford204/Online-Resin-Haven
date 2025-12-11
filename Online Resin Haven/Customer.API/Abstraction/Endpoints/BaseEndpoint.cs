using Ardalis.Result;
using FastEndpoints;
using FluentValidation.Results;
using MediatR;

namespace Customer.API.Abstraction.Endpoints
{
    public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
    {
        protected readonly IMediator _mediator;

        public BaseEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task HandleResultAsync(Result<TResponse> result, CancellationToken ct)
        {
            if (result.IsSuccess)
            {
                await SendOkAsync(result.Value, ct);
                return;
            }

            switch (result.Status)
            {
                case ResultStatus.Forbidden:
                    {
                        await SendForbiddenAsync(ct);
                        break;
                    }
                case ResultStatus.Unauthorized:
                    {
                        await SendUnauthorizedAsync(ct);
                        break;
                    }
                case ResultStatus.Invalid:
                    {
                        var validationFailures = result.ValidationErrors
                            .Select(error => new ValidationFailure(error.Identifier ?? string.Empty, error.ErrorMessage));

                        foreach (var failure in validationFailures)
                        {
                            AddError(failure);
                        }

                        await SendErrorsAsync(400, ct);
                        break;
                    }
                case ResultStatus.NotFound:
                    {
                        await SendNotFoundAsync(ct);
                        break;
                    }
                default:
                    {
                        var errors = result.Errors;

                        foreach (var error in errors)
                        {
                            AddError(error);
                        }

                        await SendErrorsAsync(500, ct);
                        break;
                    }
            }
        }
    }
}
