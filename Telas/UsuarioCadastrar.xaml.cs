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
    /// Lógica interna para UsuarioCadastrar.xaml
    /// </summary>
    public partial class UsuarioCadastrar : Window
    {
        public UsuarioCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Closing += UsuarioCadastrar_Closing;
        }

        private void CadastrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();

            // Nome - obrigatório
            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                usuario.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo de Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // CPF - obrigatório
            if (!string.IsNullOrWhiteSpace(txtCPF.Text))
            {
                usuario.Cpf = txtCPF.Text;
            }
            else
            {
                MessageBox.Show("O campo de CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Contato - obrigatório
            if (!string.IsNullOrWhiteSpace(txtContato.Text))
            {
                usuario.Contato = txtContato.Text;
            }
            else
            {
                MessageBox.Show("O campo de Telefone é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // RG - obrigatório
            if (!string.IsNullOrWhiteSpace(txtRG.Text))
            {
                usuario.Rg = txtRG.Text;
            }
            else
            {
                MessageBox.Show("O campo de RG é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Email - obrigatório
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                usuario.Email = txtEmail.Text;
            }
            else
            {
                MessageBox.Show("O campo de Email é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Endereco - obrigatório
            if (!string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                usuario.Endereco = txtEndereco.Text;
            }
            else
            {
                MessageBox.Show("O campo de Endereço é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // CEP - obrigatório
            if (!string.IsNullOrWhiteSpace(txtCEP.Text))
            {
                usuario.Cep = txtCEP.Text;
            }
            else
            {
                MessageBox.Show("O campo de CEP é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // UF - obrigatório
            if (!string.IsNullOrWhiteSpace(txtUF.Text))
            {
                usuario.Uf = txtUF.Text;
            }
            else
            {
                MessageBox.Show("O campo de UF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Bairro - obrigatório
            if (!string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                usuario.Bairro = txtBairro.Text;
            }
            else
            {
                MessageBox.Show("O campo de Bairro é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Municipio - obrigatório
            if (!string.IsNullOrWhiteSpace(txtMunicipio.Text))
            {
                usuario.Municipio = txtMunicipio.Text;
            }
            else
            {
                MessageBox.Show("O campo de Município é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Função - obrigatório (pelo menos uma opção deve ser selecionada)
            if (chkGerente.IsChecked == true || chkAtendente.IsChecked == true)
            {
                usuario.Funcao = "";
                if (chkGerente.IsChecked == true)
                {
                    usuario.Funcao += "Gerente ";
                }
                if (chkAtendente.IsChecked == true)
                {
                    usuario.Funcao += "Atendente";
                }
            }
            else
            {
                MessageBox.Show("Selecione pelo menos uma função.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Se todos os campos obrigatórios estão preenchidos, você pode prosseguir com a inserção do usuário
            var dao = new UsuarioDAO();
            dao.Insert(usuario);

            MessageBox.Show("Usuário inserido com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpar os campos após a inserção
            txtNome.Text = "";
            txtCPF.Text = "";
            txtContato.Text = "";
            txtRG.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            txtCEP.Text = "";
            txtUF.Text = "";
            txtBairro.Text = "";
            txtMunicipio.Text = "";

            // Limpar checkboxes de função
            chkGerente.IsChecked = false;
            chkAtendente.IsChecked = false;
        }


        private bool isClosingConfirmed = false;

        private void UsuarioCadastrar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
            var result = MessageBox.Show("Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à lista de clientees?", "Confirmação de Cancelamento",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                isClosingConfirmed = true; // Define a flag para true ao confirmar o fechamento
                UsuarioListar usuarioListar = new UsuarioListar();
                usuarioListar.Show();
                Close(); // Fecha a janela
            }
        }

    }
}
