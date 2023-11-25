using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks; 

namespace NewAppCacauShow.Classes
{
    internal class ProdutoDAO
    {
        private static Conexao conn;

        public ProdutoDAO()
        {
            conn = new Conexao();
        }

        public Produto GetById(int id)
        {
            var query = conn.Query();
            query.CommandText = "SELECT * FROM Produto WHERE id_pro = @id";

            query.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = query.ExecuteReader();
            var produto = new Produto();

            if (reader.Read())
            {
                produto.IdProduto = reader.GetInt32("id_pro");
                produto.Nome = reader.GetString("nome_pro");
                produto.Codigo = reader.GetInt32("codigo_pro");
                produto.DataVenc = reader.GetDateTime("data_venc_pro").ToString("dd/MM/yyyy");
                produto.ValorVenda = reader.GetDouble("valor_venda_pro");
                produto.ValorCompra = reader.GetDouble("valor_compra_pro");
                produto.Descricao = reader.GetString("descricao_pro");
            }

            return produto;
        }


        public List<Produto> List()
        {
            try
            {
                List<Produto> list = new List<Produto>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Produto";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Produto()
                    {
                        IdProduto = reader.GetInt32("id_pro"),
                        Nome = reader.GetString("nome_pro"),
                        Codigo = reader.GetInt32("codigo_pro"),
                        DataVenc = reader.GetDateTime("data_venc_pro").ToString("dd/MM/yyyy"),
                        ValorVenda = reader.GetDouble("valor_venda_pro"),
                        ValorCompra = reader.GetDouble("valor_compra_pro"),
                        Descricao = reader.GetString("descricao_pro")
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

        public void Delete(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Produto WHERE id_pro = @id";

                query.Parameters.AddWithValue("@id", produto.IdProduto);

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

        public void Insert(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO Produto (nome_pro, codigo_pro, data_venc_pro, valor_compra_pro, valor_venda_pro, descricao_pro) VALUES (@nome, @codigo, @dataVenc, @valorCompra, @valorVenda, @descricao)";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@codigo", produto.Codigo);
                query.Parameters.AddWithValue("@dataVenc", produto.DataVenc);
                query.Parameters.AddWithValue("@valorCompra", produto.ValorCompra);
                query.Parameters.AddWithValue("@valorVenda", produto.ValorVenda);
                query.Parameters.AddWithValue("descricao", produto.Descricao);

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

        public void Update(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Produto SET nome_pro = @nome, codigo_pro = @codigo, " +
                    "data_venc_pro = @dataVenc, valor_compra_pro = @valorCompra, valor_venda_pro = @valorVenda, descricao_pro = @descricao" +
                    " WHERE id_pro = @id";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@codigo", produto.Codigo);
                query.Parameters.AddWithValue("@dataVenc", produto.DataVenc);
                query.Parameters.AddWithValue("@valorCompra", produto.ValorCompra);
                query.Parameters.AddWithValue("@valorVenda", produto.ValorVenda);
                query.Parameters.AddWithValue("descricao", produto.Descricao);
                query.Parameters.AddWithValue("@id", produto.IdProduto);


                var resultado = query.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Registro não atualizado.");
                }
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

    
