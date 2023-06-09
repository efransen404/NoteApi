using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace NoteApi.Model;
[Table("Notes")]
public class Note
{
    [Key]
    public Guid guid { get;  set; }
    [Required]
    public String title { get; set; }
    [Required]
    public String content { get; set; }

    public Note()
    {
        
    }

    public Note(string name, string content)
    {
        guid = Guid.NewGuid();
        title = name;
        this.content = content;
    }

    public Note(Guid guid, string name, string content)
    {
        this.guid = guid;
        title = name;
        this.content = content;
    }
}