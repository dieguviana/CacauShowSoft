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
    /// Lógica interna para UsuarioConsultar.xaml
    /// </summary>
    public partial class UsuarioConsultar : Window
    {
        int identificadorUsuario;

        public UsuarioConsultar(int usuarioId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Closing += UsuarioCadastrar_Closing;

            var dao = new UsuarioDAO();

            // Obtém os dados do usuario usando o ID fornecido como parâmetro
            Usuario usuarioSelected = dao.GetById(usuarioId);
            identificadorUsuario = usuarioId;

            if (usuarioSelected != null)
            {
                txtNome.Text = usuarioSelected.Nome;
                txtCPF.Text = usuarioSelected.Cpf;
                txtRG.Text = usuarioSelected.Rg;
                txtContato.Text = usuarioSelected.Contato;
                txtEmail.Text = usuarioSelected.Email;
                txtEndereco.Text = usuarioSelected.Endereco;
                txtCEP.Text = usuarioSelected.Cep;
                txtUF.Text = usuarioSelected.Uf;
                txtBairro.Text = usuarioSelected.Bairro;
                txtMunicipio.Text = usuarioSelected.Municipio;

                // Configurar os CheckBox para refletir a função do usuário
                chkGerente.IsChecked = usuarioSelected.Funcao == "Gerente";
                chkAtendente.IsChecked = usuarioSelected.Funcao == "Atendente";
            }
            else
            {
                MessageBox.Show("Usuario não encontrado", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool Editou = false;

        private void EditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditar.Content.ToString() == "Editar")
            {
                btnEditar.Content = "Salvar";
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                btnEditar.Background = brush;

                txtNome.IsEnabled = true;
                txtCPF.IsEnabled = true;
                txtRG.IsEnabled = true;
                txtContato.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtEndereco.IsEnabled = true;
                txtCEP.IsEnabled = true;
                txtUF.IsEnabled = true;
                txtBairro.IsEnabled = true;
                txtMunicipio.IsEnabled = true;

                chkGerente.IsEnabled = true;
                chkAtendente.IsEnabled = true;

                Editou = true;
            }
            else if (btnEditar.Content.ToString() == "Salvar")
            {
                Usuario usuario = new Usuario();

                usuario.IdUsuario = identificadorUsuario;
                usuario.Nome = txtNome.Text;
                usuario.Cpf = txtCPF.Text;
                usuario.Rg = txtRG.Text;
                usuario.Contato = txtContato.Text;
                usuario.Email = txtEmail.Text;
                usuario.Endereco = txtEndereco.Text;
                usuario.Cep = txtCEP.Text;
                usuario.Uf = txtUF.Text;
                usuario.Bairro = txtBairro.Text;
                usuario.Municipio = txtMunicipio.Text;

                usuario.Funcao = (chkGerente.IsChecked == true) ? "Gerente" : (chkAtendente.IsChecked == true) ? "Atendente" : "";


                try
                {
                    var dao = new UsuarioDAO();
                    dao.Update(usuario);

                    MessageBox.Show("Usuario editado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    btnEditar.Background = brush;
                    btnEditar.Content = "Editar";

                    txtNome.IsEnabled = false;
                    txtCPF.IsEnabled = false;
                    txtRG.IsEnabled = false;
                    txtContato.IsEnabled = false;
                    txtEmail.IsEnabled = false;
                    txtEndereco.IsEnabled = false;
                    txtCEP.IsEnabled = false;
                    txtUF.IsEnabled = false;
                    txtBairro.IsEnabled = false;
                    txtMunicipio.IsEnabled = false;

                    chkGerente.IsEnabled = false;
                    chkAtendente.IsEnabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void VoltarListar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à lista de usuarioes?", "Confirmação de Cancelamento",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                isClosingConfirmed = true; // Define a flag para true ao confirmar o fechamento
                UsuarioListar usuarioListar = new UsuarioListar();
                usuarioListar.Show();
                Close(); // Fecha a janela
            }
        }

        private void FechandoJanela(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Editou == true)
            {
                var result = MessageBox.Show($"Algumas edições nessa tela podem ser perdidas caso continue. Deseja realmente fechar essa janela?", "Confirmação de Cancelamento",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        UsuarioListar usuarioes = new UsuarioListar();
                        usuarioes.Show();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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

    }
}