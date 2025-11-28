using FastEndpoints;
using ORH.Infrastructure.Common;
using ORH.Application.Interface.Shared.Caching;

namespace Customer.API.Processor.PreProcessors
{
    public class CachePreProcessor<TRequest> : IPreProcessor<TRequest>
    {
        private readonly ICacheService _cache;

        public CachePreProcessor(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task PreProcessAsync(IPreProcessorContext<TRequest> context, CancellationToken ct)
        {
            var httpContext = context.HttpContext;
            var key = CacheHelper.GenerateKey(httpContext.Request);
            var response = await _cache.GetAsync(key);

            if (!string.IsNullOrEmpty(response))
            {
                context.HttpContext.Response.StatusCode = 200;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(response, ct);
            }
        }
    }
}
