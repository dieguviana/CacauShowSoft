using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class PagamentoDAO
    {
        private static Conexao conn;

        public PagamentoDAO()
        {
            conn = new Conexao();
        }

        public Pagamento Insert(Pagamento pagamento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Call InserirPagamento(@valorCompra, @status_, @vencimento, @forma, @compra_fk, @fornecedor_cnpj)";

                query.Parameters.AddWithValue("@valorCompra", pagamento.ValorCompra);
                query.Parameters.AddWithValue("@status_", pagamento.Status);
                query.Parameters.AddWithValue("@vencimento", pagamento.Vencimento);
                query.Parameters.AddWithValue("@forma", pagamento.Forma);
                query.Parameters.AddWithValue("@compra_fk", pagamento.Compra_fk);
                query.Parameters.AddWithValue("@fornecedor_cnpj", pagamento.Fornecedor_cnpj);
                int rowsAffected = query.ExecuteNonQuery();

               
                return pagamento;
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

        public Pagamento Update(Pagamento pagamento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "Call UpdatePagamento(@id, @valorCompra, @status_, @vencimento, @forma, @fornecedor_cnpj)";

                query.Parameters.AddWithValue("@id", pagamento.IdPag);
                query.Parameters.AddWithValue("@valorCompra", pagamento.ValorCompra);
                query.Parameters.AddWithValue("@status_", pagamento.Status);
                query.Parameters.AddWithValue("@vencimento", pagamento.Vencimento);
                query.Parameters.AddWithValue("@forma", pagamento.Forma);
                query.Parameters.AddWithValue("@compra_fk", pagamento.Compra_fk);
                query.Parameters.AddWithValue("@fornecedor_cnpj", pagamento.Fornecedor_cnpj);
                int rowsAffected = query.ExecuteNonQuery();

                
                return pagamento;
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