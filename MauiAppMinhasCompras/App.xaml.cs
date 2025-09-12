using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras;

public partial class App : Application
{
    private static SQLiteDatabaseHelper _db;

    public static SQLiteDatabaseHelper Db
    {
        get
        {
            if (_db == null)
            {
                string dbPath = Path.Combine(
                    FileSystem.AppDataDirectory,
                    "minhascompras.db3"
                );

                _db = new SQLiteDatabaseHelper(dbPath);
            }

            return _db;
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new Views.ListaProduto());
    }
}