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
    /// Lógica interna para VendaConsultar.xaml
    /// </summary>
    public partial class VendaConsultar : Window
    {
        int identificadorVenda;
        int identificadorRecebimento;
        public VendaConsultar(int vendaId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            var dao = new VendaDAO();

            Venda vendaSelected = new Venda();
            vendaSelected.IdVenda = vendaId;
            identificadorVenda = vendaId;
            vendaSelected = dao.GetById(vendaSelected); // Obtenha os dados da venda
            identificadorRecebimento = vendaSelected.IdRec;

            if (vendaSelected != null)
            {
                // Atualize os campos da interface com os valores da venda
                string formaPagamento = vendaSelected.Forma;
                foreach (ComboBoxItem item in cmbForma.Items)
                {
                    if (item.Content.ToString() == formaPagamento)
                    {
                        cmbForma.SelectedItem = item;
                        break; // Saia do loop assim que encontrar uma correspondência
                    }
                }
                txtClienteCPF.Text = vendaSelected.Cliente;
                txtValorVenda.Text = vendaSelected.ValorVenda.ToString(); // Converta para string
                txtDesconto.Text = vendaSelected.Desconto.ToString(); // Converta para string
                txtValorPago.Text = vendaSelected.ValorPago.ToString(); // Converta para string
                Carregar(vendaId);
            }
            else
            {
                MessageBox.Show("Venda não encontrada", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        bool Editou = false;

        private void EditarVenda_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditar.Content.ToString() == "Editar")
            {
                btnEditar.Content = "Salvar";
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                btnEditar.Background = brush;

                txtCodigoProduto.IsEnabled = true;
                txtQuantidade.IsEnabled = true;
                btnAdd.IsEnabled = true;
                cmbForma.IsEnabled = true;
                DataGridVendaProduto.IsEnabled = true;
                txtClienteCPF.IsEnabled = true;
                txtDesconto.IsEnabled = true;
                txtValorPago.IsEnabled = true;

                Editou = true;
            }
            else if (btnEditar.Content.ToString() == "Salvar")
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

                    recebimento.Venda_fk = identificadorVenda;
                    recebimento.IdRec = identificadorRecebimento;

                    var dao2 = new RecebimentoDAO();
                    dao2.Update(recebimento);

                    MessageBox.Show("Venda editada com sucesso! O novo troco, de acordo com a última edição, é de R$ " + recebimento.Troco + ".", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    var dao3 = new VendaDAO();
                    dao3.Insert();

                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    btnEditar.Background = brush;
                    btnEditar.Content = "Editar";

                    txtCodigoProduto.IsEnabled = false;
                    txtQuantidade.IsEnabled = false;
                    btnAdd.IsEnabled = false;
                    cmbForma.IsEnabled = false;
                    DataGridVendaProduto.IsEnabled = false;
                    txtClienteCPF.IsEnabled = false;
                    txtValorVenda.IsEnabled = false;
                    txtDesconto.IsEnabled = false;
                    txtValorPago.IsEnabled = false;

                    Editou = false;
                }
                else
                {
                    MessageBox.Show("Selecione a forma de pagamento.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vendaProduto = DataGridVendaProduto.SelectedItem as VendaProduto;
                vendaProduto.Venda_fk = identificadorVenda;

                var result = MessageBox.Show($"Deseja realmente remover o produto `{vendaProduto.Produto}`?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var dao2 = new VendaProdutoDAO();
                    dao2.Delete(vendaProduto);
                    txtValorVenda.Text = Convert.ToString(vendaProduto.ValorTotal);
                    Carregar(vendaProduto.Venda_fk);
                }
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
                    vendaProduto.Venda_fk = identificadorVenda;

                    try
                    {
                        var dao = new VendaProdutoDAO();
                        dao.Insert(vendaProduto);
                        txtValorVenda.Text = Convert.ToString(vendaProduto.ValorTotal);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    txtCodigoProduto.Text = "";
                    txtQuantidade.Text = "";

                    Carregar(vendaProduto.Venda_fk);
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

        private bool jaFoi = false;

        private void VoltarListar_Click(object sender, RoutedEventArgs e)
        {
            if (Editou == true)
            {
                jaFoi = true;
                var result = MessageBox.Show($"Algumas edições nessa tela podem ser perdidas caso continue. Deseja realmente voltar à listar vendas?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
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
            else
            {
                VendaListar vendas = new VendaListar();
                vendas.Show();
                this.Close();
            }
        }

        private void FechandoJanela(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Editou == true)
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
}
