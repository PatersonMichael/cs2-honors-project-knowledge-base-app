using KB.Domain.Models;
using KB.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        // declare db context 
        private readonly KnowledgeBaseAppContext _context;

        // declare logger for logging activity
        private readonly ILogger<UserProfileRepository> _logger;

        public UserProfileRepository(KnowledgeBaseAppContext context, ILogger<UserProfileRepository> logger)
        {
            // If the parameters passed point to null values, throw an exception
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<UserProfile>> GetUserProfilesAsync()
        {

        }

        public async Task<UserProfile> GetUserProfileAsync(int id)
        {

        }

        public async Task<UserProfile> PostUserProfileAsync(UserProfile userProfile)
        {

        }

        public async Task<UserProfile> PutUserProfileAsync(int id, UserProfile userProfile)
        {

        }

        public async Task DeleteUserProfileAsync(int id)
        {

        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.UserProfileId == id);
        }
    }
}
