using ToDoGPT.Models;
using CommunityToolkit.Maui.Alerts;

namespace ToDoGPT;

public partial class AddToDoTask : ContentPage
{
	public AddToDoTask()
	{
		InitializeComponent();
	}

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
		var task = new ToDoTask
		{
			Title = entTitle.Text,
			DueDate = dateDue.Date,
			CreationDate = DateTime.Now,
			IsCompleted= false,
		};

		int rows = App.Database.Connection.Insert(task);
		var toast = Toast.Make((rows>0)?"Task saved.":"Error: Task did not stored at the database");
		await toast.Show();

		MainPage.UpdateUI();

		entTitle.Text = "";
    }
}