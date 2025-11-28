using Customer.API.Abstraction.Endpoints;
using Customer.API.Processor.PostProcessors;
using Customer.API.Processor.PreProcessors;
using MediatR;
using ORH.Application.UseCase.Customer.Queries;

namespace Customer.API.Endpoints
{
    public class SearchCustomersEndpoint : BaseEndpoint<SearchCustomersRequest, List<SearchCustomersResponse>>
    {
        public SearchCustomersEndpoint(IMediator mediator) : base(mediator) { }

        public override void Configure()
        {
            Get("/customer/search");
            AllowAnonymous();
            PreProcessor<CachePreProcessor<SearchCustomersRequest>>();
            PostProcessor<CachePostProcessor<SearchCustomersRequest, List<SearchCustomersResponse>>>();
        }

        public override async Task HandleAsync(SearchCustomersRequest request, CancellationToken ct)
        {
            var query = new SearchCustomersQuery(request);
            var result = await _mediator.Send(query, ct);

            await HandleResultAsync(result, ct);
        }
    }
}
