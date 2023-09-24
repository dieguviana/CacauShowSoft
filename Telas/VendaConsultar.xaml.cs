using NewAppCacauShow.Classes;
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
    /// Lógica interna para VendaConsultar.xaml
    /// </summary>
    public partial class VendaConsultar : Window
    {
        public VendaConsultar(int vendaId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            
            VendaDAO dao = new VendaDAO();

            dao.GetById(vendaId);
        }

        private void EditarVenda_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditar.Content.ToString() == "Editar")
            {
                btnEditar.Content = "Salvar";
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                btnEditar.Background = brush;


            }
            else if (btnEditar.Content.ToString() == "Salvar")
            {
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                btnEditar.Background = brush;
                btnEditar.Content = "Editar";
            }
        }

        private void CancelarVenda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
