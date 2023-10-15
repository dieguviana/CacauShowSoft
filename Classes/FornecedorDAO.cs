using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks; 

namespace NewAppCacauShow.Classes
{
    internal class FornecedorDAO
    {
        private static Conexao conn;

        public FornecedorDAO()
        {
            conn = new Conexao();
        }

        public Fornecedor GetById(int id)
        {
            var query = conn.Query();
            query.CommandText = "SELECT * FROM Fornecedor WHERE id_for = @id";

            query.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = query.ExecuteReader();
            var fornecedor = new Fornecedor();

            if (reader.Read())
            {
                fornecedor.IdFornecedor = reader.GetInt32("id_for");
                fornecedor.Nome = reader.GetString("nome_for");
                fornecedor.RazaoSocial = reader.GetString("razao_social_for");
                fornecedor.CNPJ = reader.GetString("cnpj_for");
                fornecedor.Telefone = reader.GetString("telefone_for");
                fornecedor.Endereco = reader.GetString("endereco_for");
                fornecedor.CEP = reader.GetString("cep_for");
                fornecedor.UF = reader.GetString("uf_for");
                fornecedor.Bairro = reader.GetString("bairro_for");
                fornecedor.Municipio = reader.GetString("municipio_for");
            }

            return fornecedor;
        }


        public List<Fornecedor> List()
        {
            try
            {
                List<Fornecedor> list = new List<Fornecedor>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Fornecedor";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Fornecedor()
                    {
                        IdFornecedor = reader.GetInt32("id_for"),
                        Nome = reader.GetString("nome_for"),
                        RazaoSocial = reader.GetString("razao_social_for"),
                        CNPJ = reader.GetString("cnpj_for"),
                        Telefone = reader.GetString("telefone_for"),
                        Endereco = reader.GetString("endereco_for"),
                        CEP = reader.GetString("cep_for"),
                        UF = reader.GetString("uf_for"),
                        Bairro = reader.GetString("bairro_for"),
                        Municipio = reader.GetString("municipio_for")
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

        public void Delete(Fornecedor fornecedor)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Fornecedor WHERE id_for = @id";

                query.Parameters.AddWithValue("@id", fornecedor.IdFornecedor);

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

        public void Insert(Fornecedor fornecedor)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO Fornecedor (nome_for, razao_social_for, cnpj_for, telefone_for, endereco_for, cep_for, uf_for, bairro_for, municipio_for) VALUES (@nome, @razaoSocial, @cnpj, @telefone, @endereco, @cep, @uf, @bairro, @municipio)";

                query.Parameters.AddWithValue("@nome", fornecedor.Nome);
                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                query.Parameters.AddWithValue("@cep", fornecedor.CEP);
                query.Parameters.AddWithValue("@uf", fornecedor.UF);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@municipio", fornecedor.Municipio);

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

        public void Update(Fornecedor fornecedor)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Fornecedor SET nome_for = @nome, razao_social_for = @razaoSocial, " +
                    "cnpj_for = @cnpj, telefone_for = @telefone, endereco_for = @endereco, cep_for = @cep, uf_for = @uf, " +
                    "bairro_for = @bairro, municipio_for = @municipio WHERE id_for = @id";

                query.Parameters.AddWithValue("@nome", fornecedor.Nome);
                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                query.Parameters.AddWithValue("@cep", fornecedor.CEP);
                query.Parameters.AddWithValue("@uf", fornecedor.UF);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@municipio", fornecedor.Municipio);
                query.Parameters.AddWithValue("@id", fornecedor.IdFornecedor);

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

    
