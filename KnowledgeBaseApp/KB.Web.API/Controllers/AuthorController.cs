//using AutoMapper;
//using KB.Common.Exceptions;
//using KB.Domain.Repositories.Interfaces;
//using KB.Web.API.DtoModels;
//using Microsoft.AspNetCore.Mvc;
//using AuthorEntity = KB.Domain.Models.Author;

//namespace KB.Web.API.Controllers
//{
//    [Route("api/Authors")]
//    [Produces("application/json")]
//    [ApiController]
//    public class AuthorController : ControllerBase
//    {
//        private readonly ILogger<AuthorController> _logger;
//        private readonly IMapper _mapper;
//        private readonly IAuthorRepository _authorRepository;

//        /// <summary>
//        /// Initializes new instance of <see cref="AuthorController"/>
//        /// </summary>
//        /// <param name="logger"></param>
//        /// <param name="mapper"></param>
//        /// <param name="authorRepository"></param>
//        /// <exception cref="ArgumentNullException"></exception>
//        public AuthorController(ILogger<AuthorController> logger, IMapper mapper, IAuthorRepository authorRepository)
//        {
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _mapper = mapper;
//            _authorRepository = authorRepository;
//        }

//        // GET api/Authors
//        /// <summary>
//        /// Get a list of authors
//        /// </summary>
//        /// <returns>List of authors</returns>
//        [HttpGet]
//        [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IList<Author>> GetAuthorsAsync()
//        {
//            _logger.LogInformation("Begin GetAuthorsAsync");

//            var authorEntities = await _authorRepository.GetAuthorsAsync();

//            IList<Author> authors = new List<Author>();

//            // if valid authors, map to dto
//            if (authorEntities != null)
//            {
//                authors = _mapper.Map<List<Author>>(authorEntities);
//            }

//            return authors;
//        }

//        [HttpGet("{id}")]
//        [ActionName(nameof(GetAuthorAsync))]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesDefaultResponseType]
//        public async Task<IActionResult> GetAuthorAsync([FromRoute] int id)
//        {
//            _logger.LogInformation("Begin GetAuthorAsync");

//            if (id == 0)
//            {
//                return BadRequest("Enter valid id");
//            }

//            Author author;
//            var authorEntity = await _authorRepository.GetAuthorAsync(id);

//            if (authorEntity != null)
//            {
//                author = _mapper.Map<Author>(authorEntity);
//            }
//            else
//            {
//                return NotFound();
//            }

//            return Ok(author);

//        }

//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        public async Task<IActionResult> PostAuthorAsync([FromBody] Author author)
//        {
//            _logger.LogInformation("Begin PostAuthorAsync");

//            AuthorEntity authorEntity = _mapper.Map<AuthorEntity>(author);

//            authorEntity = await _authorRepository.PostAuthorAsync(authorEntity);

//            author = _mapper.Map<Author>(authorEntity);

//            return CreatedAtAction(nameof(GetAuthorAsync), "Author", new { id = author.AuthorId }, author);

//        }

//        [HttpPut("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        public async Task<IActionResult> PutAuthorAsync([FromRoute] int id, [FromBody] Author author)
//        {
//            _logger.LogInformation("Begin PutAuthorAsync");

//            if (id == 0)
//            {
//                return BadRequest("id is needed");
//            }

//            AuthorEntity authorEntity = _mapper.Map<AuthorEntity>(author);
//            authorEntity = await _authorRepository.PutAuthorAsync(id, authorEntity);

//            if (authorEntity != null)
//            {
//                _mapper.Map<Author>(authorEntity);
//            }

//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] int id)
//        {
//            _logger.LogInformation("Begin DeleteAuthorAsync");

//            if (id == 0)
//            {
//                return BadRequest("id is needed");
//            }

//            await _authorRepository.DeleteAuthorAsync(id);

//            return NoContent();
//        }
//    }
//}
