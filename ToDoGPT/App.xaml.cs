namespace ToDoGPT;

public partial class App : Application
{
	public static DBMan Database { get; private set; }
	public static APIMan API { get; private set; }
	public App(DBMan database, APIMan api)
	{
		InitializeComponent();

		MainPage = new AppShell();
        Database = database;
		API = api;
	}
}
