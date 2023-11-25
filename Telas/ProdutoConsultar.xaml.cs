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

namespace NewAppCacauShow.Telas { 
    /// <summary>
    /// Lógica interna para ProdutoConsultar.xaml
    /// </summary>
    public partial class ProdutoConsultar : Window
    {
        int identificadorProduto;
        public ProdutoConsultar( int produtoId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Closing += ProdutoCadastrar_Closing;

            var dao = new ProdutoDAO();

            // Obtém os dados do produto usando o ID fornecido como parâmetro
            Produto produtoSelected = dao.GetById(produtoId);
            identificadorProduto = produtoId;

            if (produtoSelected != null)
            {
                txtNome.Text = produtoSelected.Nome;
                txtCodigo.Text = produtoSelected.Codigo.ToString();
                txtVencimento.SelectedDate = DateTime.Parse(produtoSelected.DataVenc);
                txtValorCom.Text = produtoSelected.ValorCompra.ToString();
                txtValorVen.Text = produtoSelected.ValorVenda.ToString();
                txtDescricao.Text = produtoSelected.Descricao;
            }
            else
            {
                MessageBox.Show("Produto não encontrado", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool Editou = false;

        private void EditarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditar.Content.ToString() == "Editar")
            {
                btnEditar.Content = "Salvar";
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                btnEditar.Background = brush;

                txtNome.IsEnabled = true;
                txtCodigo.IsEnabled = true;
                txtVencimento.IsEnabled = true;
                txtValorCom.IsEnabled = true;
                txtValorVen.IsEnabled = true;
                txtDescricao.IsEnabled = true;

                Editou = true;
            }
            else if (btnEditar.Content.ToString() == "Salvar")
            {
                Produto produto = new Produto();

                produto.IdProduto = identificadorProduto;
                produto.Nome = txtNome.Text;
                produto.Codigo = int.Parse(txtCodigo.Text);
                produto.DataVenc = txtVencimento.SelectedDate.Value.ToString("yyyy-MM-dd");
                produto.ValorCompra = double.Parse(txtValorCom.Text);
                produto.ValorVenda = double.Parse(txtValorVen.Text);
                produto.Descricao = txtDescricao.Text;

                try
                {
                    var dao = new ProdutoDAO();
                    dao.Update(produto);

                    MessageBox.Show("Produto editado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    btnEditar.Background = brush;
                    btnEditar.Content = "Editar";

                    txtNome.IsEnabled = false;
                    txtCodigo.IsEnabled = false;
                    txtVencimento.IsEnabled = false;
                    txtValorCom.IsEnabled = false;
                    txtValorVen.IsEnabled = false;
                    txtDescricao.IsEnabled = false;
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
            var result = MessageBox.Show("Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à lista de produto?", "Confirmação de Cancelamento",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                isClosingConfirmed = true; // Define a flag para true ao confirmar o fechamento
                ProdutoListar produtoListar = new ProdutoListar();
                produtoListar.Show();
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
                        ProdutoListar produto = new ProdutoListar();
                        produto.Show();
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

        private void ProdutoCadastrar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

