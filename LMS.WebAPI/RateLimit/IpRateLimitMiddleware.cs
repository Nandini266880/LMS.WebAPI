using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace LMS.WebAPI.RateLimit
{
    //references1 - https://claudiobernasconi.ch/blog/how-to-use-rate-limiting-in-asp-dotnet-core-webapi/
    //references2 - https://github.com/luizeugeniob/rate-limit/blob/main/RateLimit/
    //references3 - https://medium.com/lodgify-technology-blog/implementing-rate-limit-with-custom-middleware-in-net-9a471aaacd10
    public class IpRateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IpRateLimitMiddleware> _logger;
        private readonly RateLimitOptions _options;
        private readonly ConcurrentDictionary<string, Queue<DateTimeOffset>> _store = new();

        public IpRateLimitMiddleware(RequestDelegate next, ILogger<IpRateLimitMiddleware> logger, IOptions<RateLimitOptions> ops)
        {
            _logger = logger;
            _next = next;
            _options = ops.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = GetClientIp(context);
            var now = DateTimeOffset.UtcNow;

            var queue = _store.GetOrAdd(ip, _ => new Queue<DateTimeOffset>());

            lock(queue)
            {
                //if window time exceeded then remove all requests
                while(queue.Count > 0 && now - queue.Peek() > _options.Window)
                {
                    queue.Dequeue();
                }

                //checking if requests exceeded
                if(queue.Count >= _options.MaxRequests)
                {
                    var earliest = queue.Peek();
                    var retryAfter = (earliest+_options.Window-now).TotalSeconds;
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.Headers["Retry-After"] = Math.Ceiling(retryAfter).ToString();
                    _logger.LogWarning("Rate limit exceeded for {IP}. Allowed {Max} per {Window}.", 
                        ip, _options.MaxRequests, _options.Window);
                    return;
                }
                queue.Enqueue(now);
            }

            await _next(context);
        }

        public static string GetClientIp(HttpContext context)
        {
            //forwarded by a proxy 
            var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                // multiple IPs in the header
                var ip = forwardedHeader.Split(',').First().Trim();
                return ip.ToString();
            }
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
