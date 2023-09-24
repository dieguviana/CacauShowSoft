using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace NewAppCacauShow.Classes
{
    internal class VendaDAO
    {
        private static Conexao conn;

        public VendaDAO()
        {
            conn = new Conexao();
        }

        public List<Venda> List()
        {

            try
            {
                List<Venda> list = new List<Venda>();

                var query = conn.Query();
                query.CommandText = "select " +
                "Venda.id_ven, " +
                "Venda.data_hora_ven, " +
                "Recebimento.valor_venda_rec, " +
                "Recebimento.desconto_rec, " +
                "Recebimento.valor_pago_rec, " +
                "Recebimento.forma_rec " +
                "from " +
                "Venda, Recebimento " +
                "where " +
                "(Recebimento.id_ven_fk = Venda.id_ven);";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Venda()
                    {
                        IdVenda = reader.GetInt32("id_ven"),
                        DataHora = reader.GetDateTime("data_hora_ven").ToString("dd/MM/yyyy HH:mm:ss"),
                        ValorVenda = reader.GetDouble("valor_venda_rec"),
                        Desconto = reader.GetDouble("desconto_rec"),
                        ValorPago = reader.GetDouble("valor_pago_rec"),
                        Forma = reader.GetString("forma_rec")
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

        public void Delete(Venda venda)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Venda_Produto WHERE id_ven_fk = @id; " +
                    "Delete from Recebimento where id_ven_fk = @id; " +
                    "Delete from Venda where id_ven = @id;";

                query.Parameters.AddWithValue("@id", venda.IdVenda);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Registro não removido da base de dados. Verifique e tente novamente.");

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
                query.CommandText = "Call InserirVenda();";

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

        public Venda ultimaVenda()
        {
            Venda venda = new Venda();

            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT MAX(id_ven) as id_ven FROM Venda";

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    venda.IdVenda = reader.GetInt32("id_ven");
                }

                return venda;
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

        public Venda GetById(Venda venda)
        {
            var query = conn.Query();
            query.CommandText = "Select " +
                "Recebimento.forma_rec, " +
                "Cliente.cpf_cli, " +
                "Recebimento.valor_venda_rec, " +
                "Recebimento.desconto_rec" +
                "Recebimento.valor_pago_rec";

            MySqlDataReader reader = query.ExecuteReader();

            venda.Forma = reader.GetString("form_rec");
            venda.Cliente = reader.GetString("cpf_cli");
            venda.ValorVenda = reader.GetDouble("valor_venda_rec");
            venda.Desconto = reader.GetDouble("desconto_rec");
            venda.ValorPago = reader.GetDouble("valor_pago_rec");

            return venda;
        }
    }
}
