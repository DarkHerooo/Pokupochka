using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdministratorWPF.View.Tables
{
    public partial class CntrpartiesPage : Page
    {
        private Role _role;
        public CntrpartiesPage(Role role)
        {
            InitializeComponent();

            _role = role;
            DgCounterparties.ItemsSource = GetCounterparties();
            SetPageSettings();
        }

        /// <summary>
        /// Устанавливает настройки страницы
        /// </summary>
        private void SetPageSettings()
        {
            if (_role.Id == (int)RoleKey.Supplier)
                DgtcCounterParty.Header = "Информация о поставщике";
            else if (_role.Id == (int)RoleKey.Client)
                DgtcCounterParty.Header = "Информация о клиенте";
        }

        /// <summary>
        /// Возвращает массив контрагентов с учётом поиска
        /// </summary>
        /// <returns></returns>
        private Counterparty[] GetCounterparties()
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

        /// <summary>
        /// Событие нажатия клавиши клавиатуры при фокусе на поле поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DgCounterparties.ItemsSource = GetCounterparties();
        }

        /// <summary>
        /// Событие изменения выбранного элемента в выпадающем списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DgCounterparties.ItemsSource = GetCounterparties();
        }

        /// <summary>
        /// Событие нажатия на кнопку "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Counterparty counterparty = new();
            counterparty.User = new();
            counterparty.User.Role = _role;

            CntrpartiesWorkWin win = new CntrpartiesWorkWin(counterparty);
            win.ShowDialog();

            DgCounterparties.ItemsSource = GetCounterparties();
            Page_Loaded(null!, null!);
        }

        /// <summary>
        /// Событие нажатия на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            Counterparty? counterparty = DgCounterparties.SelectedItem as Counterparty;
            if (counterparty != null)
            {
                CntrpartiesWorkWin win = new CntrpartiesWorkWin(counterparty);
                win.ShowDialog();

                DgCounterparties.ItemsSource = GetCounterparties();
                Page_Loaded(null!, null!);
            }
        }

        /// <summary>
        /// Событие нажатия на кнопку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Counterparty? client = DgCounterparties.SelectedItem as Counterparty;
            if (client != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этого клиента?", "Удаление работника",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    client.User!.Delete();
                    DgCounterparties.ItemsSource = GetCounterparties();
                }
            }
        }

        /// <summary>
        /// Событие нажатия на кнопку "Сброс"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbFinder.Text = "";
            DgCounterparties.UnselectAll();
            DgCounterparties.ItemsSource = GetCounterparties();
        }

        /// <summary>
        /// Событие двойного нажатия на элемент списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnChange_Click(BtnChange, new RoutedEventArgs());
        }

        /// <summary>
        /// Событие изменения выделенного элемента в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnChange.IsEnabled = DgCounterparties.SelectedItem != null ? true : false;
        }

        /// <summary>
        /// Событие показа страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DbConnect.Db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
