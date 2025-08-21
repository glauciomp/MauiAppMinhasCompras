using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null) // se não houver objetos
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");
                    // aqui estamos pegando o caminho do banco de dados
                    // e concatenando com o nome do banco de dados
                    // LocalApplicationData é o local onde o aplicativo
                    // armazena os dados do usuário

                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();
      
            // return new Window(new AppShell());
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}