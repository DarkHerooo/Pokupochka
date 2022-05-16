using DbLib;
using Microsoft.EntityFrameworkCore;
using PokupochkaCompany.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PokupochkaCompany.Modules.Administrator.Tables
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        public WorkersPage()
        {
            InitializeComponent();

            GenerateFilterList();
            DgUsers.ItemsSource = ShowWorkers();
        }

        /// <summary>
        /// Возвращает массив работников с учётом поиска и фильтрации
        /// </summary>
        /// <returns></returns>
        private Worker[] ShowWorkers()
        {
            List<Worker> workers = DbConnect.Db.Workers.Include(w => w.User).ToList();

            if (!string.IsNullOrEmpty(TbFinder.Text) && !string.IsNullOrWhiteSpace(TbFinder.Text))
                workers = FindWorkers(workers);

            if (CbFilter.SelectedIndex > 0)
                workers = FilterWorkers(workers);

            return workers.ToArray();
        }

        /// <summary>
        /// Возвращает список работников с учётом поиска
        /// </summary>
        /// <returns></returns>
        private List<Worker> FindWorkers(List<Worker> workers)
        {
            if (TbFinder.IsFocused)
            {
                string text = TbFinder.Text;
                workers = workers.Where(w =>
                    w.FIO.ToLower().Contains(text.ToLower()) ||
                    w.User!.Login.ToLower().Contains(text.ToLower())).ToList();
            }

            return workers;
        }

        /// <summary>
        /// Возвращает список работников с учётом фильтрации
        /// </summary>
        /// <returns></returns>
        private List<Worker> FilterWorkers(List<Worker> workers)
        {
            Role? role = CbFilter.SelectedItem as Role;

            workers = workers.Where(w => w.User!.Role == role).ToList();

            return workers;
        }

        /// <summary>
        /// Генерирует список фильтрации
        /// </summary>
        private void GenerateFilterList()
        {
            List<Role> roles = DbConnect.Db.Roles.Where
                (r => r.Id == (int)RoleKey.Administratior ||
                r.Id == (int)RoleKey.Storekeeper ||
                r.Id == (int)RoleKey.Agent).ToList();

            foreach (var role in roles)
                CbFilter.Items.Add(role);

            CbFilter.SelectedIndex = 0;
        }

        private void TbFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
                DgUsers.ItemsSource = ShowWorkers();
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DgUsers.ItemsSource = ShowWorkers();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WorkersWorkWin win = new WorkersWorkWin(new Worker());
            win.ShowDialog();

            if (win.DialogResult == true)
                DgUsers.ItemsSource = ShowWorkers();

            Page_Loaded(null!, null!);
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            Worker? worker = DgUsers.SelectedItem as Worker;
            if (worker != null)
            {
                WorkersWorkWin win = new WorkersWorkWin(worker);
                win.ShowDialog();

                //if (win.DialogResult == true)

                DgUsers.ItemsSource = ShowWorkers();
                Page_Loaded(null!, null!);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Worker? worker = DgUsers.SelectedItem as Worker;
            if (worker != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этого работника?", "Удаление работника",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    worker.User!.Delete();
                    DgUsers.ItemsSource = ShowWorkers();
                    Page_Loaded(null!, null!);
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbFinder.Text = "";
            CbFilter.SelectedIndex = 0;
            DgUsers.UnselectAll();
            DgUsers.ItemsSource = ShowWorkers();
        }

        private void DgUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnChange_Click(BtnChange, new RoutedEventArgs());
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnChange.IsEnabled = DgUsers.SelectedItem != null ? true : false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DbConnect.Db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
