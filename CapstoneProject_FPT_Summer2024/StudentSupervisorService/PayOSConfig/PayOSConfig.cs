using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace StudentSupervisorService.PayOSConfig
{
    public class PayOSConfig
    {
        public readonly string CANCEL_API_URL = "/api/checkout/verify";
        public readonly string RETURN_API_URL = "/api/checkout/verify";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public readonly string ChecksumKey;

        public PayOSConfig(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            ChecksumKey = _configuration["Environment:PAYOS_CHECKSUM_KEY"];
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
