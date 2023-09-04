using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SistemaDeCompra
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<ItemCompra> carrinho = new ObservableCollection<ItemCompra>();

        public MainWindow()
        {
            InitializeComponent();
            lvCarrinho.ItemsSource = carrinho;
        }

        private void AdicionarAoCarrinho_Click(object sender, RoutedEventArgs e)
        {
            string produto = txtProduto.Text;
            if (string.IsNullOrWhiteSpace(produto))
            {
                MessageBox.Show("Por favor, insira um nome de produto válido.");
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Por favor, insira uma quantidade válida.");
                return;
            }

            if (!decimal.TryParse(txtPrecoUnitario.Text, out decimal precoUnitario) || precoUnitario <= 0)
            {
                MessageBox.Show("Por favor, insira um preço unitário válido.");
                return;
            }

            decimal subtotal = quantidade * precoUnitario;
            carrinho.Add(new ItemCompra { Produto = produto, Quantidade = quantidade, PrecoUnitario = precoUnitario, Subtotal = subtotal });
            CalcularTotal();
            LimparCampos();
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in carrinho)
            {
                total += item.Subtotal;
            }
            lblTotal.Content = $"Total: R$ {total:F2}";
        }

        private void LimparCampos()
        {
            txtProduto.Clear();
            txtQuantidade.Clear();
            txtPrecoUnitario.Clear();
        }
    }

    public class ItemCompra
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
