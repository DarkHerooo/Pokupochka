using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using StylesLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdministratorWPF.View.Tables
{
    public partial class MainTablePage : Page
    {
        private FrameWithHistory _fwhTables = null!;
        private ButtonSelector _btnSel = null!;

        public MainTablePage()
        {
            InitializeComponent();

            SetSettings();
        }

        /// <summary>
        /// Устанавливает необходимые настройки для страницы
        /// </summary>
        private void SetSettings()
        {
            if (_fwhTables == null)
            {
                List<Button> buttons = new();
                foreach (var item in SpButtons.Children)
                    if (item is Button) buttons.Add((Button)item);

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                _fwhTables = new FrameWithHistory(FrmMain);
                BtnRoles_Click(null!, null!);
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
