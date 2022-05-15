using DbLib;
using PokupochkaCompany.Classes;
using PokupochkaCompany.Modules.Administrator;
using PokupochkaCompany.Modules.Agent;
using PokupochkaCompany.Storekeeper;
using System;
using System.Linq;
using System.Windows;

namespace PokupochkaCompany.Windows
{
    public partial class PokupCompWin : Window
    {
        public PokupCompWin(User user)
        {
            InitializeComponent();

            SetUserFunctions(user);
        }

        /// <summary>
        /// Устанавливает функции в зависимости от пользователя
        /// </summary>
        /// <param name="user"></param>
        private void SetUserFunctions(User user)
        {
            TblFIO.Text = user.Worker!.FIO;

            if (user.Role != null)
            {
                TblRole.Text = "(" + user.Role?.Title + ")";
                Title = "Покупочка (" + user.Role?.Title + ")";
            }

            switch (user.Role!.Id)
            {
                case (int)RoleKey.Administratior:
                    SetUserStyles("/Styles/AdminStyle.xaml");
                    FrmMain.NavigationService.Navigate(new AdminMainPage());
                    break;
                case (int)RoleKey.Storekeeper:
                    SetUserStyles("/Styles/StorekepStyle.xaml");
                    FrmMain.NavigationService.Navigate(new StorekepMainPage());
                    break;
                case (int)RoleKey.Agent:
                    SetUserStyles("/Styles/AgentStyle.xaml");
                    FrmMain.NavigationService.Navigate(new AgentMainPage());
                    break;
            }

            Style = UserStyles.WindowBackground;
        }

        /// <summary>
        /// Устанавливает стили из указанного файла стилей
        /// </summary>
        /// <param name="resourceUri"></param>
        private void SetUserStyles(string resourceUri)
        {
            ResourceDictionary resource = new();
            resource.Source = new Uri(resourceUri, UriKind.Relative);

            UserStyles.WindowBackground = resource["WindowBackground"] as Style;
            UserStyles.DefaultButtonStyle = resource["DefaultButtonStyle"] as Style;
            UserStyles.SelectButtonStyle = resource["SelectButtonStyle"] as Style;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            AutorizWin win = new();
            win.Show();
            Close();
        }
    }
}
