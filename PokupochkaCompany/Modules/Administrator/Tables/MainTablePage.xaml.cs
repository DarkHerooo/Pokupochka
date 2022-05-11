using PokupochkaCompany.Classes;
using PokupochkaCompany.Modules.Administrator.Tables;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PokupochkaCompany.Modules.Administrator.Tables
{
    /// <summary>
    /// Логика взаимодействия для MainTablePage.xaml
    /// </summary>
    public partial class MainTablePage : Page
    {
        private FrameWithHistory fwhTables = null!;
        private ButtonSelector btnSel = null!;

        public MainTablePage()
        {
            InitializeComponent();

            TryApplySettings();
        }

        private void TryApplySettings()
        {
            if (fwhTables == null)
            {
                fwhTables = new FrameWithHistory(FrmMain);

                List<Button> buttons = new List<Button>();
                foreach(var item in SpButtons.Children)
                {
                    if (item is Button) 
                        buttons.Add((Button)item);
                }

                btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnRoles_Click(BtnRoles, new RoutedEventArgs());
            }
        }

        private void BtnRoles_Click(object sender, RoutedEventArgs e)
        {
            fwhTables.Navigate(new RolesPage());
            btnSel.SelectButton((Button)sender);
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            fwhTables.Navigate(new WorkersPage());
            btnSel.SelectButton((Button)sender);
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
