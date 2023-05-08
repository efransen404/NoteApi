using Microsoft.EntityFrameworkCore;
using NoteApi.Model;

namespace NoteApi;

public class AppDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
}