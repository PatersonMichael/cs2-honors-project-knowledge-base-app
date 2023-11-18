using System.IdentityModel.Tokens.Jwt;
using KB.Web.API.Middleware;

namespace KB.Web.API.Middleware
{
    public class TokenPayloadContext : IMiddleware
    {
        /* Middleware requirements:
         *  A public constructor with a parameter of type RequestDelegate.
            A public method named Invoke or InvokeAsync. This method must:
            Return a Task.
            Accept a first parameter of type HttpContext.
         *
         */

        //private readonly RequestDelegate _next;
        private readonly ILogger<TokenPayloadContext> _logger;

        public TokenPayloadContext(ILogger<TokenPayloadContext> logger)
        {
            //next = _next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Begin InvokeTokenPayloadContext");
            // Get token from Headers[Authorization], remember to split it from Bearer in string, and select the jwt (second element)
            if (context.Request.Headers["Authorization"].ToString() != null && context.Request.Headers["Authorization"].ToString() != "")
            {
                var token = context.Request.Headers["Authorization"].ToString().Split(' ')[1];
                if (!string.IsNullOrEmpty(token))
                {
                    var jwtToken = new JwtSecurityToken(token);

                    context.Items["userProfileId"] = jwtToken.Payload["userProfileId"].ToString(); // store token payload in context items

                }

            }

            await next(context);
        }


    }
}

public static class TokenPayloadMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenPayloadContext(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenPayloadContext>();
    }
}