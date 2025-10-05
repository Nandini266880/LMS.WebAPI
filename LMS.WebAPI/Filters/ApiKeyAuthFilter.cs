using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.WebAPI.Filters
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly string _apiKey;
        private readonly string HeaderName = "Custom-ApiKey";
        public ApiKeyAuthFilter(IConfiguration config)
        {
            _apiKey = config["ApiSettings:ApiKey"];
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var extractedKey))
            {
                context.Result = new UnauthorizedObjectResult("ApiKey doesn't exists.");
                return;
            }
            if(!_apiKey.Equals(extractedKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key.");
            }
        }
    }
}
