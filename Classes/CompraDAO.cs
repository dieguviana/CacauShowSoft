using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class CompraDAO
    {
        private static Conexao conn;

        public  CompraDAO()
        {
            conn = new Conexao();
        }

        public List<Compra> List()
        {

            try
            {
                List<Compra> list = new List<Compra>();

                var query = conn.Query();
                query.CommandText = "select " +
                "Compra.id_com, " +
                "Compra.data_hora_com, " +
                "Fornecedor.nome_for, " +
                "Pagamento.valor_compra_pag, " +
                "Pagamento.status_pag, " +
                "Pagamento.vencimento_pag, " +
                "Pagamento.forma_pag " +
                "from " +
                "Compra, Pagamento, Fornecedor " +
                "where " +
                "(Pagamento.id_com_fk = Compra.id_com) and " +
                "(Pagamento.id_for_fk = Fornecedor.id_for)";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Compra()
                    {
                        IdCompra = reader.GetInt32("id_com"),
                        DataHora = reader.GetDateTime("data_hora_com").ToString("dd/MM/yyyy HH:mm:ss"),
                        Fornecedor = reader.GetString("nome_for"),
                        ValorCompra = reader.GetDouble("valor_compra_pag"),
                        Status = reader.GetString("status_pag"),
                        Vencimento = reader.GetDateTime("vencimento_pag").ToString("dd/MM/yyyy"),
                        Forma = reader.GetString("forma_pag")
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Compra compra)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Compra_Produto WHERE id_com_fk = @id; " +
                    "Delete from Pagamento where id_com_fk = @id; " +
                    "Delete from Compra where id_com = @id;";

                query.Parameters.AddWithValue("@id", compra.IdCompra);

                var result = query.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insert()
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Call InserirCompra();";

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro não foi inserido. Verifique e tente novamente");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public  Compra ultimaCompra()
        {
            Compra compra = new Compra();

            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT MAX(id_com) as id_com FROM Compra";

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    compra.IdCompra = reader.GetInt32("id_com");
                }

                return compra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public Compra GetById(Compra compra)
        {
            var query = conn.Query();
            query.CommandText = "Select " +
                "Pagamento.id_pag, " +
                "Pagamento.forma_pag, " +
                "Fornecedor.nome_for, " +
                "Pagamento.valor_compra_pag, " +
                "Pagamento.status_pag, " +
                "Pagamento.vencimento_pag " +
                "from " +
                "Pagamento, Fornecedor " +
                "where " +
                "(Pagamento.id_com_fk = @id) and " +
                "(Pagamento.id_for_fk = Fornecedor.id_for);";

            query.Parameters.AddWithValue("@id", compra.IdCompra);

            MySqlDataReader reader = query.ExecuteReader();
            if (reader.Read())
            {
                compra.IdPag = reader.GetInt32("id_pag");
                compra.Forma = reader.GetString("forma_pag");
                compra.Fornecedor = reader.GetString("nome_for");
                compra.ValorCompra = reader.GetDouble("valor_compra_pag");
                compra.Status = reader.GetString("status_pag");
                compra.Vencimento = reader.GetDateTime("vencimento_pag").ToString("dd/MM/yyyy");

            }

            return compra;
        }
    }
}
