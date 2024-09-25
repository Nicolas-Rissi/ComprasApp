using ComprasApp.Helpers;
using System.IO;

namespace ComprasApp
{
    public partial class App : Application
    {
        // Instância estática do helper de banco de dados SQLite.
        static SQLiteDatabaseHelper database;

        // Propriedade que fornece acesso à instância do banco de dados.
        public static SQLiteDatabaseHelper Database
        {
            get
            {
                // Verifica se a instância do banco de dados ainda não foi criada.
                if (database == null)
                {
                    // Define o caminho do banco de dados no armazenamento local da aplicação.
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "arquivo.db3");

                    // Cria uma nova instância do helper de banco de dados com o caminho especificado.
                    database = new SQLiteDatabaseHelper(path);
                }

                // Retorna a instância do banco de dados.
                return database;
            }
        }

        // Construtor da classe App.
        public App()
        {
            InitializeComponent(); // Inicializa os componentes da aplicação.

            // Define a cultura atual do thread para PT-BR.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            // Define a página principal da aplicação como AppShell.
            MainPage = new AppShell();
        }
    }

}
