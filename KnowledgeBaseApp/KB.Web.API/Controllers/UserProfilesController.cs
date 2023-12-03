using AutoMapper;
using KB.Common.Exceptions;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserProfileEntity = KB.Domain.Models.UserProfile;

namespace KB.Web.API.Controllers
{
    [Route("api/UserProfiles")]
    [Produces("application/json")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly ILogger<UserProfilesController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserProfileRepository _userProfileRepository;

        // TODO move user GET requests to different model as to not pass on sensitive information

        public UserProfilesController(ILogger<UserProfilesController> logger, IMapper mapper, IUserProfileRepository userProfileRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserProfile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IList<UserProfileDto>> GetUserProfilesAsync()
        {
            _logger.LogInformation("Begin GetUserProfilesAsync");

            var userProfileEntities = await _userProfileRepository.GetUserProfilesAsync();

            IList<UserProfileDto> userProfiles = new List<UserProfileDto>();

            // If there are valid userProfiles in the repository, go ahead and map to dto
            if (userProfileEntities != null)
            {
                userProfiles = _mapper.Map<List<UserProfileDto>>(userProfileEntities);
            }

            return userProfiles;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ActionName(nameof(GetUserProfileAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserProfileAsync([FromRoute]int id)
        {
            // add ability to get id from token if used instead of route
            int userId = int.Parse(HttpContext.Items["userProfileId"].ToString());

            _logger.LogInformation("Begin GetUserProfileAsync");

            if (id == 0)
            {
                return BadRequest("id is required");
            }

            if (id == -1)
            {
                id = userId;
            }

            UserProfileDto userProfileDto;
            var userProfileEntity = await _userProfileRepository.GetUserProfileAsync(id);

            if (userProfileEntity != null)
            {
                userProfileDto = _mapper.Map<UserProfileDto>(userProfileEntity);
            }
            else
            {
                //throw new NotFoundException("User Not Found");
                return NotFound();
            }

            return Ok(userProfileDto);
        }

        // Post new User
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostUserProfileAsync([FromBody] UserProfile userProfile)
        {
            _logger.LogInformation("Begin PostUserProfileAsync");

            UserProfileEntity userProfileEntity = _mapper.Map<UserProfileEntity>(userProfile);

            userProfileEntity = await _userProfileRepository.PostUserProfileAsync(userProfileEntity);

            userProfile = _mapper.Map<UserProfile>(userProfileEntity);

            return CreatedAtAction(nameof(GetUserProfileAsync), "UserProfiles", new { id = userProfile.UserProfileId }, userProfile);
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUserProfileAsync([FromRoute] int id, [FromBody] UserProfile userProfile)
        {
            _logger.LogInformation("Begin PutUserProfileAsync");
            
            // what kind of problems could occur when asking to update a user profile?

            // User doesn't exist in DB
            // Invalid data
            // id is invalid
            if (id != userProfile.UserProfileId)
            {
                return BadRequest();
            }

            UserProfileEntity userProfileEntity = _mapper.Map<UserProfileEntity>(userProfile);

            userProfileEntity = await _userProfileRepository.PutUserProfileAsync(id, userProfileEntity);


            if (userProfile != null)
            {
                userProfile = _mapper.Map<UserProfile>(userProfileEntity);
            }

            return Ok(userProfile); 
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserProfileAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin DeleteUserProfileAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            await _userProfileRepository.DeleteUserProfileAsync(id);

            return NoContent();
        }


    }
}
