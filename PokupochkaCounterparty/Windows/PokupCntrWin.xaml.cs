using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using PokupochkaCounterparty.Modules.Counterparty;
using System;
using System.Linq;
using System.Windows;

namespace PokupochkaCompany.Windows
{
    public partial class PokupCntrWin : Window
    {
        public PokupCntrWin(User user)
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
                case (int)RoleKey.Supplier:
                    SetUserStyles("/Styles/AdminStyle.xaml");
                    break;
                case (int)RoleKey.Client:
                    SetUserStyles("/Styles/StorekepStyle.xaml");
                    break;
            }

            Style = UserStyles.WindowSyle;
            FrmMain.NavigationService.Navigate(new CntrMainPage());
        }

        /// <summary>
        /// Устанавливает стили из указанного файла стилей
        /// </summary>
        /// <param name="resourceUri"></param>
        private void SetUserStyles(string resourceUri)
        {
            ResourceDictionary resource = new();
            resource.Source = new Uri(resourceUri, UriKind.Relative);

            UserStyles.WindowSyle = resource["WindowBackground"] as Style;
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
