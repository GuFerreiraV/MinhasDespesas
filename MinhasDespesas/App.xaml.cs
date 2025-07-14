using MinhasDespesas.Helpers;

namespace MinhasDespesas
{
    public partial class App : Application
    {

        static SQLiteDataBaseHelper _db;

        public static SQLiteDataBaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        // Environment: prove infos do ambiente de execução (Windows e Android no caso)
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData
                            ), "banco_sqlite_compras.db3"
                        );
                    
                    _db = new SQLiteDataBaseHelper(path);
                }
                
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListarProduto());
        }
    }
}
