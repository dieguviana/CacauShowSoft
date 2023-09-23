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
                    "Venda_Produto.id_ven_pro, " +
                    "Produto.codigo_pro, " +
                    "Produto.nome_pro, " +
                    "Produto.valor_venda_pro " +
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
                        CodigoPro = reader.GetInt32("codigo_pro"),
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

        public void Delete(VendaProduto vendaProduto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Venda_Produto WHERE id_ven_pro = @id";

                query.Parameters.AddWithValue("@id", vendaProduto.IdVendaProduto);

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
    }
}
