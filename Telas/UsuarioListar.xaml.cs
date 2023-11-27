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
    /// Lógica interna para UsuarioListar.xaml
    /// </summary>
    public partial class UsuarioListar : Window
    {
        private int usuarioSelecionadoId;

        public UsuarioListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new UsuarioDAO();

            try
            {
                DataGridUsuario.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridUsuario.SelectedItem != null)
            {
                Usuario usuarioSelecionado = (Usuario)DataGridUsuario.SelectedItem;
                usuarioSelecionadoId = usuarioSelecionado.IdUsuario;
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
            UsuarioCadastrar usuarioCadastrar = new UsuarioCadastrar();
            usuarioCadastrar.Show();
            this.Close();
        }

        private void Detalhes_Click(object sender, RoutedEventArgs e)
        {
            var usuarioSelected = DataGridUsuario.SelectedItem as Usuario;
            UsuarioConsultar usuarioConsultar = new UsuarioConsultar(usuarioSelected.IdUsuario);
            usuarioConsultar.Show();
            this.Close();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var usuarioSelected = DataGridUsuario.SelectedItem as Usuario;

            var result = MessageBox.Show($"Deseja realmente remover o usuario `{usuarioSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new UsuarioDAO();
                    dao.Delete(usuarioSelected);
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
    
