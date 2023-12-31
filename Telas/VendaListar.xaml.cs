﻿using NewAppCacauShow.Classes;
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
    /// Lógica interna para VendaListar.xaml
    /// </summary>
    public partial class VendaListar : Window
    {
        private int vendaSelecionadaId;

        public VendaListar()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new VendaDAO();

            try
            {
                DataGridVenda.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridVendas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridVenda.SelectedItem != null)
            {
                Venda vendaSelecionada = (Venda)DataGridVenda.SelectedItem;
                vendaSelecionadaId = vendaSelecionada.IdVenda;
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
            VendaCadastrar vendaCadastrar = new VendaCadastrar();
            var dao = new VendaDAO();
            dao.Insert();
            vendaCadastrar.Show();
            this.Close();
        }

        private void Detalhes_Click(object sender, RoutedEventArgs e)
        {
            var vendaSelected = DataGridVenda.SelectedItem as Venda;
            VendaConsultar vendaConsultar = new VendaConsultar(vendaSelected.IdVenda);
            vendaConsultar.Show();
            this.Close();
        }


        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var vendaSelected = DataGridVenda.SelectedItem as Venda;

            var result = MessageBox.Show($"Deseja realmente remover a venda `{vendaSelected.IdVenda}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new VendaDAO();
                    dao.Delete(vendaSelected);
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
