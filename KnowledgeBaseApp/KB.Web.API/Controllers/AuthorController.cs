using AutoMapper;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace KB.Web.API.Controllers
{
    [Route("api/Authors")]
    [Produces("application/json")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(ILogger<AuthorController> logger, IMapper mapper, IAuthorRepository authorRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IList<Author>> GetAuthorsAsync()
        {
            _logger.LogInformation("Begin GetAuthorsAsync");

            var authorEntities = await _authorRepository.GetAuthorsAsync();

            IList<Author> authors = new List<Author>();

            // if valid authors, map to dto
            if (authorEntities != null)
            {
                authors = _mapper.Map<List<Author>>(authorEntities);
            }

            return authors;
        }
    }
}
