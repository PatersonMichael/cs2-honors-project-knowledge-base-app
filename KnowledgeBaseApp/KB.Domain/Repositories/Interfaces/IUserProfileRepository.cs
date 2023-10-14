using KB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KB.Domain.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        // Get users async
        Task<IEnumerable<UserProfile>> GetUserProfilesAsync();

        // Get a single user async
        Task<UserProfile> GetUserProfileAsync(int id);

        // Post (create) a user async
        Task<UserProfile> PostUserProfileAsync(UserProfile userProfile);

        // Put (update) a user async
        Task<UserProfile> PutUserProfileAsync(int id, UserProfile userProfile);

        // Delete a user async
        Task DeleteUserProfileAsync(int id);    }
}
