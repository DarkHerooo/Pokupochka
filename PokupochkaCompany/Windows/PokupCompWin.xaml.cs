using AdministratorWPF.View;
using WPFAgentLib.View;
using WPFStorekeeperLib.View;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using System.Windows;
using StylesLib;

namespace PokupochkaCompany.Windows
{
    public partial class PokupCompWin : Window
    {
        public PokupCompWin(User user)
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
            TblFIO.Text = user.Worker!.FIO;

            if (user.Role != null)
            {
                TblRole.Text = "(" + user.Role?.Title + ")";
                Title = "Покупочка (" + user.Role?.Title + ")";
            }
            switch (user.Role!.Id)
            {
                case (int)RoleKey.Administratior:
                    UserStyles.SetUserStyles("AdminStyle.xaml");
                    FrmMain.NavigationService.Navigate(new AdminMainPage());
                    break;
                case (int)RoleKey.Storekeeper:
                    UserStyles.SetUserStyles("StorekepStyle.xaml");
                    FrmMain.NavigationService.Navigate(new StorekepMainPage());
                    break;
                case (int)RoleKey.Agent:
                    UserStyles.SetUserStyles("AgentStyle.xaml");
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
    }
}
