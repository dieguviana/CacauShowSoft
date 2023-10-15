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
    /// Lógica interna para FornecedorListar.xaml
    /// </summary>
    public partial class FornecedorListar : Window
    {
        private int fornecedorSelecionadoId;

        public FornecedorListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

            private void Carregar()
            {
                var dao = new FornecedorDAO();

                try
                {
                    DataGridFornecedor.ItemsSource = dao.List();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void DataGridFornecedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (DataGridFornecedor.SelectedItem != null)
                {
                    Fornecedor fornecedorSelecionado = (Fornecedor)DataGridFornecedor.SelectedItem;
                    fornecedorSelecionadoId = fornecedorSelecionado.IdFornecedor;
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
                FornecedorCadastrar fornecedorCadastrar = new FornecedorCadastrar();
                fornecedorCadastrar.Show();
                this.Close();
            }

            private void Detalhes_Click(object sender, RoutedEventArgs e)
            {
             var fornecedorSelected = DataGridFornecedor.SelectedItem as Fornecedor;
             FornecedorConsultar fornecedorConsultar = new FornecedorConsultar(fornecedorSelected.IdFornecedor);
             fornecedorConsultar.Show();
             this.Close();
             }

            private void Excluir_Click(object sender, RoutedEventArgs e)
            {
                var fornecedorSelected = DataGridFornecedor.SelectedItem as Fornecedor;

                var result = MessageBox.Show($"Deseja realmente remover o fornecedor `{fornecedorSelected.Nome}`?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        var dao = new FornecedorDAO();
                        dao.Delete(fornecedorSelected);
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
