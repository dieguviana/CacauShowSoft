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
    /// Lógica interna para ClienteConsultar.xaml
    /// </summary>
    public partial class ClienteConsultar : Window
    {
        int identificadorCliente;

        public ClienteConsultar(int clienteId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();

            var dao = new ClienteDAO();

            // Obtém os dados do cliente usando o ID fornecido como parâmetro
            Cliente clienteSelected = dao.GetById(clienteId);
            identificadorCliente = clienteId;

            if (clienteSelected != null)
            {
                txtNome.Text = clienteSelected.Nome;
                txtCPF.Text = clienteSelected.CPF;
                txtRG.Text = clienteSelected.RG;
                txtContato.Text = clienteSelected.Contato;
                txtEmail.Text = clienteSelected.Email;
                txtEndereco.Text = clienteSelected.Endereco;
                txtCEP.Text = clienteSelected.CEP;
                txtUF.Text = clienteSelected.UF;
                txtBairro.Text = clienteSelected.Bairro;
                txtMunicipio.Text = clienteSelected.Municipio;
            }
            else
            {
                MessageBox.Show("Cliente não encontrado", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool Editou = false;

        private void EditarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditar.Content.ToString() == "Editar")
            {
                btnEditar.Content = "Salvar";
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                btnEditar.Background = brush;

                txtNome.IsEnabled = true;
                txtDataNasc.IsEnabled = true;
                txtCPF.IsEnabled = true;
                txtRG.IsEnabled = true;
                txtContato.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtEndereco.IsEnabled = true;
                txtCEP.IsEnabled = true;
                txtUF.IsEnabled = true;
                txtBairro.IsEnabled = true;
                txtMunicipio.IsEnabled = true;

                Editou = true;
            }
            else if (btnEditar.Content.ToString() == "Salvar")
            {
                Cliente cliente = new Cliente();

                cliente.IdCliente = identificadorCliente;
                cliente.Nome = txtNome.Text;
                cliente.CPF = txtCPF.Text;
                cliente.RG = txtRG.Text;
                cliente.Contato = txtContato.Text;
                cliente.Email = txtEmail.Text;
                cliente.Endereco = txtEndereco.Text;
                cliente.CEP = txtCEP.Text;
                cliente.UF = txtUF.Text;
                cliente.Bairro = txtBairro.Text;
                cliente.Municipio = txtMunicipio.Text;

                try
                {
                    var dao = new ClienteDAO();
                    dao.Update(cliente);

                    MessageBox.Show("Cliente editado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    btnEditar.Background = brush;
                    btnEditar.Content = "Editar";

                    txtNome.IsEnabled = false;
                    txtDataNasc.IsEnabled = false;
                    txtCPF.IsEnabled = false;
                    txtRG.IsEnabled = false;
                    txtContato.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    txtEndereco.IsEnabled = false;
                    txtCEP.IsEnabled = false;
                    txtUF.IsEnabled = false;
                    txtBairro.IsEnabled = false;
                    txtMunicipio.IsEnabled = false;

                    Editou = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void VoltarListar_Click(object sender, RoutedEventArgs e)
        {
            ClienteListar clienteListar = new ClienteListar();
            clienteListar.Show();
            this.Close();
        }
    }
}
