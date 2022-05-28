using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using StylesLib;
using System;
using System.Linq;
using System.Windows;
using WPFCounterpartyLib.View;

namespace PokupochkaCounterparty.Windows
{
    public partial class PokupCntrWin : Window
    {
        public PokupCntrWin(User user)
        {
            InitializeComponent();

            CurrentUser.User = user;
            SetSettings(user);
        }

        /// <summary>
        /// Устанавливает настройки в зависимости от роли пользователя
        /// </summary>
        /// <param name="user"></param>
        private void SetSettings(User user)
        {
            DataContext = user.Counterparty;

            if (user.Role != null)
                Title = "Покупочка (" + user.Role?.Title + ")";

            switch (user.Role!.Id)
            {
                case (int)RoleKey.Supplier:
                    UserStyles.SetUserStyles("SupplierStyle.xaml");
                    break;
                case (int)RoleKey.Client:
                    UserStyles.SetUserStyles("ClientStyle.xaml");
                    break;
            }
            StyleWorker.SetAllStyles();

            Style = UserStyles.WindowSyle;
            FrmMain.NavigationService.Navigate(new CntrMainPage());
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            AutorizWin win = new();
            win.Show();
            Close();
        }

    }
}
