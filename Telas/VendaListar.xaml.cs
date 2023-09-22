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
    /// Lógica interna para VendaListar.xaml
    /// </summary>
    public partial class VendaListar : Window
    {
        private int vendaSelecionadaId;

        public VendaListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new VendaDAO();

            try
            {
                DataGridVendas.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridVendas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridVendas.SelectedItem != null)
            {
                Venda vendaSelecionada = (Venda)DataGridVendas.SelectedItem;
                vendaSelecionadaId = vendaSelecionada.IdVenda;
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

        private void Produto_Click(object sender, RoutedEventArgs e)
        {
            if (vendaSelecionadaId != 0)
            {
                VendaProdutoListar vendaProduto = new VendaProdutoListar(vendaSelecionadaId);
                vendaProduto.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma venda na lista.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Excluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
