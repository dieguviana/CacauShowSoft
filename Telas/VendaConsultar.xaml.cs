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
                txtCodigoVenda.Text = vendaSelected.IdRec.ToString();
                txtFuncionario.Text = vendaSelected.Usuario.ToString();
                txtDataHora.Text = vendaSelected.DataHora.ToString();
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