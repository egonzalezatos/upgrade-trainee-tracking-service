using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Upgrade.TraineeTracking.Shared.RedisUtils;

namespace Upgrade.TraineeTracking.Security.Services
{
    public class CacheableTokenProvider : TokenProvider
    {
        private readonly IDistributedCache _cache;

        public CacheableTokenProvider(IConfiguration configuration, IHttpClientFactory httpClientFactory, IDistributedCache cache) : base(configuration, httpClientFactory)
        {
            _cache = cache;
        }

        public override async Task<string> GetTokenAsync()
        {
            string cacheKey = RedisKeyBuilder.Key(
                _identityServer.BaseAddress!.AbsoluteUri,
                nameof(CacheableTokenProvider), 
                nameof(GetTokenAsync), string.Empty);

            byte[] value = await _cache.GetAsync(cacheKey);
            if (value is not null)
            {
                Token = ByteConverter.Byte2Array<string>(value);
                if (ValidateToken(Token))
                    return Token;
            }

            Token = await base.GetTokenAsync();
            Console.Out.WriteLine($"Updating cache for {cacheKey}");
            await _cache.SetAsync(cacheKey, ByteConverter.Array2Byte(Token));
            return Token;
        }
    }
}