using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Filters
{
    public class ApiKeyAuthAttribute : TypeFilterAttribute
    {
        public ApiKeyAuthAttribute() : base(typeof(ApiKeyAuthFilter)) { }
  
    }
}
