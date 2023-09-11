using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NewAppCacauShow.Classes
{
    internal class VendasDAO : IDAO<Vendas>
    {
        private static Conexao conn;

        public VendasDAO()
        {
            conn = new Conexao();
        }

        public void Delete(Vendas t)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM venda WHERE id_ven = @id";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro não foi removido da base de dados. Verifique e tente novamente.");
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

        public Vendas GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Vendas t)
        {
            throw new NotImplementedException();
        }

        public List<Vendas> List()
        {
            try
            {
                List<Vendas> list = new List<Vendas>();

                var query = conn.Query();
                query.CommandText = "select * from Venda where (id_ven is not null)";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Vendas()
                    {
                        Id = reader.GetInt32("id_ven"),
                        Data = reader.GetDateTime("data_ven"),
                        Desconto = reader.GetFloat("desconto_ven"),
                        Hora = reader.GetTimeSpan("hora_ven"),
                        Valor = reader.GetFloat("valor_ven"),
                        Parcela = reader.GetFloat("parcela_ven"),
                        FormaPagamento = reader.GetString("form_pag_ven"),
                        Funcionario = reader.GetInt32("id_fun_fk"),
                        Cliente = reader.GetInt32("id_cli_fk")
                    });
                }

                return list;
            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Vendas t)
        {
            throw new NotImplementedException();
        }
    }
}
