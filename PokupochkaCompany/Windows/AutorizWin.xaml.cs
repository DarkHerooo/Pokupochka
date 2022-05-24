using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.CustomMessages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PokupochkaCompany.Windows
{
    public partial class AutorizWin : Window
    {
        public AutorizWin()
        {
            InitializeComponent();

            DbConnect.Db.Users.LoadAsync();
        }

        /// <summary>
        /// Открывает главное окно приложения
        /// </summary>
        /// <param name="user"></param>
        private void LoginToTheApp(User user)
        {
            PokupCompWin window = new(user);
            window.Show();
            Close();
        }

        private async void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            BtnEnter.Dispatcher.Invoke(() => BtnEnter.IsEnabled = false);

            CustomMessage message = new();
            await message.ShowMessage(SpMessage, MessageType.Loading, "Подождите...");

            List<User> users = DbConnect.Db.Users.Include(u => u.Role).Include(u => u.Worker).ToList();

            User? findUser = users.FirstOrDefault(user =>
                user.Login == TbLogin.Text &&
                user.Password == PbPassword.Password &&
                (user.RoleId == (int)RoleKey.Administratior ||
                    user.RoleId == (int)RoleKey.Storekeeper ||
                    user.RoleId == (int)RoleKey.Agent));

            if (findUser != null) LoginToTheApp(findUser);
            else await message.ShowMessage(SpMessage, MessageType.Error, "Неправильный логин или пароль!");

            BtnEnter.Dispatcher.Invoke(() => BtnEnter.IsEnabled = true);
        }

        private void TbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SpMessage.Children.Count > 0)
                SpMessage.Children.Clear();
        }

        private void PbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (SpMessage.Children.Count > 0)
                SpMessage.Children.Clear();
        }
    }
}
