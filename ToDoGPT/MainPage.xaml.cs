using ToDoGPT.Models;

namespace ToDoGPT;

public partial class MainPage : ContentPage
{
	public static ListView Ongoing { get; set; }
	public static ListView Completed { get; set; }
	public static ToDoTask SelectedTask { get; set; }
	public MainPage()
	{
		InitializeComponent();
		Ongoing = lvOngoings;
		Completed = lvCompleted;

		MainPage.UpdateUI();
    }
	public static void UpdateUI()
	{
        Ongoing.ItemsSource = App.Database.ObsTasksOngoing();
        Completed.ItemsSource = App.Database.ObsTasksCompleted();
    }
    private async void lvOngoings_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		var lv = sender as ListView;
		SelectedTask = (lv.SelectedItem as ViewToDoTask).Object;

        await Shell.Current.GoToAsync("edittask");
    }
}

