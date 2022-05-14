using DbLib;
using PokupochkaCompany.Classes;
using PokupochkaCompany.Modules.Administrator.Tables;
using System.Collections.Generic;
using System.Linq;
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
        private FrameWithHistory _fwhTables = null!;
        private ButtonSelector _btnSel = null!;

        public MainTablePage()
        {
            InitializeComponent();

            TryApplySettings();
        }

        private void TryApplySettings()
        {
            if (_fwhTables == null)
            {
                _fwhTables = new FrameWithHistory(FrmMain);

                List<Button> buttons = new List<Button>();
                foreach(var item in SpButtons.Children)
                {
                    if (item is Button) 
                        buttons.Add((Button)item);
                }

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnRoles_Click(BtnRoles, new RoutedEventArgs());
            }
        }

        private void BtnRoles_Click(object sender, RoutedEventArgs e)
        {
            _fwhTables.Navigate(new RolesPage());
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            _fwhTables.Navigate(new WorkersPage());
        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Role? role = DbConnect.Db.Roles.FirstOrDefault(r => r.Id == (int)RoleKey.Supplier);
            CntrpartiesPage page = new(role!);
            page.Title = "SuppliersPage";
            _fwhTables.Navigate(page);
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            Role? role = DbConnect.Db.Roles.FirstOrDefault(r => r.Id == (int)RoleKey.Client);
            CntrpartiesPage page = new(role!);
            page.Title = "ClientsPage";
            _fwhTables.Navigate(page);
        }
    }
}
