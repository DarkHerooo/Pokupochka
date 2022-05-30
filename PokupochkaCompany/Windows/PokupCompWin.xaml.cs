using AdministratorWPF.View;
using WPFAgentLib.View;
using WPFStorekeeperLib.View;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.Usr;
using System.Windows;
using StylesLib;
using AdministratorWPF.View.Tables;

namespace PokupochkaCompany.Windows
{
    public partial class PokupCompWin : Window
    {
        public PokupCompWin()
        {
            InitializeComponent();

            SetWinSettings();
        }

        /// <summary>
        /// Устанавливает настройки в зависимости от роли пользователя
        /// </summary>
        /// <param name="user"></param>
        private void SetWinSettings()
        {
            User user = CurrentUser.User;
            DataContext = user.Worker!;

            if (user.Role != null)
                Title = "Покупочка (" + user.Role?.Title + ")";

            switch (user.RoleId)
            {
                case (int)RoleKey.Administratior:
                    UserStyles.SetUserStyles("AdminStyles.xaml");
                    FrmMain.NavigationService.Navigate(new AdminMainPage());
                    break;
                case (int)RoleKey.Storekeeper:
                    UserStyles.SetUserStyles("StorekepStyles.xaml");
                    FrmMain.NavigationService.Navigate(new StorekepMainPage());
                    break;
                case (int)RoleKey.Agent:
                    UserStyles.SetUserStyles("AgentStyles.xaml");
                    FrmMain.NavigationService.Navigate(new AgentMainPage());
                    break;
            }
            StyleWorker.SetAllStyles();

            Style = UserStyles.WindowSyle;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            AutorizWin win = new();
            win.Show();
            Close();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            User user = CurrentUser.User;
            WorkersWorkWin win = new(user.Worker!);
            win.ShowDialog();

            if (win.DialogResult == true)
            {
                DataContext = null;
                DataContext = user.Worker!;
            }
        }
    }
}
