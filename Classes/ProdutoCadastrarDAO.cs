using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class ProdutoCadastrarDAO
    {
        

        public static Conexao conn;

        public ProdutoCadastrarDAO() 
        { 
            conn = new Conexao();
        }
        public ProdutoCadastrar GetById(int id)
        {
            var query = conn.Query();
            query.CommandText = "SELECT * ProdutoCadastrar WHERE id_for = @id";

            query.Parameters.AddWithValue("id", id);

            MySqlDataReader reader= query.ExecuteReader();
            var produto = new ProdutoCadastrar();

            if(reader.Read())
            {
                produto.Id = reader.GetInt32("id_pro");
                produto.Nome = reader.GetString("nomer_pro");
                produto.Codigo = reader.GetString("cod_pro");
                produto.Vencimento = reader.GetDateTime("venc_pro");
                produto.ValorCompra = reader.GetFloat("com_pro");
                produto.ValorVenda = reader.GetFloat("ven_pro");
                produto.Descricao = reader.GetString("des_pro-)");
            }
            return produto;
        }
        public List<ProdutoCadastrar> List() 
        {
            try
            {
                List<ProdutoCadastrar> list = new List<ProdutoCadastrar>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Fornecedor";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ProdutoCadastrar()
                    {
                        Id = reader.GetInt32("id_pro"),
                        Nome = reader.GetString("nome_pro"),
                        Codigo = reader.GetString("cod_pro"),
                        Vencimento = reader.GetDateTime("venc_pro"),
                        ValorCompra = reader.GetFloat("com_pro"),
                        ValorVenda = reader.GetFloat("ven_pro")
                    });
                }
                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            { 
                conn.Close();
            }
        }

        public void Delete(ProdutoCadastrar produtoCadastrar)
        {
            try 
            { 
                var query = conn.Query();
                query.CommandText= "Delete";

            } catch(Exception e)
            {
                throw e;
            }
            finally 
            {
                conn.Close();
            }
        }
        public void Insert(ProdutoCadastrar produtocadastrar)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO ProdutoCadastrar (id_pro, nome_pro, cod_pro, venc_pro, com_pro, ven_pro) VALUES(@nome, @codigo, @vencimento, @comprarProduto, @vendaProduto)";

                query.Parameters.AddWithValue("@nome",produtocadastrar.Nome);
                query.Parameters.AddWithValue("@codigo", produtocadastrar.Codigo);
                
            }
        }
        }

         
    }

