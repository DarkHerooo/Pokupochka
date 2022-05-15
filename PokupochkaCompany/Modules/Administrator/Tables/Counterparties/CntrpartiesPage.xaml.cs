using DbLib;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokupochkaCompany.Modules.Administrator.Tables
{
    /// <summary>
    /// Логика взаимодействия для CntrpartiesPage.xaml
    /// </summary>
    public partial class CntrpartiesPage : Page
    {
        private Role _role;
        public CntrpartiesPage(Role role)
        {
            InitializeComponent();

            _role = role;
            DgCounterparties.ItemsSource = ShowCounterparties();
            SetData();
        }

        private void SetData()
        {
            if (_role.Id == (int)RoleKey.Supplier)
                DgtcCounterParty.Header = "Информация о поставщике";
            else if (_role.Id == (int)RoleKey.Client)
                DgtcCounterParty.Header = "Информация о клиенте";
        }

        private Counterparty[] ShowCounterparties()
        {
            List<Counterparty> counterparties = DbConnect.Db.Counterparties.Include(c => c.User).Where(c => c.User!.Role == _role).ToList();

            if (!string.IsNullOrEmpty(TbFinder.Text) && !string.IsNullOrWhiteSpace(TbFinder.Text))
                counterparties = FindCounterparties(counterparties);

            return counterparties.ToArray();
        }

        /// <summary>
        /// Возвращает список контрагентов с учётом поиска
        /// </summary>
        /// <returns></returns>
        private List<Counterparty> FindCounterparties(List<Counterparty> counterparties)
        {
            if (TbFinder.IsFocused)
            {
                string text = TbFinder.Text;
                counterparties = counterparties.Where(c =>
                    c.FIO.ToLower().Contains(text.ToLower()) ||
                    c.User!.Login.ToLower().Contains(text.ToLower())).ToList();
            }

            return counterparties;
        }

        private void TbFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DgCounterparties.ItemsSource = ShowCounterparties();
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DgCounterparties.ItemsSource = ShowCounterparties();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Counterparty counterparty = new();
            counterparty.User = new();
            counterparty.User.Role = _role;

            CntrpartiesWorkWin win = new CntrpartiesWorkWin(counterparty);
            win.ShowDialog();

            if (win.DialogResult == true)
                DgCounterparties.ItemsSource = ShowCounterparties();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            Counterparty? counterparty = DgCounterparties.SelectedItem as Counterparty;
            if (counterparty != null)
            {
                CntrpartiesWorkWin win = new CntrpartiesWorkWin(counterparty);
                win.ShowDialog();

                if (win.DialogResult == true)
                    DgCounterparties.ItemsSource = ShowCounterparties();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Counterparty? client = DgCounterparties.SelectedItem as Counterparty;
            if (client != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Удаление работника",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    client.User!.Delete();
                    DgCounterparties.ItemsSource = ShowCounterparties();
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbFinder.Text = "";
            DgCounterparties.SelectedItem = null;
            DgCounterparties.ItemsSource = ShowCounterparties();
        }

        private void DgUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnChange_Click(BtnChange, new RoutedEventArgs());
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnChange.IsEnabled = DgCounterparties.SelectedItem != null ? true : false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DbConnect.Db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
