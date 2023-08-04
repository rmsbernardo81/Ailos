using Microsoft.Data.Sqlite;

namespace Questao5.Infrastructure.Repository
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\database.sqlite"; }
        }

        public static SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
