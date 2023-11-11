using AutoMapper;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;
using KeywordEntity = KB.Domain.Models.Keyword;

namespace KB.Web.API.Controllers
{
    [Route("api/Keywords")]
    [Produces("application/json")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly ILogger<KeywordsController> _logger;
        private readonly IMapper _mapper;
        private readonly IKeywordRepository _keywordRepository;

        public KeywordsController(ILogger<KeywordsController> logger, IMapper mapper, IKeywordRepository keywordRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _keywordRepository = keywordRepository;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Keyword>> GetKeywordsAsync()
        {
            _logger.LogInformation("Begin GetKeywordsAsync");

            IList<Keyword> keywords = new List<Keyword>();
            var keywordEntities = await _keywordRepository.GetKeywordsAsync();

            if (keywordEntities != null)
            {
                keywords = _mapper.Map<List<Keyword>>(keywordEntities);
            }
            
            return keywords;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetKeywordAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetKeywordAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin GetKeywordAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            Keyword keyword;
            var keywordEntity = await _keywordRepository.GetKeywordAsync(id);

            if (keywordEntity != null)
            {
                keyword = _mapper.Map<Keyword>(keywordEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(keyword);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostKeywordAsync([FromBody] Keyword keyword)
        {
            _logger.LogInformation("Begin PostKeywordAsync");

            KeywordEntity keywordEntity = _mapper.Map<KeywordEntity>(keyword);

            keywordEntity = await _keywordRepository.PostKeywordAsync(keywordEntity);

            keyword = _mapper.Map<Keyword>(keywordEntity);

            return CreatedAtAction(nameof(GetKeywordAsync), "Keywords", new { id = keyword.KeywordId }, keyword);
        }

        // PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutKeywordAsync([FromRoute] int id, [FromBody] Keyword keyword)
        {
            _logger.LogInformation("Begin PutKeywordAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            if (id != keyword.KeywordId)
            {
                return BadRequest("id parameter does not match Keyword id");
            }

            KeywordEntity keywordEntity = _mapper.Map<KeywordEntity>(keyword);

            keywordEntity = await _keywordRepository.PutKeywordAsync(id, keywordEntity);

            if (keywordEntity != null)
            {
                keyword = _mapper.Map<Keyword>(keywordEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(keyword);
        }


        // DELETE

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteKeywordAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin DeleteKeywordAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            await _keywordRepository.DeleteKeywordAsync(id);

            return NoContent();
        }
    }
}
