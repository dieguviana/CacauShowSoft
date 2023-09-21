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
                query.CommandText = "select " +
                "Venda.id_ven, " +
                "Venda.data_hora_ven, " +
                "Usuario.nome_usu, " +
                "Cliente.nome_cli, " +
                "Recebimento.valor_venda_rec, " +
                "Recebimento.desconto_rec, " +
                "Recebimento.valor_entrada_rec, " +
                "Recebimento.forma_pagamento_rec " +
                "from " +
                "Venda, Usuario, Cliente, Recebimento " +
                "where " +
                "(Usuario.id_usu = Venda.id_usu_fk) and " +
                "(Cliente.id_cli = Venda.id_cli_fk) and " +
                "(Recebimento.id_ven_fk = Venda.id_ven);";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Vendas()
                    {
                        Id = reader.GetInt32("id_ven"),
                        DataHora = reader.GetDateTime("data_hora_ven"),
                        Usuario = reader.GetString("nome_usu"),
                        Cliente = reader.GetString("nome_cli"),
                        ValorVenda = reader.GetDouble("valor_venda_rec"),
                        Desconto = reader.GetDouble("desconto_rec"),
                        ValorEntrada = reader.GetDouble("valor_entrada_rec"),
                        FormaPagamento = reader.GetString("forma_pagamento_rec")
                    });
                }

                return list;
            }//catch (Exception ex)
            //{
                //throw ex;
            //}
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