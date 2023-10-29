using AppFit.Helpers;

namespace AppFit;

public partial class App : Application
{
    static SQLiteDatabaseHelper database;

    public static SQLiteDatabaseHelper Database =>
        database ??= new SQLiteDatabaseHelper(Constants.DatabasePath);

    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}
