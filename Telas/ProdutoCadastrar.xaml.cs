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
    /// Lógica interna para ProdutoCadastrar.xaml
    /// </summary>
    public partial class ProdutoCadastrar : Window
    {
        public ProdutoCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Closing += FornecedorCadastrar_Closing;
        }

        private void CancelarCadastro_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à lista de produtos?", "Confirmação de Cancelamento",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                isClosingConfirmed = true; // Define a flag para true ao confirmar o fechamento
                ProdutoListar produtoListar = new ProdutoListar();
                produtoListar.Show();
                Close(); // Fecha a janela
            }

        }

        private void CadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();

            // Nome - obrigatório
            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                produto.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo de Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Código - obrigatório e deve ser numérico
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text) && int.TryParse(txtCodigo.Text, out int codigo))
            {
                produto.Codigo = codigo;
            }
            else
            {
                MessageBox.Show("O campo de Código é obrigatório e deve conter apenas números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Vencimento - obrigatório
            if (!string.IsNullOrWhiteSpace(dtpVencimento.Text))
            {
                DateTime vencimentoDate;
                if (DateTime.TryParse(dtpVencimento.Text, out vencimentoDate))
                {
                    produto.DataVenc = vencimentoDate.ToString("yyyy-MM-dd");
                }

            }
            else
            {
                MessageBox.Show("O campo de Vencimento é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Valor Compra - obrigatório e deve ser numérico
            if (!string.IsNullOrWhiteSpace(txtValorCom.Text) && double.TryParse(txtValorCom.Text, out double valorCompra))
            {
                produto.ValorCompra = valorCompra;
            }
            else
            {
                MessageBox.Show("O campo de Valor Compra é obrigatório e deve conter apenas números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Valor Venda - obrigatório e deve ser numérico
            if (!string.IsNullOrWhiteSpace(txtValorVen.Text) && double.TryParse(txtValorVen.Text, out double valorVenda))
            {
                produto.ValorVenda = valorVenda;
            }
            else
            {
                MessageBox.Show("O campo de Valor Venda é obrigatório e deve conter apenas números.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Descrição - obrigatória
            if (!string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                produto.Descricao = txtDescricao.Text;
            }
            else
            {
                MessageBox.Show("O campo de Descrição é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Se todos os campos obrigatórios estão preenchidos, você pode prosseguir com a inserção do produto
            var dao = new ProdutoDAO();
            dao.Insert(produto);

            MessageBox.Show("Produto cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpar os campos após a inserção
            txtNome.Text = "";
            txtCodigo.Text = "";
            dtpVencimento.SelectedDate = null;
            txtValorCom.Text = "";
            txtValorVen.Text = "";
            txtDescricao.Text = "";

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