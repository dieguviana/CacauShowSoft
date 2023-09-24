using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace NewAppCacauShow.Classes
{
    internal class RecebimentoDAO
    {
        private static Conexao conn;

        public RecebimentoDAO()
        {
            conn = new Conexao();
        }

        public Recebimento Insert(Recebimento recebimento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Call InserirRecebimento(@valorVenda, @desconto, @valorPago, @forma, @venda_fk, @cliente_cpf)";

                query.Parameters.AddWithValue("@valorVenda", recebimento.ValorVenda);
                query.Parameters.AddWithValue("@desconto", recebimento.Desconto);
                query.Parameters.AddWithValue("@valorPago", recebimento.ValorPago);
                query.Parameters.AddWithValue("@forma", recebimento.Forma);
                query.Parameters.AddWithValue("@venda_fk", recebimento.Venda_fk);
                query.Parameters.AddWithValue("@cliente_cpf", recebimento.Cliente_cpf);
                int rowsAffected = query.ExecuteNonQuery();

                // Passo 2: Execute a consulta separada para obter o troco
                if (rowsAffected > 0)
                {
                    query.CommandText = "SELECT troco_rec FROM Recebimento WHERE id_ven_fk = @venda_fk";
                    using (MySqlDataReader reader = query.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recebimento.Troco = reader.GetDouble("troco_rec");
                        }
                    }
                }
                return recebimento;
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
