using AutoMapper;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;
using CitationEntity = KB.Domain.Models.Citation;

namespace KB.Web.API.Controllers
{
    [Route("api/Citations")]
    [Produces("application/json")]
    [ApiController]
    public class CitationsController : ControllerBase
    {
        private readonly ILogger<CitationsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICitationRepository _citationRepository;

        public CitationsController(ILogger<CitationsController> logger, IMapper mapper, ICitationRepository citationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _citationRepository = citationRepository;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IList<Citation>> GetCitationsAsync()
        {
            _logger.LogInformation("Begin GetCitationsAsync");

            var citationEntities = await _citationRepository.GetCitationsAsync();

            IList<Citation> citations = new List<Citation>();

            if (citationEntities != null)
            {
                citations = _mapper.Map<List<Citation>>(citationEntities);
            }
            
            return citations;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetCitationAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCitationAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin GetCitationAsync");

            if (id == 0)
            {
                return BadRequest("id is required");
            }

            Citation citationDto;
            var citationEntity = await _citationRepository.GetCitationAsync(id);

            if (citationEntity != null)
            {
                citationDto = _mapper.Map<Citation>(citationEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(citationDto);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostCitationAsync([FromBody] Citation citation)
        {
            CitationEntity citationEntity = _mapper.Map<CitationEntity>(citation);

            citationEntity = await _citationRepository.PostCitationAsync(citationEntity);

            citation = _mapper.Map<Citation>(citationEntity);

            return CreatedAtAction(nameof(GetCitationAsync), "Citations", new { id = citation.CitationId }, citation);
        }

        // PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutCitationAsync([FromRoute] int id, [FromBody] Citation citation)
        {
            _logger.LogInformation("Begin PutCitationAsync");
            // if for some reason id doesn't match, guard against that
            if (id != citation.CitationId)
            {
                return BadRequest("Id parameter does not match citation id");
            }

            // set up a domain entity from the incoming citation dto
            CitationEntity citationEntity = _mapper.Map<CitationEntity>(citation);

            // PUT that entity into the db
            citationEntity = await _citationRepository.PutCitationAsync(id, citationEntity);

            if (citationEntity != null)
            {
                // populate dto with new entity from db
                citation = _mapper.Map<Citation>(citationEntity);
            }

            return Ok(citation);
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCitationAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin DeleteCitationAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            await _citationRepository.DeleteCitationAsync(id);

            return NoContent();
        }
    }
}
