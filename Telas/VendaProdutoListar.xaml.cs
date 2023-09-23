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
    /// Lógica interna para VendaProdutoListar.xaml
    /// </summary>
    public partial class VendaProdutoListar : Window
    {
        private int vendaId;

        public VendaProdutoListar(int vendaId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            this.vendaId = vendaId;
            InitializeComponent();
            Carregar(vendaId);
        }

        private void Carregar(int vendaId)
        {
            var dao = new VendaProdutoDAO();

            try
            {
                DataGridVendaProduto.ItemsSource = dao.List(vendaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var vendaProdutoSelected = DataGridVendaProduto.SelectedItem as VendaProduto;

            var result = MessageBox.Show($"Deseja realmente remover o produto `{vendaProdutoSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new VendaProdutoDAO();
                    dao.Delete(vendaProdutoSelected);
                    Carregar(vendaId);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            VendaListar vendasListar = new VendaListar();
            vendasListar.Show();
            this.Close();
        }
    }
}
