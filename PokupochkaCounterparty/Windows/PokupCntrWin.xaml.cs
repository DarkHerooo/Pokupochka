using AdministratorWPF.View.Tables;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using GeneralLib.Usr;
using StylesLib;
using System;
using System.Linq;
using System.Windows;
using WPFCounterpartyLib.View;

namespace PokupochkaCounterparty.Windows
{
    public partial class PokupCntrWin : Window
    {
        public PokupCntrWin()
        {
            InitializeComponent();

            SetSettings();
        }

        /// <summary>
        /// Устанавливает настройки в зависимости от роли пользователя
        /// </summary>
        /// <param name="user"></param>
        private void SetSettings()
        {
            User user = CurrentUser.User!;
            DataContext = user.Counterparty;

            if (user.Role != null)
                Title = "Покупочка (" + user.Role?.Title + ")";

            switch (user.Role!.Id)
            {
                case (int)RoleKey.Supplier:
                    UserStyles.SetUserStyles("SupplierStyles.xaml");
                    break;
                case (int)RoleKey.Client:
                    UserStyles.SetUserStyles("ClientStyles.xaml");
                    break;
            }
            StyleWorker.SetAllStyles();

            Style = UserStyles.WindowSyle;
            FrmMain.NavigationService.Navigate(new CntrMainPage());
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            User user = CurrentUser.User!;
            CntrpartiesWorkWin win = new(user.Counterparty!);
            win.ShowDialog();

            if (win.DialogResult == true)
            {
                DataContext = null;
                DataContext = user.Counterparty!;
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            AutorizWin win = new();
            win.Show();
            Close();
        }
    }
}
