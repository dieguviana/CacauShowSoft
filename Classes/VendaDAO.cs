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
                "Recebimento.id_rec, " +
                "Venda.data_hora_ven, " +
                "Cliente.nome_cli, " +
                "Recebimento.valor_venda_rec, " +
                "Recebimento.desconto_rec, " +
                "Recebimento.valor_pago_rec, " +
                "Recebimento.forma_rec " +
                "from " +
                "Venda, Recebimento, Cliente " +
                "where " +
                "(Recebimento.id_ven_fk = Venda.id_ven) and " +
                "(Recebimento.id_cli_fk = Cliente.id_cli)";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Venda()
                    {
                        IdVenda = reader.GetInt32("id_ven"),
                        IdRec = reader.GetInt32("id_rec"),
                        DataHora = reader.GetDateTime("data_hora_ven").ToString("dd/MM/yyyy HH:mm:ss"),
                        Cliente = reader.GetString("nome_cli"),
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
                "Recebimento.id_rec, " +
                "Recebimento.forma_rec, " +
                "Cliente.nome_cli, " +
                "Recebimento.valor_venda_rec, " +
                "Recebimento.desconto_rec, " +
                "Recebimento.valor_pago_rec, " +
                "Venda.id_usu_fk, " +
                "Usuario.nome_usu, " +
                "Venda.data_hora_ven " +
                "" +
                "from " +
                "Recebimento, Cliente, Usuario, Venda " +
                "where " +
                "(Recebimento.id_ven_fk = @id) and " +
                "(Usuario.id_usu = Venda.id_usu_fk) and " +
                "(Recebimento.id_cli_fk = Cliente.id_cli);";

            query.Parameters.AddWithValue("@id", venda.IdVenda);

            MySqlDataReader reader = query.ExecuteReader();
            if (reader.Read())
            {
                venda.IdRec = reader.GetInt32("id_rec");
                venda.Usuario = reader.GetString("nome_usu");
                venda.Forma = reader.GetString("forma_rec");
                venda.Cliente = reader.GetString("nome_cli");
                venda.ValorVenda = reader.GetDouble("valor_venda_rec");
                venda.Desconto = reader.GetDouble("desconto_rec");
                venda.ValorPago = reader.GetDouble("valor_pago_rec");
                venda.DataHora = reader.GetDateTime("data_hora_ven").ToString("dd/MM/yyyy HH:mm:ss");
            }

            return venda;
        }
    }
}
