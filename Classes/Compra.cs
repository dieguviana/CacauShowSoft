using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class Compra
    {
        public int IdCompra { get; set; }

        public int IdPag { get; set; }

        public string DataHora { get; set; }

        public string Usuario { get; set; }

        public string Fornecedor { get; set; }

        public string Status { get; set; }

        public double ValorCompra { get; set; }

        public string Vencimento { get; set; }

        public string Forma { get; set; }
    }
}
