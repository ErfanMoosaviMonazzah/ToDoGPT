namespace ToDoGPT;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("mainpage", typeof(MainPage));
        Routing.RegisterRoute("addtask", typeof(AddToDoTask));
        Routing.RegisterRoute("edittask", typeof(EditToDoTask));
    }
}
