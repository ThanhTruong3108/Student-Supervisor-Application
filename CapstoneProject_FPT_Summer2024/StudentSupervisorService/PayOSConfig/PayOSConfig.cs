using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.PayOSConfig
{
    public class PayOSConfig
    {
        public readonly string CANCEL_API_URL = "/api/checkout/history";
        public readonly string RETURN_API_URL = "/api/checkout/history";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PayOSConfig(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCancelUrl()
        {
            // Root context path
            var requestURL = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{requestURL.Scheme}://{requestURL.Host}{requestURL.PathBase}";
            return baseUrl + CANCEL_API_URL;
        }

        public string GetReturnUrl()
        {
            // Root context path
            var requestURL = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{requestURL.Scheme}://{requestURL.Host}{requestURL.PathBase}";
            return baseUrl + RETURN_API_URL;
        }
    }
}
