using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    class Vendas
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public float Desconto { get; set; }

        public TimeSpan Hora { get; set; }

        public float Valor { get; set; }

        public double Parcela { get; set; }
        
        public string FormaPagamento { get; set; }
        
        public int Funcionario { get; set; }

        public int Cliente { get; set; }
    }
}
