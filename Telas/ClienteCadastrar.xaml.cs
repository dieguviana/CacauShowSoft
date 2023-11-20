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
            Closing += FornecedorCadastrar_Closing;
        }


        private void CadastrarCliente_Click(object sender, RoutedEventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();

            // Nome - obrigatório
            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                fornecedor.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo de Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // CPF - obrigatória
            if (!string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                fornecedor.CPF = txtCPF.Text;
            }
            else
            {
                MessageBox.Show("O campo de CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Contato - obrigatório
            if (!string.IsNullOrWhiteSpace(txtContato.Text))
            {
                fornecedor.Contato = txtContato.Text;
            }
            else
            {
                MessageBox.Show("O campo de Telefone é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Se todos os campos obrigatórios estão preenchidos, você pode prosseguir com a inserção do fornecedor
            var dao = new ClienteDAO();
            dao.Insert(Cliente);

            MessageBox.Show("Fornecedor inserido com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpar os campos após a inserção
            txtNome.Text = "";
            txtDataNasc.Text = "";
            txtCPF.Text = "";
            txtContato.Text = "";
            txtRG.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            txtCEP.Text = "";
            txtUF.Text = "";
            txtBairro.Text = "";
            txtMunicipio.Text = "";
        }


        private bool isClosingConfirmed = false;

        private void FornecedorCadastrar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosingConfirmed)
            {
                var result = MessageBox.Show("Qualquer informação registrada nessa tela será perdida. Deseja realmente fechar essa janela?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true; // Cancela o evento de fechamento da janela
                }
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à lista de fornecedores?", "Confirmação de Cancelamento",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                isClosingConfirmed = true; // Define a flag para true ao confirmar o fechamento
                FornecedorListar fornecedorListar = new FornecedorListar();
                fornecedorListar.Show();
                Close(); // Fecha a janela
            }
        }
    }
}
