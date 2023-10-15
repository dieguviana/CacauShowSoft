using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class Pagamento
    {
        public double IdPag { get; set; }

        public double ValorCompra { get; set; }

        public string Status { get; set; }

        public string Vencimento { get; set; }

        public string Forma { get; set; }

        public int Compra_fk { get; set; }

        public string Fornecedor_nome { get; set; }
    }
}
