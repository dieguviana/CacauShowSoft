using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class Venda
    {
        public int IdVenda { get; set; }
        
        public DateTime DataHora { get; set; }
        
        public string Usuario { get; set; }
        
        public string Cliente { get; set; }
        
        public double ValorVenda { get; set; }
        
        public double Desconto { get; set; }
        
        public double ValorEntrada { get; set; }
        
        public string FormaPagamento { get; set; }
    }
}
