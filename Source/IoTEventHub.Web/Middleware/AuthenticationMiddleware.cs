using System;
using System.Threading.Tasks;
using IoTEventHub.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IoTEventHub.Web
{
    /// <summary>
    /// Middleware to authenticate with an api key header
    /// </summary>
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ApiConfiguration> _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="config">The configuration.</param>
        public AuthenticationMiddleware(RequestDelegate next, IOptions<ApiConfiguration> config)
        {
            _next = next;
            _config = config;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException($"{nameof(context)} cannot be null");
            }
                                    
            var apiKeyConfig = _config?.Value?.ApiKey;
            var apiKeyHeader = context.Request.Headers["apikey"];
            if (!string.IsNullOrWhiteSpace(apiKeyHeader) && !string.IsNullOrWhiteSpace(apiKeyConfig))
            {
                if (apiKeyConfig.Equals(apiKeyHeader, StringComparison.CurrentCultureIgnoreCase))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
            }
        }
    }
}
