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
    public partial class WorkersPage : Page
    {
        public WorkersPage()
        {
            InitializeComponent();

            CreateFilterList();
            DgUsers.ItemsSource = GetWorkers();
        }

        /// <summary>
        /// Возвращает массив работников с учётом поиска и фильтрации
        /// </summary>
        /// <returns></returns>
        private Worker[] GetWorkers()
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
        private void CreateFilterList()
        {
            List<Role> roles = DbConnect.Db.Roles.Where
                (r => r.Id == (int)RoleKey.Administratior ||
                r.Id == (int)RoleKey.Agent).ToList();

            foreach (var role in roles)
                CbFilter.Items.Add(role);

            CbFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Событие нажатия клавиши клавиатуры при фокусе на поле поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
                DgUsers.ItemsSource = GetWorkers();
        }

        /// <summary>
        /// Событие изменения выбранного элемента в выпадающем списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DgUsers.ItemsSource = GetWorkers();
        }

        /// <summary>
        /// Событие нажатия на кнопку "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WorkersWorkWin win = new WorkersWorkWin(new Worker());
            win.ShowDialog();

            if (win.DialogResult == true)
                DgUsers.ItemsSource = GetWorkers();

            Page_Loaded(null!, null!);
        }

        /// <summary>
        /// Событие нажатия на кнопку "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            Worker? worker = DgUsers.SelectedItem as Worker;
            if (worker != null)
            {
                WorkersWorkWin win = new WorkersWorkWin(worker);
                win.ShowDialog();

                DgUsers.ItemsSource = GetWorkers();
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
            Worker? worker = DgUsers.SelectedItem as Worker;
            if (worker != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этого работника?", "Удаление работника",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    worker.User!.Delete();
                    DgUsers.ItemsSource = GetWorkers();
                    Page_Loaded(null!, null!);
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
            CbFilter.SelectedIndex = 0;
            DgUsers.UnselectAll();
            DgUsers.ItemsSource = GetWorkers();
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
            BtnChange.IsEnabled = DgUsers.SelectedItem != null ? true : false;
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
