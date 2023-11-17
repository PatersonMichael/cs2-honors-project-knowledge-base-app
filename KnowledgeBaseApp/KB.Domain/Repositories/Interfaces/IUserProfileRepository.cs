using KB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Common.Exceptions;

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
        Task DeleteUserProfileAsync(int id);

        /// <summary>
        /// Verifies provided user Email and Password by checking for their presence within a single user record in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns a full UserProfile that matches the provided credentials.</returns>
        /// <exception cref="BadRequestException">thrown if no user was found with the provided credentials.</exception>
        Task<UserProfile> VerifyUserCredentialCombination(UserProfile user);

    }
}
