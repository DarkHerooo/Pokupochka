﻿using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.CustomMessages;
using GeneralLib.Usr;
using Microsoft.EntityFrameworkCore;
using StylesLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PokupochkaCounterparty.Windows
{
    public partial class AutorizWin : Window
    {
        public AutorizWin()
        {
            InitializeComponent();
            SetWinSettings();
            DbConnect.Db.Users.LoadAsync();
        }

        private void SetWinSettings()
        {
            UserStyles.SetUserStyles("CounterpartyStyles.xaml");
            Style = UserStyles.WindowSyle;

            UserDataReader.Set();
            if (UserDataReader.UserData.Remember)
            {
                TbLogin.Text = UserDataReader.UserData.Login;
                PbPassword.Password = UserDataReader.UserData.Password;
                ChbMemory.IsChecked = true;
            }
        }

        /// <summary>
        /// Открывает главное окно приложения
        /// </summary>
        /// <param name="user"></param>
        private void LoginToTheApp(User user)
        {
            CurrentUser.User = user;

            UserDataReader.UserData.Login = user.Login;
            UserDataReader.UserData.Password = user.Password;
            UserDataReader.UserData.Remember = (bool)ChbMemory.IsChecked!;
            UserDataReader.Save();

            PokupCntrWin window = new();
            window.Show();
            Close();
        }

        private async void BtnEnter_ClickAsync(object sender, RoutedEventArgs e)
        {
            BtnEnter.IsEnabled = false;
            TbLogin.IsEnabled = false;
            PbPassword.IsEnabled = false;

            CustomMessage message = new();
            await message.ShowMessage(SpMessage, MessageType.Loading, "Подождите...");

            List<User> users = DbConnect.Db.Users.Include(u => u.Role).Include(u => u.Counterparty).Include(u => u.Counterparty!.Company).ToList();

            User? findUser = users.FirstOrDefault(user => 
                user.Login == TbLogin.Text &&
                user.Password == PbPassword.Password &&
                (user.RoleId == (int)RoleKey.Supplier || 
                    user.RoleId == (int)RoleKey.Client));

            if (findUser != null) LoginToTheApp(findUser);
            else await message.ShowMessage(SpMessage, MessageType.Error, "Неправильный логин или пароль!");

            BtnEnter.IsEnabled = true;
            TbLogin.IsEnabled = true;
            PbPassword.IsEnabled = true;
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

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            HowCounterpartyWin win = new();
            win.ShowDialog();
        }
    }
}
