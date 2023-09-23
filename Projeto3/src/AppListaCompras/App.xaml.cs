using AppListaCompras.Helpers;
using AppListaCompras.Views;

namespace AppListaCompras;

public partial class App : Application
{
    private static SQLiteDatabaseHelper _database;

    //public static SQLiteDatabaseHelper Database
    //{
    //    get
    //    {
    //        if (_database is null)
    //        {
    //            string path = Path.Combine(
    //                Environment.GetFolderPath(
    //                    Environment.SpecialFolder.LocalApplicationData),
    //                    "arquivo.db3"
    //                );

    //            _database = new SQLiteDatabaseHelper(path);
    //        }

    //        return _database;
    //    }
    //}

    public static SQLiteDatabaseHelper Db => _database is null
        ? new SQLiteDatabaseHelper(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "arquivo.db3"))
        : _database;


    public App()
    {
        //InitializeComponent();

        MainPage = new NavigationPage(new Listagem());
    }
}
