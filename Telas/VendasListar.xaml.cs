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
using NewAppCacauShow.Classes;

namespace NewAppCacauShow.Telas
{
    /// <summary>
    /// Lógica interna para VendasListar.xaml
    /// </summary>
    public partial class VendasListar : Window
    {
        public VendasListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Loaded += VendasListar_Loaded;
        }

        private void VendasListar_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                var dao = new VendasDAO();

                DataGridVendas.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
