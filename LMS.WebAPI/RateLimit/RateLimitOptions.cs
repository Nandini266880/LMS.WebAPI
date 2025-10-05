namespace LMS.WebAPI.RateLimit
{
    public class RateLimitOptions
    {
        public int MaxRequests { get; set; } = 10;
        public TimeSpan Window { get; set; } = TimeSpan.FromMinutes(1);
    }
}
