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
        private int vendaSelecionadaId;

        public VendasListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new VendasDAO();
            
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
                Vendas vendaSelecionada = (Vendas)DataGridVendas.SelectedItem;
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
