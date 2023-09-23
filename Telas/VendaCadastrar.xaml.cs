using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NewAppCacauShow.Telas
{
    /// <summary>
    /// Lógica interna para VendaCadastrar.xaml
    /// </summary>
    public partial class VendaCadastrar : Window
    {
        public VendaCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelarVenda_Click(object sender, RoutedEventArgs e)
        {
            VendaListar vendas = new VendaListar();
            vendas.Show();
            this.Close();
        }

        private void FinalizarVenda_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
