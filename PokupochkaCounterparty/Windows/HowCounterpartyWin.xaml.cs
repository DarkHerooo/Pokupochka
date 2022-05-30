using AdministratorWPF.View.Tables;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
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

namespace PokupochkaCounterparty.Windows
{
    /// <summary>
    /// Логика взаимодействия для HowCounterpartyWin.xaml
    /// </summary>
    public partial class HowCounterpartyWin : Window
    {
        public HowCounterpartyWin()
        {
            InitializeComponent();
        }

        private void Gr_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            grid.Background = Brushes.LightGreen;
        }

        private void Gr_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grid = (Grid)sender;
            grid.Background = Brushes.Transparent;
        }

        private void GrSupplier_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Counterparty counterparty = new();
            counterparty.User!.RoleId = (int)RoleKey.Supplier;

            CntrpartiesWorkWin win = new(counterparty);
            win.ShowDialog();
            Close();
        }

        private void GrClient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Counterparty counterparty = new();
            counterparty.User!.RoleId = (int)RoleKey.Client;

            CntrpartiesWorkWin win = new(counterparty);
            win.ShowDialog();
            Close();
        }
    }
}
