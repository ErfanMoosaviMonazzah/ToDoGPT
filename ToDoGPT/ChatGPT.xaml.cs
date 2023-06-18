using Azure.AI.OpenAI;
using CommunityToolkit.Maui.Alerts;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoGPT.Models;

namespace ToDoGPT;

public partial class ChatGPT : ContentPage
{
	public string Prompt { get; set; }
	public ChatGPT()
	{
		InitializeComponent();
        pickTools.SelectedIndex = 0;
	}

    private void pickTools_SelectedIndexChanged(object sender, EventArgs e)
    {
        var prompt = pickTools.SelectedIndex switch
        {
            0 => Preferences.Default.Get("promptplan", "Suggest a plan to do my tasks,"),
            1 => Preferences.Default.Get("promptresource", "Suggest resources for each task,"),
            2 => Preferences.Default.Get("promptcategory", "Categorize the following tasks based on similarity,"),
            _ => ""
        };
        
        prompt += $"\nTasks:\n{GetTasks()}\nToday's Date: {DateTime.Now}";
        editPrompt.Text = prompt;
    }

    public string GetTasks()
    {
        var tasks = App.Database.ObsTasksOngoing()
            .Select(x => $"{x.Object.Id}: {x.Object.Title} - {x.Object.DueDate}")
            .ToList();
        return string.Join("\n", tasks);
    }

    private async void btnSendPrompt_Clicked(object sender, EventArgs e)
    {
        ChatCompletionsOptions options = new ChatCompletionsOptions();

        btnSendPrompt.Text = "Awaiting ChatGPT's Response...";

        options.Temperature = 0;
        options.Messages.Add(new ChatMessage(ChatRole.User, editPrompt.Text));
        var response = await App.API.OpenAI.GetChatCompletionsAsync(
            App.API.LM, options);

        btnSendPrompt.Text = "Ask ChatGPT";

        var choise = response.Value.Choices.First();
        editResponse.Text = choise.Message.Content;

        btnSave.IsVisible = true;
        editResponse.IsVisible = true;
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        switch (pickTools.SelectedIndex)
        {
            case 0:
                Preferences.Default.Set("plan", editResponse.Text);
                SeePlan.Plan.Text = editResponse.Text;

                var toast0 = Toast.Make("Plan added.");
                await toast0.Show();
                break;
            case 1:
                var json = JsonSerializer.Deserialize<Dictionary<int, List<string>>>(editResponse.Text);
                foreach (var key in json.Keys)
                {
                    var value = json[key];
                    var task = App.Database.Connection.Table<ToDoTask>()
                        .Where(x => x.Id == key)
                        .First();
                    task.Resources = string.Join("\n", value);
                    App.Database.Connection.Update(task);
                }

                var toast = Toast.Make("Resources added.");
                await toast.Show();
                break;
            case 2:
                var json2 = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(editResponse.Text);
                foreach (var key in json2.Keys)
                {
                    var value = json2[key];
                    foreach (var id in value)
                    {
                        var task = App.Database.Connection.Table<ToDoTask>()
                            .Where(x => x.Id == id)
                            .First();
                        task.Category = key;
                        App.Database.Connection.Update(task);

                    }
                }

                var toast2 = Toast.Make("Categories added.");
                await toast2.Show();
                break;
        }

        MainPage.UpdateUI();

    }
}