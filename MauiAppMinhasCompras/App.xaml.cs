using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Declarando um campo estático ligando com o SQLite
        static SQLiteDatabaseHelper _db;
        // e assim vamos chamar de _db.

        // para chegar ao campo db, tenho que tornar ela pública 
        public static SQLiteDatabaseHelper Db
            // como é uma propriedade, então vamos deixar em maiúsculo
        
        
        {
            get
            {
                // toda vez que eu chamar o Db, tenho que dar o retorno no campo db
                // ele vai ter uma instancia da classe databasehelper

                if (_db == null) // se não houver objetos
                {
                    string path = Path.Combine( // declarmos a variavel path
                        Environment.GetFolderPath( // aqui é onde vai estar o path, começando pelo Combine (caminho absoluto)
                            Environment.SpecialFolder.LocalApplicationData), // dentro do combine, passamos os parâmetros, se eu tiver
                                                   // no windows, ele vai dar informações do windows, se android, do android
                        "banco_sqlite_compras.db3");
                    // aqui estamos pegando o caminho do banco de dados
                    // e concatenando com o nome do banco de dados
                    // LocalApplicationData é o local onde o aplicativo
                    // armazena os dados do usuário

                    _db = new SQLiteDatabaseHelper(path); // aqui estamos instanciando a classe
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