namespace personality_quiz;

public partial class MainPage : ContentPage
{
    private class Question
    {
        public string Text { get; set; } = string.Empty;
        // True = Introvert point, False = Extrovert point
        public bool IntrovertIfTrue { get; set; }
    }

    private List<Question> _allQuestions = new List<Question>
    {
        new Question { Text = "I prefer a quiet night in over a big party.", IntrovertIfTrue = true },
        new Question { Text = "I feel energized after spending time with a large group of people.", IntrovertIfTrue = false },
        new Question { Text = "I usually think before I speak.", IntrovertIfTrue = true },
        new Question { Text = "I enjoy being the center of attention.", IntrovertIfTrue = false },
        new Question { Text = "I find small talk exhausting.", IntrovertIfTrue = true },
        new Question { Text = "I often strike up conversations with strangers.", IntrovertIfTrue = false },
        new Question { Text = "I prefer working alone rather than in a team.", IntrovertIfTrue = true },
        new Question { Text = "I make decisions based on logic rather than emotion.", IntrovertIfTrue = true },
        new Question { Text = "I am often described as a 'people person'.", IntrovertIfTrue = false },
        new Question { Text = "I need alone time to recharge my batteries.", IntrovertIfTrue = true },
        new Question { Text = "I love trying new and exciting activities.", IntrovertIfTrue = false },
        new Question { Text = "I tend to have a small circle of close friends.", IntrovertIfTrue = true },
        new Question { Text = "I am very comfortable in crowded places.", IntrovertIfTrue = false },
        new Question { Text = "I enjoy deep, one-on-one conversations.", IntrovertIfTrue = true },
        new Question { Text = "I often act on impulse.", IntrovertIfTrue = false },
        new Question { Text = "I am a good listener.", IntrovertIfTrue = true },
        new Question { Text = "I enjoy public speaking.", IntrovertIfTrue = false },
        new Question { Text = "I prefer to plan things out rather than being spontaneous.", IntrovertIfTrue = true },
        new Question { Text = "I find it easy to express my feelings to others.", IntrovertIfTrue = false },
        new Question { Text = "I value my privacy highly.", IntrovertIfTrue = true }
    };

    private List<Question> _quizQuestions = new List<Question>();
    private int _currentQuestionIndex = 0;
    private int _introvertScore = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        StartQuiz();
    }

    private void StartQuiz()
    {
        var random = new Random();
        _quizQuestions = _allQuestions.OrderBy(x => random.Next()).Take(5).ToList();
        _currentQuestionIndex = 0;
        _introvertScore = 0;
        
        StartBtn.IsVisible = false;
        QuizButtons.IsVisible = true;
        
        ShowNextQuestion();
    }

    private void ShowNextQuestion()
    {
        if (_currentQuestionIndex < _quizQuestions.Count)
        {
            QuestionLabel.Text = _quizQuestions[_currentQuestionIndex].Text;
        }
        else
        {
            ShowResults();
        }
    }

    private void OnTrueClicked(object sender, EventArgs e)
    {
        if (_quizQuestions[_currentQuestionIndex].IntrovertIfTrue)
            _introvertScore++;
            
        _currentQuestionIndex++;
        ShowNextQuestion();
    }

    private void OnFalseClicked(object sender, EventArgs e)
    {
        if (!_quizQuestions[_currentQuestionIndex].IntrovertIfTrue)
            _introvertScore++;
            
        _currentQuestionIndex++;
        ShowNextQuestion();
    }

    private void ShowResults()
    {
        QuizButtons.IsVisible = false;
        StartBtn.IsVisible = true;
        StartBtn.Text = "Restart Quiz";
        
        string result = _introvertScore >= 3 
            ? "You are an Introvert! You find strength in solitude and deep reflection." 
            : "You are an Extrovert! You thrive on social energy and outward action.";
            
        QuestionLabel.Text = result;
    }
}
