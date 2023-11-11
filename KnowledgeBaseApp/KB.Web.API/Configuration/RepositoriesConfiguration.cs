using KB.Domain.Repositories;
using KB.Domain.Repositories.Interfaces;

namespace KB.Web.API.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            // Registers new services for dependency injection
            // In this case, the repository implementations are injected as dependencies for their respective interfaces,
            // so that the web API can write to the repository interfaces instead of the implementations

            // Repositories
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<ISourceMaterialRepository, SourceMaterialRepository>();
            services.AddScoped<ICitationRepository, CitationRepository>();
            services.AddScoped<IKeywordRepository, KeywordRepository>();
            services.AddScoped<IExcerptCardRepository, ExcerptCardRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
        }
        
    }
}
