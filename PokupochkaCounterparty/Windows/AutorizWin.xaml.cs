﻿using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.CustomMessages;
using Microsoft.EntityFrameworkCore;
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
            DbConnect.Db.Users.LoadAsync();
        }

        /// <summary>
        /// Открывает главное окно приложения
        /// </summary>
        /// <param name="user"></param>
        private void LoginToTheApp(User user)
        {
            PokupCntrWin window = new(user);
            window.Show();
            Close();
        }

        private async void BtnEnter_ClickAsync(object sender, RoutedEventArgs e)
        {
            BtnEnter.IsEnabled = false;

            CustomMessage message = new();
            await message.ShowMessage(SpMessage, MessageType.Loading, "Подождите...");

            List<User> users = DbConnect.Db.Users.Include(u => u.Role).Include(u => u.Counterparty).ToList();

            User? findUser = users.FirstOrDefault(user => 
                user.Login == TbLogin.Text &&
                user.Password == PbPassword.Password &&
                (user.RoleId == (int)RoleKey.Supplier || 
                    user.RoleId == (int)RoleKey.Client));

            if (findUser != null) LoginToTheApp(findUser);
            else await message.ShowMessage(SpMessage, MessageType.Error, "Неправильный логин или пароль!");

            BtnEnter.IsEnabled = true;
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
