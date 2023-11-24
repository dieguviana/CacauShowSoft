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
    /// Lógica interna para ClienteListar.xaml
    /// </summary>
    public partial class ClienteListar : Window
    {
        private int clienteSelecionadoId;

        public ClienteListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new ClienteDAO();


                DataGridCliente.ItemsSource = dao.List();
            
        
        }

        private void DataGridCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridCliente.SelectedItem != null)
            {
                Cliente clienteSelecionado = (Cliente)DataGridCliente.SelectedItem;
                clienteSelecionadoId = clienteSelecionado.IdCliente;
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
            ClienteCadastrar clienteCadastrar = new ClienteCadastrar();
            clienteCadastrar.Show();
            this.Close();
        }

        private void Detalhes_Click(object sender, RoutedEventArgs e)
        {
            var clienteSelected = DataGridCliente.SelectedItem as Cliente;
            ClienteConsultar clienteConsultar = new ClienteConsultar(clienteSelected.IdCliente);
            clienteConsultar.Show();
            this.Close();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var clienteSelected = DataGridCliente.SelectedItem as Cliente;

            var result = MessageBox.Show($"Deseja realmente remover o cliente `{clienteSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new ClienteDAO();
                    dao.Delete(clienteSelected);
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
