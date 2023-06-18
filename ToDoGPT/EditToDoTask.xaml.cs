using CommunityToolkit.Maui.Alerts;
using ToDoGPT.Models;

namespace ToDoGPT;

public partial class EditToDoTask : ContentPage
{
	public EditToDoTask()
	{
		InitializeComponent();

		var task = MainPage.SelectedTask;
		entTitle.Text = task.Title;
		dateDue.Date = task.DueDate.Date;
		cbCompleted.IsChecked = task.IsCompleted;
		editResources.Text = task.Resources;
		entCategory.Text = task.Category;
	}

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
		var task = MainPage.SelectedTask;

		task.DueDate = dateDue.Date;
		task.Title = entTitle.Text;
		task.Resources = editResources.Text;
        task.IsCompleted= cbCompleted.IsChecked;
		task.Category= entCategory.Text;
		if (task.IsCompleted)
            task.CompletionDate = DateTime.Now;

		int rows = App.Database.Connection.Update(task);
        var toast = Toast.Make((rows > 0) ? "Task edited." : "Error: Task did not stored at the database");
        await toast.Show();

		MainPage.UpdateUI();
    }

    private async void btnDel_Clicked(object sender, EventArgs e)
    {
        var task = MainPage.SelectedTask;
        int rows = App.Database.Connection.Delete(task);
        var toast = Toast.Make((rows > 0) ? "Task deleted." : "Error: Task did not stored at the database");
        await toast.Show();

        MainPage.UpdateUI();

    }
}