using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NoteApi.Model;
[Table("Notes")]
public class Note
{
    [Key]
    public Guid ID { get;  set; }

    public NoteContent Content { get; set; } = new NoteContent();

    public Note()
    {
        
    }

    public Note(NoteContent content)
    {
        ID = Guid.NewGuid();
        Content = content;
    }

    public Note(string name, string content)
    {
        ID = Guid.NewGuid();
        Content = new NoteContent { Name = name, Content = content };
    }

    public Note(Guid id, NoteContent content)
    {
        ID = id;
        Content = content;
    }
}

[Owned]
public class NoteContent
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
}