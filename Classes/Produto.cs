using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAppCacauShow.Classes
{
    internal class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public string DataVenc { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public string Descricao { get; set; }
  
    }
}
