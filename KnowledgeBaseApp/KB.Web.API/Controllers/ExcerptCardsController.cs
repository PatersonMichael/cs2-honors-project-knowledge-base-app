using AutoMapper;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;
using ExcerptCardEntity = KB.Domain.Models.ExcerptCard;

namespace KB.Web.API.Controllers
{
    [Route("api/ExcerptCards")]
    [Produces("application/json")]
    [ApiController]
    public class ExcerptCardsController : ControllerBase
    {
        private readonly ILogger<ExcerptCardsController> _logger;
        private readonly IMapper _mapper;
        private readonly IExcerptCardRepository _excerptCardRepository;

        public ExcerptCardsController(ILogger<ExcerptCardsController> logger, IMapper mapper, IExcerptCardRepository excertCardRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _excerptCardRepository = excertCardRepository;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(typeof(List<ExcerptCard>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IList<ExcerptCard>> GetExcerptCardsAsync()
        {
            _logger.LogInformation("Begin GetExcerptCardsAsync");

            var excerptCardEntities = await _excerptCardRepository.GetExcerptCardsAsync();

            IList<ExcerptCard> excerptCards = new List<ExcerptCard>();

            excerptCards = _mapper.Map<List<ExcerptCard>>(excerptCardEntities);

            return excerptCards;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetExcerptCardAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetExcerptCardAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin GetExcerptCardAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            ExcerptCard excerptCard;

            var excerptCardEntity = await _excerptCardRepository.GetExcerptCardAsync(id);

            if (excerptCardEntity != null)
            {
                excerptCard = _mapper.Map<ExcerptCard>(excerptCardEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(excerptCard);
        }

        // POST 

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostExcerptCardAsync([FromBody] ExcerptCard excerptCard)
        {
            _logger.LogInformation("Begin PostExcerptCardAsync");

            var excerptCardEntity = _mapper.Map<ExcerptCardEntity>(excerptCard);

            excerptCardEntity = await _excerptCardRepository.PostExcerptCardAsync(excerptCardEntity);

            excerptCard = _mapper.Map<ExcerptCard>(excerptCardEntity);

            return CreatedAtAction(nameof(GetExcerptCardAsync), "ExcerptCards", new { id = excerptCard.ExcerptCardId },
                excerptCard);
        }

        // PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutExcerptCardAsync([FromRoute] int id, [FromBody] ExcerptCard excerptCard)
        {
            _logger.LogInformation("Begin PutExcerptCardAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            if (id != excerptCard.ExcerptCardId)
            {
                return BadRequest("id parameter does not match Excerpt Card id");
            }

            var excerptCardEntity = _mapper.Map<ExcerptCardEntity>(excerptCard);

            await _excerptCardRepository.PutExcerptCardAsync(id, excerptCardEntity);

            if (excerptCardEntity != null)
            {
                excerptCard = _mapper.Map<ExcerptCard>(excerptCardEntity);
            }

            return Ok(excerptCard);
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteExcerptCardAsync([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            await _excerptCardRepository.DeleteExcerptCardAsync(id);

            return NoContent();
        }
    }
}
