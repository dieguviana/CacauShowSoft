using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewAppCacauShow.Classes
{
    internal class CompraProdutoDAO
    {
        private static Conexao conn;

        public CompraProdutoDAO()
        {
            conn = new Conexao();
        }

        public List<CompraProduto> List(int IdCompra)
        {
            try
            {
                List<CompraProduto> list = new List<CompraProduto>();

                var query = conn.Query();
                query.CommandText = "Select " +
                    "Compra_Produto.id_com_pro, " +
                    "Produto.codigo_pro, " +
                    "Produto.nome_pro, " +
                    "Produto.valor_compra_pro, " +
                    "Compra_Produto.quantidade_com_pro," +
                    "Compra_Produto.subtotal_com_pro " +
                    "from " +
                    "Produto, Compra_Produto " +
                    "where " +
                    "(Compra_Produto.id_com_fk = @IdCompra) and " +
                    "(Compra_Produto.id_pro_fk = Produto.id_pro);";

                query.Parameters.AddWithValue("@IdCompra", IdCompra);

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new CompraProduto()
                    {
                        IdCompraProduto = reader.GetInt32("id_com_pro"),
                        Codigo = reader.GetInt32("codigo_pro"),
                        Produto = reader.GetString("nome_pro"),
                        Quantidade = reader.GetInt32("quantidade_com_pro"),
                        ValorUnitario = reader.GetDouble("valor_compra_pro"),
                        Subtotal = reader.GetDouble("subtotal_com_pro")
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

        public CompraProduto Delete(CompraProduto compraProduto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Delete from Compra_Produto where (id_com_pro = @id); " +
                    "Select sum(subtotal_com_pro) as total from Compra_Produto where (id_com_fk = @compra_fk);";

                query.Parameters.AddWithValue("@id", compraProduto.IdCompraProduto);
                query.Parameters.AddWithValue("@compra_fk", compraProduto.Compra_fk);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.IsDBNull(reader.GetOrdinal("total")))
                    {
                        compraProduto.ValorTotal = 0;
                    }
                    else
                    {
                        compraProduto.ValorTotal = reader.GetDouble("total");
                    }
                }

                return compraProduto;
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

        public CompraProduto Insert(CompraProduto compraProduto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Call InserirCompraProduto(@codigoPro, @quantidade, @compra_fk); " +
                    "Select sum(subtotal_com_pro) as subtotal_com_pro from Compra_Produto where (id_com_fk = @compra_fk);";

                query.Parameters.AddWithValue("@codigoPro", compraProduto.Codigo);
                query.Parameters.AddWithValue("@quantidade", compraProduto.Quantidade);
                query.Parameters.AddWithValue("@compra_fk", compraProduto.Compra_fk);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    compraProduto.ValorTotal = reader.GetDouble("subtotal_com_pro");
                }

                return compraProduto;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " Insira um código de produto que exista no sistema.", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
