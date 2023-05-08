using Microsoft.EntityFrameworkCore;
using NoteApi.Model;

namespace NoteApi.DataStores;

public class EFNoteDictionary : INoteDictionary
{
    private readonly AppDbContext _context;
    public EFNoteDictionary(AppDbContext context)
    {
        _context = context;
    }
    public Task<Note?> Get(Guid id)
    {
        return _context.Notes.Where(x => x.ID == id).FirstOrDefaultAsync();
    }

    public async Task Add(Guid id, Note? note)
    {
        if (note is null) throw new ArgumentNullException(nameof(note));
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
    }

    public Task Add(Note? note)
    {
        if (note is null) throw new ArgumentNullException(nameof(note));
        return Add(note.ID, note);
    }

    public async Task<bool> Remove(Guid id)
    {
        var note = await _context.Notes.Where(x => x.ID == id).FirstOrDefaultAsync();
        if (note is null) return false;
        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Guid id, NoteContent content)
    {
        var newNote = new Note(id, content);
        _context.Notes.Update(newNote);
        await _context.SaveChangesAsync();
        return true;
    }

    public IAsyncEnumerable<Note?> GetAll()
    {
        return _context.Notes.AsAsyncEnumerable();
    }
}