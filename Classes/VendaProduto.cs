using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    class VendaProduto
    {
        public int IdVendaProduto { get; set; }

        public int Codigo { get; set; }

        public double Quantidade { get; set; }

        public string Produto { get; set; }

        public double ValorUnitario { get; set; }

        public int Venda_fk { get; set; }

        public double Subtotal { get; set; }

        public double ValorTotal { get; set; }
    }
}
