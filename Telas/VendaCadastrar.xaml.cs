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
    /// Lógica interna para VendaCadastrar.xaml
    /// </summary>
    public partial class VendaCadastrar : Window
    {
        public VendaCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
        }

        private void Carregar(int vendaId)
        {
            var dao = new VendaProdutoDAO();

            try
            {
                DataGridVendaProduto.ItemsSource = dao.List(vendaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica se o texto inserido é um número inteiro
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true; // Ignora a entrada se não for um número inteiro
            }
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            VendaProduto vendaProduto = new VendaProduto();
            var dao = new VendaProdutoDAO();

            if (txtCodigoProduto.Text != "")
            {
                vendaProduto.Codigo = Convert.ToInt32(txtCodigoProduto.Text);

                if (dao.ProdutoExiste(vendaProduto.Codigo))
                {
                    if (txtQuantidade.Text != "")
                    {
                        vendaProduto.Quantidade = Convert.ToDouble(txtQuantidade.Text);
                        var dao2 = new VendaDAO(); var vendaSelected = dao2.ultimaVenda();
                        vendaProduto.Venda_fk = vendaSelected.IdVenda;

                        try
                        {
                            var dao3 = new VendaProdutoDAO();
                            dao3.Insert(vendaProduto);
                            txtValorVenda.Text = Convert.ToString(vendaProduto.ValorTotal);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        txtCodigoProduto.Text = "";
                        txtQuantidade.Text = "";

                        Carregar(vendaSelected.IdVenda);
                    }
                    else
                    {
                        MessageBox.Show("Insira a quantidade de produtos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("O produto com o código especificado não existe.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Insira o código do produto", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FinalizarVenda_Click(object sender, RoutedEventArgs e)
        {
            Recebimento recebimento = new Recebimento();

            if (DataGridVendaProduto.ItemsSource == null || DataGridVendaProduto.Items.Count == 0)
            {
                MessageBox.Show("Adicione produtos à venda antes de finalizá-la.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Sai da função se não houver produtos cadastrados
            }

            if (!string.IsNullOrWhiteSpace(txtValorVenda.Text))
            {
                recebimento.ValorVenda = Convert.ToDouble(txtValorVenda.Text);
            }

            // Desconto - opcional (será zero se não informado)
            if (!string.IsNullOrWhiteSpace(txtDesconto.Text))
            {
                recebimento.Desconto = Convert.ToDouble(txtDesconto.Text);
            }
            else
            {
                recebimento.Desconto = 0; // Define como zero se não informado
            }

            // Valor pago - obrigatório
            if (!string.IsNullOrWhiteSpace(txtValorPago.Text))
            {
                recebimento.ValorPago = Convert.ToDouble(txtValorPago.Text);
            }
            else
            {
                recebimento.ValorPago = Convert.ToDouble(txtValorVenda.Text);
            }

            // Forma de pagamento - obrigatório
            var selectedItem = cmbForma.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string textoSelecionado = selectedItem.Content.ToString();
                recebimento.Forma = textoSelecionado;

                recebimento.Cliente_cpf = txtClienteCPF.Text;

                var dao = new RecebimentoDAO();
                var dao2 = new VendaDAO();
                var vendaSelected = dao2.ultimaVenda();
                recebimento.Venda_fk = vendaSelected.IdVenda;
                var dao3 = new RecebimentoDAO();
                dao3.Insert(recebimento);

                MessageBox.Show("Venda inserida com sucesso! O troco é de R$ " + recebimento.Troco + ".", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                var dao4 = new VendaDAO();
                dao4.Insert();

                // Limpe os campos
                txtValorVenda.Text = "";
                txtDesconto.Text = "";
                txtValorPago.Text = "";
                cmbForma.SelectedItem = null;
                txtClienteCPF.Text = "";
                DataGridVendaProduto.ItemsSource = null;
            }
            else
            {
                MessageBox.Show("Selecione a forma de pagamento.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vendaProduto = DataGridVendaProduto.SelectedItem as VendaProduto;
                var dao = new VendaDAO(); var vendaSelected = dao.ultimaVenda();
                vendaProduto.Venda_fk = vendaSelected.IdVenda;

                var result = MessageBox.Show($"Deseja realmente remover o produto `{vendaProduto.Produto}`?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var dao2 = new VendaProdutoDAO();
                    dao2.Delete(vendaProduto);
                    Carregar(vendaSelected.IdVenda);
                    txtValorVenda.Text = Convert.ToString(vendaProduto.ValorTotal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool jaFoi = false;

        private void CancelarVenda_Click(object sender, RoutedEventArgs e)
        {
            if (!jaFoi)
            {
                jaFoi = true;
                var dao = new VendaDAO();

                var vendaSelected = dao.ultimaVenda();

                var result = MessageBox.Show($"Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à listar vendas?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        var dao2 = new VendaDAO();
                        dao2.Delete(vendaSelected);
                        VendaListar vendas = new VendaListar();
                        vendas.Show();
                        this.Close();
                    }
                    else
                    {
                        jaFoi = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FechandoJanela(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!jaFoi)
            {
                var dao = new VendaDAO();

                var vendaSelected = dao.ultimaVenda();

                var result = MessageBox.Show($"Qualquer informação registrada nessa tela será perdida. Deseja realmente fechar essa janela?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        var dao2 = new VendaDAO();
                        dao2.Delete(vendaSelected);
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

        private void CadastrarCliente_Click(object sender, RoutedEventArgs e)
        {
            ClienteCadastrar clienteCadastrar = new ClienteCadastrar();
            clienteCadastrar.Show();
            this.Close();
        }

        private void TxtClienteCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cpf2 = txtClienteCPF.Text.ToString();
            if (cpf2.Length == 14)
            {
                var dao = new RecebimentoDAO();
                if (!dao.ClienteExiste(cpf2))
                {
                    MessageBox.Show("O cliente com o CPF especificado não existe no sistema.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtClienteCPF.Text = "";
                }
            }

            string cpf = txtClienteCPF.Text.Replace(".", "").Replace("-", "");

            if (cpf.Length <= 11)
            {
                if (cpf.Length >= 3)
                    cpf = cpf.Insert(3, ".");

                if (cpf.Length >= 7)
                    cpf = cpf.Insert(7, ".");

                if (cpf.Length >= 11)
                    cpf = cpf.Insert(11, "-");

                txtClienteCPF.Text = cpf;
                txtClienteCPF.SelectionStart = cpf.Length; // Mantém o cursor na posição correta
            }
            else
            {
                // Limita o texto do TextBox a 11 caracteres
                txtClienteCPF.Text = cpf.Substring(0, 11);
                txtClienteCPF.SelectionStart = 11;
            }
        }

        private void txtCodigoProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;

                var codigoProduto = Convert.ToInt32(txtCodigoProduto.Text);


                VendaProdutoDAO vendaProduto = new VendaProdutoDAO();
                bool produtoExiste = vendaProduto.ProdutoExiste(codigoProduto);

                if (produtoExiste)
                {
                    txtQuantidade.Focus();
                }
                else
                {
                    MessageBox.Show("Produto não encontrado no banco de dados.");

                    txtCodigoProduto.Text = "";
                    txtCodigoProduto.Focus();
                }
            }
        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true; // Para evitar que o caractere de nova linha seja inserido no campo de texto

                AdicionarProduto_Click(sender, e); // Chama a mesma função que o botão Add

                txtCodigoProduto.Text = "";
                txtQuantidade.Text = "";

                // Definir o foco de volta para o campo txtCodigoProduto para continuar adicionando itens
                txtCodigoProduto.Focus();
            }
        }
    }
}