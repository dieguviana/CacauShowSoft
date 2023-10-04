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

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            VendaProduto vendaProduto = new VendaProduto();

            if (txtCodigoProduto.Text != "")
            {
                vendaProduto.Codigo = Convert.ToInt32(txtCodigoProduto.Text);
                if (txtQuantidade.Text != "")
                {
                    vendaProduto.Quantidade = Convert.ToDouble(txtQuantidade.Text);
                    var dao = new VendaDAO(); var vendaSelected = dao.ultimaVenda();
                    vendaProduto.Venda_fk = vendaSelected.IdVenda;

                    try
                    {
                        var dao2 = new VendaProdutoDAO();
                        dao2.Insert(vendaProduto);
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

                // Cliente - opcional (pode ser null se não informado)
                if (!string.IsNullOrWhiteSpace(txtClienteCPF.Text))
                {
                    recebimento.Cliente_cpf = txtClienteCPF.Text;
                }
                else
                {
                    recebimento.Cliente_cpf = ""; // Define como null se não informado
                }

                var dao = new VendaDAO();
                var vendaSelected = dao.ultimaVenda();
                recebimento.Venda_fk = vendaSelected.IdVenda;

                var dao2 = new RecebimentoDAO();
                dao2.Insert(recebimento);

                MessageBox.Show("Venda inserida com sucesso! O troco é de R$ " + recebimento.Troco + ".", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                var dao3 = new VendaDAO();
                dao3.Insert();

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
                VendaListar vendas = new VendaListar();
                vendas.Show();
                this.Close();
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
    }
}
