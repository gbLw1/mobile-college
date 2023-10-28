using AppFit.Helpers;

namespace AppFit;

public partial class App : Application
{
    static SQLiteDatabaseHelper database;

    public static SQLiteDatabaseHelper Database =>
            database ??= new SQLiteDatabaseHelper(
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    "banco.db3"
                )
            );

    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}
