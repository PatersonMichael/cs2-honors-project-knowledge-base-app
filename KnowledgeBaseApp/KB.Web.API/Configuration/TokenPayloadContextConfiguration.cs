using KB.Web.API.Middleware;

namespace KB.Web.API.Configuration
{
    public static class TokenPayloadContextConfiguration
    {
        public static void AddTokenPayloadContext(this IServiceCollection services)
        {
            services.AddScoped<TokenPayloadContext>();
        }
    }
}
