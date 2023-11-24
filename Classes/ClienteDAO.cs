using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks; 

namespace NewAppCacauShow.Classes
{
    internal class ClienteDAO
    {
        private static Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
        }

        public Cliente GetById(int id)
        {
            var query = conn.Query();
            query.CommandText = "SELECT * FROM Cliente WHERE id_cli = @id";

            query.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = query.ExecuteReader();
            var cliente = new Cliente();

            if (reader.Read())
            {
                cliente.IdCliente = reader.GetInt32("id_cli");
                cliente.Nome = reader.GetString("nome_cli");
                cliente.DataNasc = reader.GetString("data_nasc_cli");
                cliente.CPF = reader.GetString("cpf_cli");
                cliente.RG = reader.GetString("rg_cli");
                cliente.Contato = reader.GetString("contato_cli");
                cliente.Email = reader.GetString("email_cli");
                cliente.Endereco = reader.GetString("endereco_cli");
                cliente.CEP = reader.GetString("cep_cli");
                cliente.UF = reader.GetString("uf_cli");
                cliente.Bairro = reader.GetString("bairro_cli");
                cliente.Municipio = reader.GetString("municipio_cli");
            }

            return cliente;
        }


        public List<Cliente> List()
        {
            try
            {
                List<Cliente> list = new List<Cliente>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Cliente";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Cliente()
                    {
                        IdCliente = reader.GetInt32("id_cli"),
                        Nome = reader.GetString("nome_cli"),
                        DataNasc = reader.GetString("data_nasc_cli"),
                        CPF = reader.GetString("cpf_cli"),
                        RG = reader.GetString("rg_cli"),
                        Contato = reader.GetString("contato_cli"),
                        Email = reader.GetString("email_cli"),
                        Endereco = reader.GetString("endereco_cli"),
                        CEP = reader.GetString("cep_cli"),
                        UF = reader.GetString("uf_cli"),
                        Bairro = reader.GetString("bairro_cli"),
                        Municipio = reader.GetString("municipio_cli")
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

        public void Delete(Cliente cliente)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Cliente WHERE id_cli = @id";

                query.Parameters.AddWithValue("@id", cliente.IdCliente);

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

        public void Insert(Cliente cliente)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO Cliente (nome_cli, data_nasc_cli, cpf_cli, rg_cli, contato_cli, email_cli, endereco_cli, cep_cli, uf_cli, bairro_cli, municipio_cli) VALUES (@nome, @datanasc, @cpf, @rg, @contato, @email, @endereco, @cep, @uf, @bairro, @municipio)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@datanasc", cliente.DataNasc);
                query.Parameters.AddWithValue("@cpf", cliente.CPF);
                query.Parameters.AddWithValue("@rg", cliente.RG);
                query.Parameters.AddWithValue("@contato", cliente.Contato);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@endereco", cliente.Endereco);
                query.Parameters.AddWithValue("@cep", cliente.CEP);
                query.Parameters.AddWithValue("@uf", cliente.UF);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@municipio", cliente.Municipio);

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

        public void Update(Cliente cliente)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Cliente SET nome_cli = @nome, data_nasc_cli = @datanasc, " +
                    "cpf_cli = @cpf, rg_cli = @rg, contato_cli = @contato, email_cli = @email, endereco_cli = @endereco, cep_cli = @cep, uf_cli = @uf, " +
                    "bairro_cli = @bairro, municipio_cli = @municipio WHERE id_cli = @id";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@datanasc", cliente.DataNasc);
                query.Parameters.AddWithValue("@cpf", cliente.CPF);
                query.Parameters.AddWithValue("@rg", cliente.RG);
                query.Parameters.AddWithValue("@contato", cliente.Contato);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@endereco", cliente.Endereco);
                query.Parameters.AddWithValue("@cep", cliente.CEP);
                query.Parameters.AddWithValue("@uf", cliente.UF);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@municipio", cliente.Municipio);
                query.Parameters.AddWithValue("@id", cliente.IdCliente);

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

    
