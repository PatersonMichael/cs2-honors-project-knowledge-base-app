using AutoMapper;
using AutoMapper.Configuration.Conventions;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using SourceMaterialDto = KB.Web.API.DtoModels.SourceMaterial;
using SourceMaterialEntity = KB.Domain.Models.SourceMaterial;

namespace KB.Web.API.Controllers
{
    [Route("api/SourceMaterials")]
    [Produces("application/json")]
    [ApiController]
    public class SourceMaterialsController : ControllerBase
    {
        private readonly ILogger<SourceMaterialsController> _logger;
        private readonly IMapper _mapper;
        private readonly ISourceMaterialRepository _sourceMaterialRepository;

        public SourceMaterialsController(ILogger<SourceMaterialsController> logger, IMapper mapper, ISourceMaterialRepository sourceMaterialRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _sourceMaterialRepository = sourceMaterialRepository;
        }

        // GET
        /// <summary>
        /// Get a list of Source Materials
        /// </summary>
        /// <returns>List of SourceMaterials</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<SourceMaterial>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IList<SourceMaterial>> GetSourceMaterialsAsync()
        {
            _logger.LogInformation("Begin GetSourceMaterialsAsync");

            var sourceMaterialEntities = await _sourceMaterialRepository.GetSourceMaterialsAsync();

            IList<SourceMaterialDto> sourceMaterials = new List<SourceMaterialDto>();

            if (sourceMaterialEntities != null)
            {
                sourceMaterials = _mapper.Map<List<SourceMaterialDto>>(sourceMaterialEntities);
            }

            return sourceMaterials;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetSourceMaterialAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSourceMaterialAsync([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest("must include valid id");
            }

            SourceMaterialDto sourceMaterial;
            var sourceMaterialEntity = await _sourceMaterialRepository.GetSourceMaterialAsync(id);

            if (sourceMaterialEntity != null)
            {
                sourceMaterial = _mapper.Map<SourceMaterialDto>(sourceMaterialEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(sourceMaterial);
        }
        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostSourceMaterialAsync([FromBody] SourceMaterialDto sourceMaterial)
        {
            _logger.LogInformation("Begin PostSourceMaterialAsync");

            SourceMaterialEntity sourceMaterialEntity = _mapper.Map<SourceMaterialEntity>(sourceMaterial);

            sourceMaterialEntity = await _sourceMaterialRepository.PostSourceMaterialAsync(sourceMaterialEntity);

            sourceMaterial = _mapper.Map<SourceMaterialDto>(sourceMaterialEntity);

            return CreatedAtAction(nameof(GetSourceMaterialAsync), "SourceMaterials",
                new { id = sourceMaterialEntity.SourceMaterialId }, sourceMaterial);
        }

        // PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutSourceMaterialAsync([FromRoute] int id,
            [FromBody] SourceMaterialDto sourceMaterial)
        {
            _logger.LogInformation("Begin PutSourceMaterialAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            SourceMaterialEntity sourceMaterialEntity = _mapper.Map<SourceMaterialEntity>(sourceMaterial);
            sourceMaterialEntity = await _sourceMaterialRepository.PutSourceMaterialAsync(id, sourceMaterialEntity);

            if (sourceMaterialEntity != null)
            {
                _mapper.Map<SourceMaterialDto>(sourceMaterialEntity);
            }

            return Ok();

        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSourceMaterialAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin DeleteSourceMaterialAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            await _sourceMaterialRepository.DeleteSourceMaterialAsync(id);

            return NoContent();
        }
    }
}
