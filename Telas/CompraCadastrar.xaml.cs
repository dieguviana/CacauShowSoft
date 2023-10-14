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
    /// Lógica interna para CompraCadastrar.xaml
    /// </summary>
    public partial class CompraCadastrar : Window
    {
        public CompraCadastrar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
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

       

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var compraProduto = DataGridCompraProduto.SelectedItem as CompraProduto;
                var dao = new CompraDAO(); var compraSelected = dao.ultimaCompra();
                compraProduto.Compra_fk = compraSelected.IdCompra;

                var result = MessageBox.Show($"Deseja realmente remover o produto `{compraProduto.Produto}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var dao2 = new CompraProdutoDAO();
                    dao2.Delete(compraProduto);
                    Carregar(compraSelected.IdCompra);
                    txtValorCompra.Text = Convert.ToString(compraProduto.ValorTotal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool jaFoi = false;

       
     
        private void FinalizarCompra_Click(object sender, RoutedEventArgs e)
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

                // Fornecedor - Obrigatório
                if (!string.IsNullOrWhiteSpace(txtFornecedor.Text))
                {
                    pagamento.Fornecedor_cnpj = txtFornecedor.Text;
                }
                else
                {
                    MessageBox.Show("O campo fornecedor é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var dao = new CompraDAO();
                    var compraSelected = dao.ultimaCompra();
                    pagamento.Compra_fk = compraSelected.IdCompra;

                    var dao2 = new PagamentoDAO();
                    dao2.Insert(pagamento);

                    MessageBox.Show("Compra inserida com sucesso!" + ".", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    var dao3 = new CompraDAO();
                    dao3.Insert();

                    // Limpe os campos
                    txtValorCompra.Text = "";
                    cbStatus.Text = "";
                    dtpVencimento.Text = "";
                    cmbForma.SelectedItem = null;
                    txtFornecedor.Text = "";
                    DataGridCompraProduto.ItemsSource = null;
                }
                else
                {
                    MessageBox.Show("Selecione a forma de pagamento.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
                CompraProduto compraProduto = new CompraProduto();

                if (txtCodigoProduto.Text != "")
                {
                    compraProduto.Codigo = Convert.ToInt32(txtCodigoProduto.Text);
                    if (txtQuantidade.Text != "")
                    {
                        compraProduto.Quantidade = Convert.ToDouble(txtQuantidade.Text);
                        var dao = new CompraDAO(); var compraSelected = dao.ultimaCompra();
                        compraProduto.Compra_fk = compraSelected.IdCompra;

                        try
                        {
                            var dao2 = new CompraProdutoDAO();
                            dao2.Insert(compraProduto);
                            txtValorCompra.Text = Convert.ToString(compraProduto.ValorTotal);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        txtCodigoProduto.Text = "";
                        txtQuantidade.Text = "";

                        Carregar(compraSelected.IdCompra);
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

        private void CancelarCompra_Click(object sender, RoutedEventArgs e)
        {
            
                if (!jaFoi)
                {
                    jaFoi = true;
                    var dao = new CompraDAO();

                    var compraSelected = dao.ultimaCompra();

                    var result = MessageBox.Show($"Qualquer informação registrada nessa tela será perdida. Deseja realmente voltar à listar vendas?", "Confirmação de Exclusão",
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
                            jaFoi = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    CompraListar compras = new CompraListar();
                    compras.Show();
                    this.Close();
                }
        }

        private void FechandoJanela(object sender, System.ComponentModel.CancelEventArgs e)
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