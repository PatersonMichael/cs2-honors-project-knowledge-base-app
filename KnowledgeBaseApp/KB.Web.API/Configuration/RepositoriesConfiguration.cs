﻿using KB.Domain.Repositories;
using KB.Domain.Repositories.Interfaces;

namespace KB.Web.API.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepsitories(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
        }
        
    }
}
