using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class CompraProduto
    {
        public int IdCompraProduto { get; set; }

        public int Codigo { get; set; }

        public double Quantidade { get; set; }

        public string Produto { get; set; }

        public double ValorUnitario { get; set; }

        public int Compra_fk { get; set; }

        public double Subtotal { get; set; }

        public double ValorTotal { get; set; }
    }
}
