﻿using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
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
                    r.Id == (int)RoleKey.Agent).ToList();
                CbRole.ItemsSource = roles;

                if (_worker.Id == 0) _worker.User!.Role = roles.First();
            }
            else GrRoles.Visibility = Visibility.Hidden;

            if (_worker.Id == 0)
            {
                Title = "Добавить работника";
                BtnAddOrChange.Content = "Добавить работника";
            }
            else
            {
                Title = "Изменить работника";
                BtnAddOrChange.Content = "Изменить работника";
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

            errorMessage += WPFChecks.CheckEmail(TbEmail.Text);

            errorMessage += WPFChecks.CheckLogin(TbLogin.Text);

            errorMessage += WPFChecks.CheckPassword(TbPassword.Text);

            if (_worker.Id == 0)
            {
                User? user = DbConnect.Db.Users.FirstOrDefault(u => u.Login == TbLogin.Text /*&& u.Id != CurrentUser.User.Id*/);
                if (user != null)
                    errorMessage += "Пользователь с таким логином уже существует\n";
            }

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

        private void TbEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string email = TbEmail.Text;
            if (email.Contains('@') && e.Text[0] == '@')
                e.Handled = true;
        }

        private void TbFIO_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text[0]))
                e.Handled = true;
        }
    }
}
