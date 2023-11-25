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
    /// Lógica interna para ProdutoListar.xaml
    /// </summary>
    public partial class ProdutoListar : Window
    {
        private int produtoSelecionadoId;

        public ProdutoListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new ProdutoDAO();

            try
            {
                DataGridProduto.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridProduto.SelectedItem != null)
            {
                Produto produtoSelecionado = (Produto)DataGridProduto.SelectedItem;
                produtoSelecionadoId = produtoSelecionado.IdProduto;
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
            ProdutoCadastrar produtoCadastrar = new ProdutoCadastrar();
            produtoCadastrar.Show();
            this.Close();
        }

        private void Detalhes_Click(object sender, RoutedEventArgs e)
        {
            var produtoSelected = DataGridProduto.SelectedItem as Produto;
            ProdutoConsultar produtoConsultar = new ProdutoConsultar(produtoSelected.IdProduto);
            produtoConsultar.Show();
            this.Close();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var produtoSelected = DataGridProduto.SelectedItem as Produto;

            var result = MessageBox.Show($"Deseja realmente remover o produto `{produtoSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new ProdutoDAO();
                    dao.Delete(produtoSelected);
                    Carregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
