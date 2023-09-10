using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NewAppCacauShow
{
    class Conexao
    {
        private static string host = "localhost";

        private static string porta = "3306";

        private static string usuario = "root";

        private static string senha = "root";

        private static string nomebd = "soft_cacaushow";

        private static MySqlConnection connection;

        private static MySqlCommand commmand;

        public Conexao()
        {
            try
            {
                connection = new MySqlConnection($"server={host};database={nomebd};port={porta};user={usuario};password={senha}");
                connection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
