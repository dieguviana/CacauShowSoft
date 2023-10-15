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
    /// Lógica interna para FornecedorConsultar.xaml
    /// </summary>
    public partial class FornecedorConsultar : Window
    {
            int identificadorFornecedor;
            public FornecedorConsultar(int fornecedorId)
            {        
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.SingleBorderWindow;
                InitializeComponent();
                Closing += FornecedorCadastrar_Closing;

                var dao = new FornecedorDAO();

                // Obtém os dados do fornecedor usando o ID fornecido como parâmetro
                Fornecedor fornecedorSelected = dao.GetById(fornecedorId);
                identificadorFornecedor = fornecedorId;

                if (fornecedorSelected != null)
                {
                    txtNome.Text = fornecedorSelected.Nome;
                    txtRazaoSocial.Text = fornecedorSelected.RazaoSocial;
                    txtCNPJ.Text = fornecedorSelected.CNPJ;
                    txtTelefone.Text = fornecedorSelected.Telefone;
                    txtEndereco.Text = fornecedorSelected.Endereco;
                    txtCEP.Text = fornecedorSelected.CEP;
                    txtUF.Text = fornecedorSelected.UF;
                    txtBairro.Text = fornecedorSelected.Bairro;
                    txtMunicipio.Text = fornecedorSelected.Municipio;
                }
                else
                {
                    MessageBox.Show("Fornecedor não encontrado", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            bool Editou = false;

            private void EditarFornecedor_Click(object sender, RoutedEventArgs e)
            {
                if (btnEditar.Content.ToString() == "Editar")
                {
                    btnEditar.Content = "Salvar";
                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                    btnEditar.Background = brush;

                    txtNome.IsEnabled = true;
                    txtRazaoSocial.IsEnabled = true;
                    txtCNPJ.IsEnabled = true;
                    txtTelefone.IsEnabled = true;
                    txtEndereco.IsEnabled = true;
                    txtCEP.IsEnabled = true;
                    txtUF.IsEnabled = true;
                    txtBairro.IsEnabled = true;
                    txtMunicipio.IsEnabled = true;

                    Editou = true;
                }
                else if (btnEditar.Content.ToString() == "Salvar")
                {
                    Fornecedor fornecedor = new Fornecedor();

                    fornecedor.IdFornecedor = identificadorFornecedor;
                    fornecedor.Nome = txtNome.Text;
                    fornecedor.RazaoSocial = txtRazaoSocial.Text;
                    fornecedor.CNPJ = txtCNPJ.Text;
                    fornecedor.Telefone = txtTelefone.Text;
                    fornecedor.Endereco = txtEndereco.Text;
                    fornecedor.CEP = txtCEP.Text;
                    fornecedor.UF = txtUF.Text;
                    fornecedor.Bairro = txtBairro.Text;
                    fornecedor.Municipio = txtMunicipio.Text;

                    try
                    {
                        var dao = new FornecedorDAO();
                        dao.Update(fornecedor);

                        MessageBox.Show("Fornecedor editado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                        SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                        btnEditar.Background = brush;
                        btnEditar.Content = "Editar";

                        txtNome.IsEnabled = false;
                        txtRazaoSocial.IsEnabled = false;
                        txtCNPJ.IsEnabled = false;
                        txtTelefone.IsEnabled = false;
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
                            FornecedorListar fornecedores = new FornecedorListar();
                            fornecedores.Show();
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

