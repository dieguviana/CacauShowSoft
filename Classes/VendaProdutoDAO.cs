using MySql.Data.MySqlClient;
using NewAppCacauShow.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    "Produto.codigo_pro, " +
                    "Produto.nome_pro, " +
                    "Produto.valor_venda_pro " +
                    "from " +
                    "Produto, Produto_Venda " +
                    "where " +
                    "(Produto_Venda.id_ven_fk = @IdVenda) and " +
                    "(Produto_Venda.id_pro_fk = Produto.id_pro);";

                query.Parameters.AddWithValue("@IdVenda", IdVenda);

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new VendaProduto()
                    {
                        IdVendaProduto = reader.GetInt32("codigo_pro"),
                        Nome = reader.GetString("nome_pro"),
                        ValorVenda = reader.GetDouble("valor_venda_pro")
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
    }
}
