using Microsoft.AspNetCore.Mvc;
using NoteApi.Model;

namespace NoteApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteDictionary _notes;
    private ILogger<NoteController> _logger;

    public NoteController(INoteDictionary notes, ILogger<NoteController> logger)
    {
        _notes = notes;
        _logger = logger;
    }

    [HttpGet]
    public IAsyncEnumerable<Note?> Get()
    {
        return _notes.GetAll();
    }

    [HttpPost]
    public async Task<Guid> CreateNote(NoteContent content)
    {
        Note? note = new Note(content);
        await _notes.Add(note);
        return note.ID;
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveNote(Guid id)
    {
        if(await _notes.Remove(id)) return Ok();
        return NotFound();
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Note>> GetNote(Guid id)
    {
        var note = await _notes.Get(id);
        if (note is null) return NotFound();
        return note;
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateNote(Guid id, NoteContent content)
    {
        bool result = await _notes.Update(id, content);
        if (!result) return NotFound();
        return Ok();
    }
}