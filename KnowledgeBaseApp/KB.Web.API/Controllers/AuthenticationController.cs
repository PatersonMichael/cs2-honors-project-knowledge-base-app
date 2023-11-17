using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using KB.Domain.Models;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;
using KB.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using UserProfileDto = KB.Web.API.DtoModels.UserProfile;
using UserProfileEntity = KB.Domain.Models.UserProfile;

namespace KB.Web.API.Controllers
{
    [Route("api/authentication")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        

            private readonly ILogger<AuthenticationController> _logger;
            private readonly IUserProfileRepository _userProfileRepository;
            private readonly IMapper _mapper;

            private static readonly TimeSpan TokenLifeSpan = TimeSpan.FromHours(2);

            public AuthenticationController(ILogger<AuthenticationController> logger, IUserProfileRepository repo, IMapper mapper)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _userProfileRepository = repo ?? throw new ArgumentNullException(nameof(repo));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

        // Receive http request with login credentials
        // check db for email

        // if email exists
        // check db for email/password combo
        // if email/password combo exists
        // return 200 with jwt
        // else
        // return 401 Unauthorized
        // else
        // return 401 Unauthorized
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyCredentials([FromBody] UserLoginCredentials credentials)
        {
            var userEntityCredentials = _mapper.Map<UserProfileEntity>(credentials);

            try
            {
                UserProfileEntity userProfileEntity =
                    await _userProfileRepository.VerifyUserCredentialCombination(userEntityCredentials);
                return Ok(GenerateToken(userProfileEntity));
            }
            catch (BadRequestException)
            {
                return BadRequest("Invalid Credentials");
            }

        }

        IActionResult GenerateToken(UserProfileEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("userProfileId", user.UserProfileId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifeSpan),
                Issuer = "pater/cs2honorsapp",
                Audience = "user",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);
        }
    }
}
