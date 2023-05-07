using Microsoft.AspNetCore.Mvc.Diagnostics;
using MySqlConnector;
using NoteApi.Model;

namespace NoteApi;

public class DatabaseNoteDictionary : INoteDictionary
{
    private MySqlConnection CreateConnection()
    {
        return new MySqlConnection("Server=localhost;User ID=db_user;Password=db_user_pass;Database=app_db");
    }

    public async Task<Note?> Get(Guid id)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT * FROM Notes WHERE ID = @id", connection);
        await cmd.PrepareAsync();
        cmd.Parameters.AddWithValue("@id", id);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var readID = reader.GetGuid(0);
            var readName = reader.GetString(1);
            var readContent = reader.GetString(2);
            return new Note(readID, new NoteContent { Name = readName, Content = readContent });
        }
        else
        {
            return null;
        }
    }
    public async Task Add(Guid id, Note? note)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        await using var cmd = new MySqlCommand("INSERT INTO Notes(ID, Name, Content) VALUES (@id, @name, @content)", connection);
        await cmd.PrepareAsync();
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", note.Content.Name);
        cmd.Parameters.AddWithValue("@content", note.Content.Content);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task Add(Note? note)
    {
        await Add(note.ID, note);
    }

    public async Task<bool> Remove(Guid id)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        await using var cmd = new MySqlCommand("DELETE FROM Notes WHERE ID = @id", connection);
        await cmd.PrepareAsync();
        cmd.Parameters.AddWithValue("@id", id);
        return await cmd.ExecuteNonQueryAsync() > 0;
        
    }

    public async Task<bool> Update(Guid id, NoteContent content)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        await using var cmd = new MySqlCommand("UPDATE Notes SET Name = @name, Content = @content WHERE ID = @id",
            connection);
        await cmd.PrepareAsync();
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", content.Name);
        cmd.Parameters.AddWithValue("@content", content.Content);
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    public async IAsyncEnumerable<Note?> GetAll()
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT * FROM Notes", connection);
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var readID = reader.GetGuid(0);
            var readName = reader.GetString(1);
            var readContent = reader.GetString(2);
            yield return new Note(readID, new NoteContent { Name = readName, Content = readContent });
        }
    }
}