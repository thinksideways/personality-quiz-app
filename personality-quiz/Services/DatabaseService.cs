using SQLite;
using personality_quiz.Models;

namespace personality_quiz.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;

    private async Task Init()
    {
        if (_database is not null)
            return;

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "QuizData.db");
        _database = new SQLiteAsyncConnection(databasePath);
        await _database.CreateTableAsync<Setting>();
    }

    public async Task SaveSettingAsync(string key, string value)
    {
        await Init();
        var setting = new Setting { Key = key, Value = value };
        var existing = await _database!.Table<Setting>().Where(x => x.Key == key).FirstOrDefaultAsync();
        
        if (existing != null)
        {
            await _database.UpdateAsync(setting);
        }
        else
        {
            await _database.InsertAsync(setting);
        }
    }

    public async Task<string> GetSettingAsync(string key, string defaultValue)
    {
        await Init();
        var setting = await _database!.Table<Setting>().Where(x => x.Key == key).FirstOrDefaultAsync();
        return setting?.Value ?? defaultValue;
    }
}
