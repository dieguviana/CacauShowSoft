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
    /// Lógica interna para ClienteCadastrar.xaml
    /// </summary>
    public partial class ClienteCadastrar : Window
    {
        public ClienteCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
        }


        private void CadastrarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();

            // Nome - obrigatório
            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                cliente.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo de Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // CPF - obrigatório
            if (!string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                cliente.CPF = txtCPF.Text;
            }
            else
            {
                MessageBox.Show("O campo de CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Contato - obrigatório
            if (!string.IsNullOrWhiteSpace(txtContato.Text))
            {
                cliente.Contato = txtContato.Text;
            }
            else
            {
                MessageBox.Show("O campo de Telefone é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Campos não obrigatórios (ajuste para evitar nulos)
            cliente.RG = string.Empty;
            cliente.Email = string.Empty;
            cliente.Endereco = string.Empty;
            cliente.CEP = string.Empty;
            cliente.UF = string.Empty;
            cliente.Bairro = string.Empty;
            cliente.Municipio = string.Empty;


            // Adicione outros campos não obrigatórios aqui

            // Se todos os campos obrigatórios estão preenchidos, você pode prosseguir com a inserção do cliente
            var dao = new ClienteDAO();
            dao.Insert(cliente);

            MessageBox.Show("Cliente inserido com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }


        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            ClienteListar clienteListar = new ClienteListar();
            clienteListar.Show();
            this.Close();
        }
    }
}
