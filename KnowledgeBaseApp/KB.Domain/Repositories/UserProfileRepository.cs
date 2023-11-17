using KB.Domain.Models;
using KB.Domain.Repositories.Interfaces;
using KB.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using BCrypt.Net;

namespace KB.Domain.Repositories
{
    /// <summary>
    ///  User Profile
    /// </summary>
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

        /// <summary>
        /// Returns a collection of User Profiles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserProfile>> GetUserProfilesAsync()
        {
            return await _context.UserProfiles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<UserProfile> GetUserProfileAsync(int id)
        {
            // Log this procedure since it may throw an exception
            _logger.LogInformation("Begin GetUserProfileAsync from UserProfileRepository");

            try
            {
                var UserProfile = await _context.UserProfiles
                    //.Include(x => x.Authors)
                    .AsNoTracking()
                    .SingleAsync(x => x.UserProfileId == id);

                return UserProfile;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException($"User with Id: {id} was not found.");
            }
        }

        public async Task<UserProfile> PostUserProfileAsync(UserProfile userProfile)
        {
            _logger.LogInformation("Begin PostUserProfileAsync from UserProfileRepository");

            // hash and salt password
            userProfile.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userProfile.Password);

            await _context.UserProfiles.AddAsync(userProfile);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // userProfile may already exist in DB, need to check for that
                if (UserProfileExists(userProfile.UserProfileId))
                {
                    throw new ConflictException("User already exists");
                }

                if (UserEmailExists(userProfile.Email))
                {
                    throw new ConflictException("Email already used");
                }

                if (UserNameTagExists(userProfile.Nametag))
                {
                    throw new ConflictException("Nametag already taken");
                }

                throw;
            }

            return userProfile;
        }

        public async Task<UserProfile> PutUserProfileAsync(int id, UserProfile userProfile)
        {
            _logger.LogInformation("Begin PutUserProfileAsync from UserProfileRepository");

            // guard against incorrect id input
            if (id != userProfile.UserProfileId)
            {
                return null;
            }

            // Set EF tracker to alter state of current entity
            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(userProfile.UserProfileId))
                {
                    throw new NotFoundException("User was not found.");
                }

                throw;
            }

            return userProfile;
        }

       
        public async Task<UserProfile> VerifyUserCredentialCombination(UserProfile user)
        {
            try
            {
                var userEntity = await _context.UserProfiles.SingleAsync(x => x.Email == user.Email);
                if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, userEntity.Password))
                {
                    return userEntity;
                }
            }
            catch (InvalidOperationException)
            {
                throw new BadRequestException("Invalid Credentials");
            }

            throw new BadRequestException("Invalid Credentials");
        }

        public async Task DeleteUserProfileAsync(int id)
        {
            _logger.LogInformation("Begin DeleteUserProfileAsync from UserProfileRespository");

            // find and assign user
            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullEntityException("UserProfile is Null");
            }
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.UserProfileId == id);
        }
        

        private bool UserEmailExists(string email)
        {
            return _context.UserProfiles.Any(e => e.Email == email);
        }

        private bool UserNameTagExists(string nameTag)
        {
            return _context.UserProfiles.Any(e => e.Nametag == nameTag);
        }
    }
}
