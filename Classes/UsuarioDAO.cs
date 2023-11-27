using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text; 
using System.Threading.Tasks; 

namespace NewAppCacauShow.Classes
{
    internal class UsuarioDAO
    {
        private static Conexao conn;

        // Construtor que inicializa a conexão
        public UsuarioDAO()
        {
            conn = new Conexao();
        }

        // Obtém um usuário pelo ID
        public Usuario GetById(int id)
        {
            var query = conn.Query();
            query.CommandText = "SELECT * FROM Usuario WHERE id_usu = @id";

            query.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = query.ExecuteReader();
            var usuario = new Usuario();

            if (reader.Read())
            {
                usuario.IdUsuario = reader.GetInt32("id_usu");
                usuario.Nome = reader.GetString("nome_usu");
                usuario.Rg = reader.GetString("rg_usu");
                usuario.Cpf = reader.GetString("cpf_usu");
                usuario.Email = reader.GetString("email_usu");
                usuario.Funcao = reader.GetString("funcao_usu");
                usuario.Contato = reader.GetString("contato_usu");
                usuario.Endereco = reader.GetString("endereco_usu");
                usuario.Cep = reader.GetString("cep_usu");
                usuario.Uf = reader.GetString("uf_usu");
                usuario.Bairro = reader.GetString("bairro_usu");
                usuario.Municipio = reader.GetString("municipio_usu");
            }

            return usuario;
        }

        // Obtém uma lista de todos os usuários
        public List<Usuario> List()
        {
            try
            {
                List<Usuario> list = new List<Usuario>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Usuario";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Usuario()
                    {
                        IdUsuario = reader.GetInt32("id_usu"),
                        Nome = reader.GetString("nome_usu"),
                        Cpf = reader.GetString("cpf_usu"),
                        Rg = reader.GetString("rg_usu"),
                        Contato = reader.GetString("contato_usu"),
                        Funcao = reader.GetString("funcao_usu"),
                        Email = reader.GetString("email_usu"),
                        Endereco = reader.GetString("endereco_usu"),
                        Cep = reader.GetString("cep_usu"),
                        Uf = reader.GetString("uf_usu"),
                        Bairro = reader.GetString("bairro_usu"),
                        Municipio = reader.GetString("municipio_usu")
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

        // Exclui um usuário
        public void Delete(Usuario usuario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Usuario WHERE id_usu = @id";

                query.Parameters.AddWithValue("@id", usuario.IdUsuario);

                var resultado = query.ExecuteNonQuery();

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

        // Insere um novo usuário
        public void Insert(Usuario usuario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO Usuario (nome_usu, rg_usu, cpf_usu, email_usu, funcao_usu, contato_usu, endereco_usu, cep_usu, uf_usu, bairro_usu, municipio_usu) VALUES (@nome, @rg, @cpf, @email, @funcao, @contato, @endereco, @cep, @uf, @bairro, @municipio)";

                query.Parameters.AddWithValue("@nome", usuario.Nome);
                query.Parameters.AddWithValue("@rg", usuario.Rg);
                query.Parameters.AddWithValue("@cpf", usuario.Cpf);
                query.Parameters.AddWithValue("@email", usuario.Email);
                query.Parameters.AddWithValue("@funcao", usuario.Funcao);
                query.Parameters.AddWithValue("@contato", usuario.Contato);
                query.Parameters.AddWithValue("@endereco", usuario.Endereco);
                query.Parameters.AddWithValue("@cep", usuario.Cep);
                query.Parameters.AddWithValue("@uf", usuario.Uf);
                query.Parameters.AddWithValue("@bairro", usuario.Bairro);
                query.Parameters.AddWithValue("@municipio", usuario.Municipio);

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


        // Atualiza um usuário existente
        public void Update(Usuario usuario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Usuario SET nome_usu = @nome, " +
           "cpf_usu = @cpf, rg_usu = @rg, contato_usu = @contato, email_usu = @email, endereco_usu = @endereco, cep_usu = @cep, uf_usu = @uf, " +
           "bairro_usu = @bairro, municipio_usu = @municipio, funcao_usu = @funcao WHERE id_usu = @id";

                query.Parameters.AddWithValue("@nome", usuario.Nome);
                query.Parameters.AddWithValue("@cpf", usuario.Cpf);
                query.Parameters.AddWithValue("@rg", usuario.Rg);
                query.Parameters.AddWithValue("@contato", usuario.Contato);
                query.Parameters.AddWithValue("@email", usuario.Email);
                query.Parameters.AddWithValue("@endereco", usuario.Endereco);
                query.Parameters.AddWithValue("@cep", usuario.Cep);
                query.Parameters.AddWithValue("@uf", usuario.Uf);
                query.Parameters.AddWithValue("@bairro", usuario.Bairro);
                query.Parameters.AddWithValue("@municipio", usuario.Municipio);
                query.Parameters.AddWithValue("@funcao", usuario.Funcao);  
                query.Parameters.AddWithValue("@id", usuario.IdUsuario);


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
