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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewAppCacauShow.Telas
{
    /// <summary>
    /// Lógica interna para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
        }

        private void Vendas_Click(object sender, RoutedEventArgs e)
        {
            VendasListar vendas = new VendasListar();
            vendas.Show();
            this.Close();
        }

        private void Estoque_Click(object sender, RoutedEventArgs e)
        {
            EstoqueListar estoque = new EstoqueListar();
            estoque.Show();
            this.Close();
        }

        private void Produtos_Click(object sender, RoutedEventArgs e)
        {
            ProdutosListar produto = new ProdutosListar();
            produto.Show();
            this.Close();
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesListar clientes = new ClientesListar();
            clientes.Show();
            this.Close();
        }

        private void Funcionarios_Click(object sender, RoutedEventArgs e)
        {
            FuncionariosListar funcionarios = new FuncionariosListar();
            funcionarios.Show();
            this.Close();
        }

        private void Fornecedores_Click(object sender, RoutedEventArgs e)
        {
            FornecedoresListar fornecedores = new FornecedoresListar();
            fornecedores.Show();
            this.Close();
        }
    }
}
