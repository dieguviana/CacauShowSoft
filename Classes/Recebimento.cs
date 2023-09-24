using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    class Recebimento
    {
        public double IdRec { get; set; }

        public double ValorVenda { get; set; }

        public double Desconto { get; set; }

        public double ValorPago { get; set; }

        public string Forma { get; set; }

        public int Venda_fk { get; set; }

        public string Cliente_cpf { get; set; }

        public double Troco { get; set; }
    }
}
