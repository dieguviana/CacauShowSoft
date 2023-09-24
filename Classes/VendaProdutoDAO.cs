using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using NewAppCacauShow.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewAppCacauShow.Classes
{
    internal class VendaProdutoDAO
    {
        private static Conexao conn;

        public VendaProdutoDAO()
        {
            conn = new Conexao();
        }

        public List<VendaProduto> List(int IdVenda)
        {
            try
            {
                List<VendaProduto> list = new List<VendaProduto>();

                var query = conn.Query();
                query.CommandText = "Select " +
                    "Venda_Produto.id_ven_pro, " +
                    "Produto.codigo_pro, " +
                    "Produto.nome_pro, " +
                    "Produto.valor_venda_pro, " +
                    "Venda_Produto.quantidade_ven_pro," +
                    "Venda_Produto.subtotal_ven_pro " +
                    "from " +
                    "Produto, Venda_Produto " +
                    "where " +
                    "(Venda_Produto.id_ven_fk = @IdVenda) and " +
                    "(Venda_Produto.id_pro_fk = Produto.id_pro);";

                query.Parameters.AddWithValue("@IdVenda", IdVenda);

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new VendaProduto()
                    {
                        IdVendaProduto = reader.GetInt32("id_ven_pro"),
                        Codigo = reader.GetInt32("codigo_pro"),
                        Produto = reader.GetString("nome_pro"),
                        Quantidade = reader.GetInt32("quantidade_ven_pro"),
                        ValorUnitario = reader.GetDouble("valor_venda_pro"),
                        Subtotal = reader.GetDouble("subtotal_ven_pro")
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

        public VendaProduto Delete(VendaProduto vendaProduto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Delete from Venda_Produto where (id_ven_pro = @id); " +
                    "Select sum(subtotal_ven_pro) as total from Venda_Produto where (id_ven_fk = @venda_fk);";

                query.Parameters.AddWithValue("@id", vendaProduto.IdVendaProduto);
                query.Parameters.AddWithValue("@venda_fk", vendaProduto.Venda_fk);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.IsDBNull(reader.GetOrdinal("total")))
                    {
                        vendaProduto.ValorUnitario = 0;
                    }
                    else
                    {
                        vendaProduto.ValorUnitario = reader.GetDouble("total");
                    }
                }

                return vendaProduto;
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

        public VendaProduto Insert(VendaProduto vendaProduto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Call InserirVendaProduto(@codigoPro, @quantidade, @venda_fk); " +
                    "Select sum(subtotal_ven_pro) as subtotal_ven_pro from Venda_Produto where (id_ven_fk = @venda_fk);";

                query.Parameters.AddWithValue("@codigoPro", vendaProduto.Codigo);
                query.Parameters.AddWithValue("@quantidade", vendaProduto.Quantidade);
                query.Parameters.AddWithValue("@venda_fk", vendaProduto.Venda_fk);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    vendaProduto.ValorUnitario = reader.GetDouble("subtotal_ven_pro");
                }

                return vendaProduto;
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
