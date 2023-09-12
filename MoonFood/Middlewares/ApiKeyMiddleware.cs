

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, string apiKey)
    {
        _next = next;
        _apiKey = apiKey;
    }

    public async Task Invoke(HttpContext context)
    {
        // Kiểm tra xem API key có trong header "Mykey" không
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyHeader)) 
        {
            context.Response.StatusCode = 401; 
            await context.Response.WriteAsync("API key is missing.");
            return;
        }

        // So sánh API key trong header với API key đã cấp
        if (!string.Equals(apiKeyHeader, _apiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = 401; 
            await context.Response.WriteAsync("Invalid API key.");
            return;
        }

        // Nếu API key hợp lệ, tiếp tục xử lý yêu cầu
        await _next(context);
    }
}

