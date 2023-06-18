using CommunityToolkit.Maui.Alerts;

namespace ToDoGPT;

public partial class UserPrefs : ContentPage
{
	public UserPrefs()
	{
		InitializeComponent();

        if (Preferences.Default.ContainsKey("openaikey"))
            entOpenAIAPI.Text = Preferences.Default.Get("openaikey", "");
        if (Preferences.Default.ContainsKey("promptplan"))
            entPromptPlanning.Text = Preferences.Default.Get("promptplan", "");
        if (Preferences.Default.ContainsKey("promptresource"))
            entPromptResource.Text = Preferences.Default.Get("promptresource", "");
        if (Preferences.Default.ContainsKey("promptcategory"))
            entPromptCategory.Text = Preferences.Default.Get("promptcategory", "");

    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
		Preferences.Default.Set("openaikey", entOpenAIAPI.Text);
		Preferences.Default.Set("promptplan", entPromptPlanning.Text);
		Preferences.Default.Set("promptresource", entPromptResource.Text);
        Preferences.Default.Set("promptcategory", entPromptCategory.Text);

        App.API.OpenAI = new(entOpenAIAPI.Text);

        var toast = Toast.Make("Preferences updated.");
        await toast.Show();
    }


}