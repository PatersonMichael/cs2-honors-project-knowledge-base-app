using AutoMapper;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;

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

        public UserProfilesController(ILogger<UserProfilesController> logger, IMapper mapper, IUserProfileRepository userProfileRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserProfileDto>), StatusCodes.Status200OK)]
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

        [HttpGet("{id}")]
        [ActionName(nameof(GetUserProfileAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserProfileAsync(int id)
        {
            _logger.LogInformation("Begin GetUserProfileAsync");

            if (id == 0)
            {
                return BadRequest("id is required");
            }

            UserProfileDto userProfileDto;
            var userProfileEntity = await _userProfileRepository.GetUserProfileAsync(id);

            if (userProfileEntity != null)
            {
                userProfileDto = _mapper.Map<UserProfileDto>(userProfileEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(userProfileDto);
        }


    }
}
