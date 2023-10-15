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
    /// Lógica interna para CompraConsultar.xaml
    /// </summary>
    public partial class CompraConsultar : Window
    {
        int identificadorCompra;
        int identificadorPagamento;
        public CompraConsultar(int compraId)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            var dao = new CompraDAO();

            Compra compraSelected = new Compra();
            compraSelected.IdCompra = compraId;
            identificadorCompra = compraId;
            compraSelected = dao.GetById(compraSelected); // Obtenha os dados da compra
            identificadorPagamento = compraSelected.IdPag;

            if (compraSelected != null)
            {
                // Atualize os campos da interface com os valores da compra
                string formaPagamento = compraSelected.Forma;
                foreach (ComboBoxItem item in cmbForma.Items)
                {
                    if (item.Content.ToString() == formaPagamento)
                    {
                        cmbForma.SelectedItem = item;
                        break; // Saia do loop assim que encontrar uma correspondência
                    }
                }
               
                txtValorCompra.Text = compraSelected.ValorCompra.ToString(); // Converta para string
                cbStatus.Text = compraSelected.Status.ToString(); // Converta para string
                dtpVencimento.Text = compraSelected.Vencimento.ToString(); // Converta para string
                Carregar(compraId);
            }
            else
            {
                MessageBox.Show("Compra não encontrada", "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Carregar(int compraId)
        {
            var dao = new CompraProdutoDAO();

            try
            {
                DataGridCompraProduto.ItemsSource = dao.List(compraId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool Editou = false;

        private void EditarCompra_Click(object sender, RoutedEventArgs e)
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
                DataGridCompraProduto.IsEnabled = true;
                cbFornecedor.IsEnabled = true;
                cbStatus.IsEnabled = true;
                dtpVencimento.IsEnabled = true;

                Editou = true;
            }
            else if (btnEditar.Content.ToString() == "Salvar")
            {

                Pagamento pagamento = new Pagamento();

                if (DataGridCompraProduto.ItemsSource == null || DataGridCompraProduto.Items.Count == 0)
                {
                    MessageBox.Show("Adicione produtos à venda antes de finalizá-la.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Sai da função se não houver produtos cadastrados
                }

                if (!string.IsNullOrWhiteSpace(txtValorCompra.Text))
                {
                    pagamento.ValorCompra = Convert.ToDouble(txtValorCompra.Text);
                }

                // Vencimento - obrigatório
                if (!string.IsNullOrWhiteSpace(dtpVencimento.Text))
                {
                    DateTime vencimentoDate;
                    if (DateTime.TryParse(dtpVencimento.Text, out vencimentoDate))
                    {
                        pagamento.Vencimento = vencimentoDate.ToString("yyyy-MM-dd");
                    }

                }
                else
                {
                    MessageBox.Show("O campo de Vencimento é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Status - obrigatório
                if (!string.IsNullOrWhiteSpace(cbStatus.Text))
                {
                    pagamento.Status = Convert.ToString(cbStatus.Text);
                }
                else
                {
                    MessageBox.Show("O campo de Status é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Forma de pagamento - obrigatório
                var selectedItem = cmbForma.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    string textoSelecionado = selectedItem.Content.ToString();
                    pagamento.Forma = textoSelecionado;

                    // Obter o nome do fornecedor selecionado no ComboBox
                    string fornecedorSelecionado = cbFornecedor.SelectedValue as string;
                    if (!string.IsNullOrWhiteSpace(fornecedorSelecionado))
                    {
                        pagamento.Fornecedor_nome = fornecedorSelecionado;
                    }
                    else
                    {
                        MessageBox.Show("Selecione um fornecedor válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    pagamento.Compra_fk = identificadorCompra;
                    pagamento.IdPag = identificadorPagamento;

                    var dao2 = new PagamentoDAO();
                    dao2.Update(pagamento);

                    MessageBox.Show("Compra editada com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    var dao3 = new CompraDAO();
                    dao3.Insert();

                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    btnEditar.Background = brush;
                    btnEditar.Content = "Editar";

                    txtCodigoProduto.IsEnabled = false;
                    txtQuantidade.IsEnabled = false;
                    btnAdd.IsEnabled = false;
                    cmbForma.IsEnabled = false;
                    DataGridCompraProduto.IsEnabled = false;
                    cbFornecedor.IsEnabled = false;
                    txtValorCompra.IsEnabled = false;
                    cbStatus.IsEnabled = false;
                    dtpVencimento.IsEnabled = false;

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
                var vendaProduto = DataGridCompraProduto.SelectedItem as CompraProduto;
                vendaProduto.Compra_fk = identificadorCompra;

                var result = MessageBox.Show($"Deseja realmente remover o produto `{vendaProduto.Produto}`?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var dao2 = new CompraProdutoDAO();
                    dao2.Delete(vendaProduto);
                    txtValorCompra.Text = Convert.ToString(vendaProduto.ValorTotal);
                    Carregar(vendaProduto.Compra_fk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            CompraProduto vendaProduto = new CompraProduto();

            if (txtCodigoProduto.Text != "")
            {
                vendaProduto.Codigo = Convert.ToInt32(txtCodigoProduto.Text);
                if (txtQuantidade.Text != "")
                {
                    vendaProduto.Quantidade = Convert.ToDouble(txtQuantidade.Text);
                    vendaProduto.Compra_fk = identificadorCompra;

                    try
                    {
                        var dao = new CompraProdutoDAO();
                        dao.Insert(vendaProduto);
                        txtValorCompra.Text = Convert.ToString(vendaProduto.ValorTotal);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    txtCodigoProduto.Text = "";
                    txtQuantidade.Text = "";

                    Carregar(vendaProduto.Compra_fk);
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
                var result = MessageBox.Show($"Algumas edições nessa tela podem ser perdidas caso continue. Deseja realmente voltar à listar compras?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        CompraListar compras = new CompraListar();
                        compras.Show();
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
                CompraListar compras = new CompraListar();
                compras.Show();
                this.Close();
            }
        }

        private void FechandoJanela(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Editou == true)
            {
                if (!jaFoi)
                {
                    var dao = new CompraDAO();

                    var compraSelected = dao.ultimaCompra();

                    var result = MessageBox.Show($"Qualquer informação registrada nessa tela será perdida. Deseja realmente fechar essa janela?", "Confirmação de Exclusão",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    try
                    {
                        if (result == MessageBoxResult.Yes)
                        {
                            var dao2 = new CompraDAO();
                            dao2.Delete(compraSelected);
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
