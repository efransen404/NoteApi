using NoteApi.Model;
using System.Linq;
namespace NoteApi;


public interface INoteDictionary
{
    public Task<Note?> Get(Guid id);
    public Task Add(Guid id, Note? note);
    public Task Add(Note? note);
    public Task<bool> Remove(Guid id);
    public Task<bool> Update(Guid id, String name, String content);

    public IAsyncEnumerable<Note?> GetAll();
}
public class NoteDictionary : INoteDictionary
{
    private readonly Dictionary<Guid, Note?> _notes = new Dictionary<Guid, Note?>();

    public Task<Note?> Get(Guid id) => Task.FromResult(_notes[id]);

    public Task Add(Guid id, Note? note)
    {
        _notes.Add(id, note);
        return Task.CompletedTask;
    }

    public Task Add(Note? note)
    {
        _notes.Add(note.guid, note);
        return Task.CompletedTask;
    }

    public Task<bool> Remove(Guid id)
    {
        return Task.FromResult(_notes.Remove(id));
    }

#pragma warning disable CS1998
    public async IAsyncEnumerable<Note?> GetAll()
#pragma warning restore CS1998
    {
        foreach (var notesValue in _notes.Values)
        {
            yield return notesValue;
        }
    }

    public Task<bool> Update(Guid id, String name, String content)
    {
        var note = _notes.GetValueOrDefault(id, null);
        if (note == null) return Task.FromResult(false);
        note.title = name;
        note.content = content;
        return Task.FromResult(true);
    }
}