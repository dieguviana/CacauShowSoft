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
    /// Lógica interna para CompraListar.xaml
    /// </summary>
    public partial class CompraListar : Window
    {
        private int compraSelecionadaId;

        public CompraListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new CompraDAO();

            try
            {
                DataGridCompra.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridVendas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridCompra.SelectedItem != null)
            {
                Compra compraSelecionada = (Compra)DataGridCompra.SelectedItem;
                compraSelecionadaId = compraSelecionada.IdCompra;
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            CompraCadastrar compraCadastrar = new CompraCadastrar();
            var dao = new CompraDAO();
            dao.Insert();
            compraCadastrar.Show();
            this.Close();
        }

        private void Detalhes_Click(object sender, RoutedEventArgs e)
        {
            var compraSelected = DataGridCompra.SelectedItem as Compra;
            VendaConsultar compraConsultar = new VendaConsultar(compraSelected.IdCompra);
            compraConsultar.Show();
            this.Close();
        }


        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var compraSelected = DataGridCompra.SelectedItem as Compra;

            var result = MessageBox.Show($"Deseja realmente remover a compra `{compraSelected.IdCompra}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new CompraDAO();
                    dao.Delete(compraSelected);
                    Carregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}