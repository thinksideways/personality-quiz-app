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
        new Question { Text = "I prefer quiet nights in.", IntrovertIfTrue = true },
        new Question { Text = "Large groups energize me.", IntrovertIfTrue = false },
        new Question { Text = "I think before I speak.", IntrovertIfTrue = true },
        new Question { Text = "I enjoy the spotlight.", IntrovertIfTrue = false },
        new Question { Text = "Small talk exhausts me.", IntrovertIfTrue = true },
        new Question { Text = "I talk to strangers often.", IntrovertIfTrue = false },
        new Question { Text = "I prefer working alone.", IntrovertIfTrue = true },
        new Question { Text = "I decide using logic.", IntrovertIfTrue = true },
        new Question { Text = "I am a 'people person'.", IntrovertIfTrue = false },
        new Question { Text = "I need alone time to recharge.", IntrovertIfTrue = true },
        new Question { Text = "I love trying new things.", IntrovertIfTrue = false },
        new Question { Text = "I have a small friend circle.", IntrovertIfTrue = true },
        new Question { Text = "I am comfortable in crowds.", IntrovertIfTrue = false },
        new Question { Text = "I enjoy deep conversations.", IntrovertIfTrue = true },
        new Question { Text = "I often act on impulse.", IntrovertIfTrue = false },
        new Question { Text = "I am a good listener.", IntrovertIfTrue = true },
        new Question { Text = "I enjoy public speaking.", IntrovertIfTrue = false },
        new Question { Text = "I prefer planning things out.", IntrovertIfTrue = true },
        new Question { Text = "I express feelings easily.", IntrovertIfTrue = false },
        new Question { Text = "I value my privacy highly.", IntrovertIfTrue = true }
    };

    private List<Question> _quizQuestions = new List<Question>();
    private int _currentQuestionIndex = 0;
    private int _introvertScore = 0;
    private bool _isQuizActive = false;

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
        _isQuizActive = true;
        
        StartBtn.IsVisible = false;
        SwipeHint.IsVisible = true;
        
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

    private void OnSwiped(object sender, SwipedEventArgs e)
    {
        if (!_isQuizActive) return;

        // Right = True, Left = False
        bool userChoice = e.Direction == SwipeDirection.Right;

        if (_quizQuestions[_currentQuestionIndex].IntrovertIfTrue == userChoice)
            _introvertScore++;

        _currentQuestionIndex++;
        ShowNextQuestion();
    }

    private void ShowResults()
    {
        _isQuizActive = false;
        SwipeHint.IsVisible = false;
        StartBtn.IsVisible = true;
        StartBtn.Text = "Restart Quiz";
        
        string result = _introvertScore >= 3 
            ? "Introvert: You enjoy solitude." 
            : "Extrovert: You thrive on energy.";
            
        QuestionLabel.Text = result;
    }
}
