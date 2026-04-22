using System.Collections.ObjectModel;
using personality_quiz.Models;

namespace personality_quiz;

public partial class MainPage : ContentPage
{
    private Quiz _allQuestions = new Quiz();

    private List<string> _introvertHobbies = new List<string> { "Reading", "Solo Gaming", "Nature Walks", "Painting", "Baking" };
    private List<string> _extrovertHobbies = new List<string> { "Team Sports", "Public Speaking", "Group Fitness", "Concerts", "Hosting Events" };

    public ObservableCollection<string> Hobbies { get; set; } = new ObservableCollection<string>();

    private List<Question> _quizQuestions = new List<Question>();
    private int _currentQuestionIndex = 0;
    private int _introvertScore = 0;
    private bool _isQuizActive = false;
    private CancellationTokenSource? _animationCts;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        StartBackgroundAnimation();
    }

    private async void StartBackgroundAnimation()
    {
        _animationCts?.Cancel();
        _animationCts = new CancellationTokenSource();
        var token = _animationCts.Token;

        try
        {
            while (!token.IsCancellationRequested)
            {
                if (!_isQuizActive && ResultLayout.IsVisible == false)
                {
                    await Task.WhenAll(
                        QuizImage.FadeTo(0, 2000),
                        IntrovertImage.FadeTo(1, 2000)
                    );
                    await Task.Delay(1000, token);
                    await Task.WhenAll(
                        QuizImage.FadeTo(1, 2000),
                        IntrovertImage.FadeTo(0, 2000)
                    );
                    await Task.Delay(1000, token);
                }
                else
                {
                    await Task.Delay(500, token);
                }
            }
        }
        catch (OperationCanceledException) { }
    }

    private void OnStartQuizTapped(object? sender, EventArgs e)
    {
        StartQuiz();
    }

    private void StartQuiz()
    {
        _isQuizActive = true;
        var random = new Random();
        _quizQuestions = _allQuestions.OrderBy(x => random.Next()).Take(5).ToList();
        _currentQuestionIndex = 0;
        _introvertScore = 0;
        _isQuizActive = true;
        Hobbies.Clear();
        
        StartBtn.IsVisible = false;
        ResultButtons.IsVisible = false;
        SwipeHint.IsVisible = true;
        ResultLayout.IsVisible = false;
        
        // Reset positions and ensure correct starting image state
        QuizImage.Scale = 1.0;
        QuizImage.TranslationY = 0;
        QuestionLabel.TranslationY = 0;
        IntrovertImage.Opacity = 0;
        QuizImage.Opacity = 1;
        
        ShowNextQuestion();
    }

    private void ShowNextQuestion()
    {
        if (_currentQuestionIndex < _quizQuestions.Count)
        {
            var currentQuestion = _quizQuestions[_currentQuestionIndex];
            QuestionLabel.Text = currentQuestion.Text;
            
            // During quiz, swap sources on the primary image and hide the other
            IntrovertImage.Opacity = 0;
            QuizImage.Opacity = 1;
            QuizImage.Source = currentQuestion.IntrovertIfTrue 
                ? "glowing_introvert_head.png" 
                : "glowing_head.png";
        }
        else
        {
            ShowResults();
        }
    }

    private void OnSwiped(object sender, SwipedEventArgs e)
    {
        if (!_isQuizActive) return;

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
        StartBtn.IsVisible = false;
        ResultButtons.IsVisible = true;
        ResultLayout.IsVisible = true;
        
        bool isIntrovert = _introvertScore >= 3;
        
        QuizImage.Source = isIntrovert 
            ? "glowing_introvert_head.png" 
            : "glowing_head.png";
        
        QuizImage.Opacity = 1;
        IntrovertImage.Opacity = 0;
            
        // Final Page Only: Adjust size and shift elements up
        QuizImage.Scale = 0.8;
        QuizImage.TranslationY = -40; // Shift image up
        QuestionLabel.TranslationY = -40; // Shift label up
        ResultLayout.TranslationY = -40; // Shift results up
            
        QuestionLabel.Text = isIntrovert 
            ? "Introvert: You enjoy solitude." 
            : "Extrovert: You thrive on energy.";

        var selectedHobbies = isIntrovert ? _introvertHobbies : _extrovertHobbies;
        
        foreach (var hobby in selectedHobbies)
        {
            Hobbies.Add(hobby);
        }
    }

    private void OnHomeClicked(object sender, EventArgs e)
    {
        _isQuizActive = false;
        ResultButtons.IsVisible = false;
        ResultLayout.IsVisible = false;
        StartBtn.IsVisible = true;
        
        // Reset state to initial landing page
        QuizImage.Scale = 1.0;
        QuizImage.TranslationY = 0;
        QuestionLabel.TranslationY = 0;
        QuestionLabel.Text = "What's your personality type?";
        
        // Ensure the animation loop can pick back up naturally
        QuizImage.Opacity = 1;
        IntrovertImage.Opacity = 0;
    }
}
