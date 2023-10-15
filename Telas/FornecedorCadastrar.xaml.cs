using NewAppCacauShow.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Lógica interna para FornecedorCadastrar.xaml
    /// </summary>
    public partial class FornecedorCadastrar : Window
    {


        public FornecedorCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Closing += FornecedorCadastrar_Closing; 

        }


        private void CadastrarFornecedor_Click(object sender, RoutedEventArgs e)
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

                // Razão Social - obrigatória
                if (!string.IsNullOrWhiteSpace(txtRazaoSocial.Text))
                {
                    fornecedor.RazaoSocial = txtRazaoSocial.Text;
                }
                else
                {
                    MessageBox.Show("O campo de Razão Social é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                // CNPJ - obrigatório
                if (!string.IsNullOrWhiteSpace(txtCNPJ.Text))
                {
                    fornecedor.CNPJ = txtCNPJ.Text;
                }
                else
                {
                    MessageBox.Show("O campo de CNPJ é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Telefone - obrigatório
                if (!string.IsNullOrWhiteSpace(txtTelefone.Text))
                {
                    fornecedor.Telefone = txtTelefone.Text;
                }
                else
                {
                    MessageBox.Show("O campo de Telefone é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Endereço - obrigatório
                if (!string.IsNullOrWhiteSpace(txtEndereco.Text))
                {
                    fornecedor.Endereco = txtEndereco.Text;
                }
                else
                {
                    MessageBox.Show("O campo de Endereço é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // CEP - obrigatório
                if (!string.IsNullOrWhiteSpace(txtCEP.Text))
                {
                    fornecedor.CEP = txtCEP.Text;
                }
                else
                {
                    MessageBox.Show("O campo de CEP é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // UF - obrigatório
                if (!string.IsNullOrWhiteSpace(txtUF.Text))
                {
                    fornecedor.UF = txtUF.Text;
                }
                else
                {
                    MessageBox.Show("O campo de UF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Bairro - obrigatório
                if (!string.IsNullOrWhiteSpace(txtBairro.Text))
                {
                    fornecedor.Bairro = txtBairro.Text;
                }
                else
                {
                    MessageBox.Show("O campo de Bairro é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Município - obrigatório
                if (!string.IsNullOrWhiteSpace(txtMunicipio.Text))
                {
                    fornecedor.Municipio = txtMunicipio.Text;
                }
                else
                {
                    MessageBox.Show("O campo de Município é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Se todos os campos obrigatórios estão preenchidos, você pode prosseguir com a inserção do fornecedor
                var dao = new FornecedorDAO();
                dao.Insert(fornecedor);

                MessageBox.Show("Fornecedor inserido com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                // Limpar os campos após a inserção
                txtNome.Text = "";
                txtRazaoSocial.Text = "";
                txtCNPJ.Text = "";
                txtTelefone.Text = "";
                txtEndereco.Text = "";
                txtCEP.Text = "";
                txtUF.Text = "";
                txtBairro.Text = "";
                txtMunicipio.Text = "";
        
        }

        private void CancelarCadastro_Click(object sender, RoutedEventArgs e)
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

      




    }
}

