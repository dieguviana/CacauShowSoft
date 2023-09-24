using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    class Venda
    {
        public int IdVenda { get; set; }

        public int IdRec { get; set; }
        
        public string DataHora { get; set; }
        
        public string Usuario { get; set; }
        
        public string Cliente { get; set; }
        
        public double Desconto { get; set; }

        public double ValorVenda { get; set; }

        public double ValorPago { get; set; }
        
        public string Forma { get; set; }
    }
}
