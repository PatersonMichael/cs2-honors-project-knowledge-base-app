using AutoMapper;
using KB.Domain.Repositories.Interfaces;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Mvc;
using NoteEntity = KB.Domain.Models.Note;

namespace KB.Web.API.Controllers
{
    [Route("api/Notes")]
    [Produces("application/json")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;
        private readonly IMapper _mapper;
        private readonly INoteRepository _NoteRepository;

        public NotesController(ILogger<NotesController> logger, IMapper mapper, INoteRepository noteRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _NoteRepository = noteRepository;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(typeof(List<Note>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IList<Note>> GetNotesAsync()
        {
            _logger.LogInformation("Begin GetNotesAsync");

            IList<Note> notes = new List<Note>();
            var noteEntities = await _NoteRepository.GetNotesAsync();

            if (noteEntities != null)
            {
                notes = _mapper.Map<List<Note>>(noteEntities);

            }

            return notes;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetNoteAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetNoteAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin GetNoteAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            Note note;
            var noteEntity = await _NoteRepository.GetNoteAsync(id);

            if (noteEntity != null)
            {
                note = _mapper.Map<Note>(noteEntity);
            }
            else
            {
                return NotFound();
            }

            return Ok(note);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostNoteAsync([FromBody] Note note)
        {
            _logger.LogInformation("Begin PostNoteAsync");

            var noteEntity = _mapper.Map<NoteEntity>(note);

            noteEntity = await _NoteRepository.PostNoteAsync(noteEntity);

            note = _mapper.Map<Note>(noteEntity);

            return CreatedAtAction(nameof(GetNoteAsync), "Notes", new { id = note.NoteId }, note);
        }

        // PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutNoteAsync([FromRoute] int id, [FromBody] Note note)
        {
            _logger.LogInformation("Begin PutNoteAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            if (note.NoteId != id)
            {
                return BadRequest("id parameter does not match Note id");
            }

            var noteEntity = _mapper.Map<NoteEntity>(note);
            noteEntity = await _NoteRepository.PutNoteAsync(id, noteEntity);

            if (noteEntity != null)
            {
                note = _mapper.Map<Note>(noteEntity);
                return Ok(note);
            }
            else
            {
                return NotFound();
            }

        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNoteAsync([FromRoute] int id)
        {
            _logger.LogInformation("Begin DeleteNoteAsync");

            if (id == 0)
            {
                return BadRequest("id is needed");
            }

            await _NoteRepository.DeleteNoteAsync(id);

            return NoContent();
        }
    }
}
