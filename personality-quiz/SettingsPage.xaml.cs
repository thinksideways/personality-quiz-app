using personality_quiz.Services;

namespace personality_quiz;

public partial class SettingsPage : ContentPage
{
    private readonly DatabaseService _databaseService;

	public SettingsPage(DatabaseService databaseService)
	{
		InitializeComponent();
        _databaseService = databaseService;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        string mode = await _databaseService.GetSettingAsync("QuizMode", "Swipe");
        ModeSwitch.IsToggled = mode == "Swipe";
        UpdateDescription(mode);
    }

	private async void OnModeToggled(object sender, ToggledEventArgs e)
	{
		string mode = e.Value ? "Swipe" : "Button";
		await _databaseService.SaveSettingAsync("QuizMode", mode);
		UpdateDescription(mode);
	}

	private void UpdateDescription(string mode)
	{
		DescriptionLabel.Text = $"The quiz will use {mode.ToLower()}s for answers.";
	}
}
