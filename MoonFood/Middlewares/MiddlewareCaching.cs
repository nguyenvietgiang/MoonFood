using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MoonFood.Middlewares
{
    public class MiddlewareCaching : IMiddleware
    {
        private readonly IMemoryCache _cache;

        public MiddlewareCaching(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var cacheKey = context.Request.Path + context.Request.QueryString;
            // Kiểm tra xem kết quả có trong cache hay không
            if (_cache.TryGetValue(cacheKey, out var cachedResponse))
            {
                // Nếu có trong cache, trả về kết quả từ cache
                context.Response.ContentType = "application/json"; 
                await context.Response.WriteAsync(cachedResponse.ToString());
                return;
            }
            using (var responseStream = new MemoryStream())
            {
                var originalBodyStream = context.Response.Body;
                context.Response.Body = responseStream;
                await next(context);
                responseStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(responseStream).ReadToEndAsync();
                _cache.Set(cacheKey, responseBody, TimeSpan.FromMinutes(2)); // Cache với thời gian 2 phút

                responseStream.Seek(0, SeekOrigin.Begin);
                await responseStream.CopyToAsync(originalBodyStream);
            }
        }
    }
}

