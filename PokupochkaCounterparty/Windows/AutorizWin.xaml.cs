using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
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

        /// <summary>
        /// Показывает ошибку, если пользователь не найден
        /// </summary>
        private void ShowError()
        {
            if (SpError.Children.Count > 0)
                SpError.Children.Clear();

            Image img = new();
            img.Source = new BitmapImage(new Uri("/Images/Message/error.png", UriKind.Relative));
            img.Width = 20;
            img.Height = 20;
            SpError.Children.Add(img);

            TextBlock tbl = new TextBlock();
            tbl.VerticalAlignment = VerticalAlignment.Center;
            tbl.Margin = new Thickness(5, 0, 0, 0);
            tbl.Foreground = Brushes.Red;
            tbl.FontWeight = FontWeights.Bold;
            tbl.FontSize = 15;
            tbl.Text = "Неправильный логин или пароль";
            SpError.Children.Add(tbl);
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = DbConnect.Db.Users.Include(u => u.Role).Include(u => u.Worker).ToList();

            User? findUser = users.FirstOrDefault(user => 
                user.Login == TbLogin.Text &&
                user.Password == PbPassword.Password &&
                (user.RoleId == (int)RoleKey.Administratior || 
                    user.RoleId == (int)RoleKey.Storekeeper ||
                    user.RoleId == (int)RoleKey.Agent));

            if (findUser != null) LoginToTheApp(findUser);
            else ShowError();
        }

        private void TbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SpError.Children.Count > 0)
                SpError.Children.Clear();
        }

        private void PbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (SpError.Children.Count > 0)
                SpError.Children.Clear();
        }
    }
}
