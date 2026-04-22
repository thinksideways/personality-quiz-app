namespace personality_quiz.Models;

public class Question
{
    public string Text { get; set; } = string.Empty;
    // True = Introvert point, False = Extrovert point
    public bool IntrovertIfTrue { get; set; }
}
