namespace ToDoGPT;

public partial class SeePlan : ContentPage
{
	public static Editor Plan { get; set; }
	public SeePlan()
	{
		InitializeComponent();
		Plan = editPlan;

		editPlan.Text = Preferences.Default.Get("plan", "No plan, use ChatGPT to create one!");
	}

    private void btnSaveChanges_Clicked(object sender, EventArgs e)
		=> Preferences.Default.Set("plan", editPlan.Text);
}