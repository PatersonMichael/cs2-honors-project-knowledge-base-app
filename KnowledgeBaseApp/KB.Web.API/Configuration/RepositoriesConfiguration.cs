using KB.Domain.Repositories;
using KB.Domain.Repositories.Interfaces;

namespace KB.Web.API.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            //services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ISourceMaterialRepository, SourceMaterialRepository>();
            services.AddScoped<ICitationRepository, CitationRepository>();
        }
        
    }
}
