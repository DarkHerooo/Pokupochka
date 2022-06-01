using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.Usr;
using StylesLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AdministratorWPF.View.Tables
{
    public partial class WorkersWorkWin : Window
    {
        private Worker _worker;
        public WorkersWorkWin(Worker worker)
        {
            InitializeComponent();

            _worker = worker;
            Style = UserStyles.WindowSyle;
            SetWinSettings();
        }

        /// <summary>
        /// Устанавливает настройки окна
        /// </summary>
        private void SetWinSettings()
        {
            User user = CurrentUser.User!;
            if (user.RoleId == (int)RoleKey.Administratior && user != _worker.User!)
            {
                List<Role> roles = DbConnect.Db.Roles.Where
                    (r => r.Id == (int)RoleKey.Administratior ||
                    r.Id == (int)RoleKey.Storekeeper ||
                    r.Id == (int)RoleKey.Agent).ToList();
                CbRole.ItemsSource = roles;

                if (_worker.Id == 0) _worker.User!.Role = roles.First();
            }
            else GrRoles.Visibility = Visibility.Hidden;

            if (_worker.Id == 0)
            {
                Title = "Добавить пользователя";
                BtnAddOrChange.Content = "Добавить пользователя";
            }
            else
            {
                Title = "Изменить пользователя";
                BtnAddOrChange.Content = "Изменить пользователя";
            }

            DataContext = _worker;
        }

        /// <summary>
        /// Проверяет корректность заполнения полей ввода
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(TbSecondName.Text) || string.IsNullOrWhiteSpace(TbSecondName.Text))
                errorMessage += "Не введена фамилия\n";

            if (string.IsNullOrEmpty(TbFirstName.Text) || string.IsNullOrWhiteSpace(TbFirstName.Text))
                errorMessage += "Не введено имя\n";

            string clearPhone = AdditionalFields.GetClearPhone(TbPhone.Text);
            if (string.IsNullOrEmpty(clearPhone) || string.IsNullOrWhiteSpace(clearPhone))
                errorMessage += "Не введен телефон\n";
            else if (clearPhone.Length < 11)
                errorMessage += "Введите телефон полностью\n";

            if (string.IsNullOrEmpty(TbEmail.Text) || string.IsNullOrWhiteSpace(TbEmail.Text))
                errorMessage += "Не введена почта\n";

            if (string.IsNullOrEmpty(TbLogin.Text) || string.IsNullOrWhiteSpace(TbLogin.Text))
                errorMessage += "Не введен логин\n";
            else
            {
                User? user = DbConnect.Db.Users.FirstOrDefault(u => u.Login == TbLogin.Text);
                if (user != null && user != _worker.User)
                    errorMessage += "Пользователь с таким логином уже существует\n";
            } 

            if (string.IsNullOrEmpty(TbPassword.Text) || string.IsNullOrWhiteSpace(TbPassword.Text))
                errorMessage += "Не введен пароль\n";

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else return true;

        }

        /// <summary>
        /// Событие нажатия на кнопку "Сохранить/изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddOrChange_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                _worker.User!.AddOrChange();
                _worker.AddOrChange();

                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
