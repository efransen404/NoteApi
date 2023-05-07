namespace NoteApi.Model;

public class Note
{
    public Guid ID { get; private set; }
    public NoteContent Content { get; set; }

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

public class NoteContent
{
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}