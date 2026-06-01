using SQLite;

namespace personality_quiz.Models;

public class Setting
{
    [PrimaryKey]
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
