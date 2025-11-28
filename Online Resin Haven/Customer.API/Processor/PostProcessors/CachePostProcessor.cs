using FastEndpoints;
using ORH.Infrastructure.Common;
using ORH.Application.Interface.Shared.Caching;

namespace Customer.API.Processor.PostProcessors
{
    public class CachePostProcessor<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
    {
        private readonly ICacheService _cache;
        private readonly int? _cacheDuration;

        public CachePostProcessor(IConfiguration configuration, ICacheService cache)
        {
            _cache = cache;
            _cacheDuration = int.TryParse(configuration["CacheConfiguration:Redis:Expiration"], out var seconds) ? seconds : null;
        }

        public async Task PostProcessAsync(IPostProcessorContext<TRequest, TResponse> context, CancellationToken ct)
        {
            var key = CacheHelper.GenerateKey(context.HttpContext.Request);

            if (context.HttpContext.Response != null && context.HttpContext.Response.StatusCode == 200)
            {
                TimeSpan? expiration = _cacheDuration != null ? TimeSpan.FromSeconds(_cacheDuration.Value) : null;
                await _cache.SetAsync(key, context.Response!, expiration);
            }
        }
    }
}
