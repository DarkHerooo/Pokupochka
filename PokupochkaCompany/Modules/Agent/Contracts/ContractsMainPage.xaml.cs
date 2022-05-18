using DbLib;
using GeneralLib;
using PokupochkaCompany.Modules.Agent.Contracts.Counterparties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokupochkaCompany.Modules.Agent.Contracts
{
    /// <summary>
    /// Логика взаимодействия для ContractsMainPage.xaml
    /// </summary>
    public partial class ContractsMainPage : Page
    {
        private FrameWithHistory _fwhTables = null!;
        private ButtonSelector _btnSel = null!;

        public ContractsMainPage()
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
                foreach (var item in SpButtons.Children)
                {
                    if (item is Button)
                        buttons.Add((Button)item);
                }

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnSuppliers_Click(null!, null!);
            }
        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Role? role = DbConnect.Db.Roles.FirstOrDefault(r => r.Id == (int)RoleKey.Supplier);
            CntrContractsPage page = new(role!);
            page.Title = "SuppsContracts";
            _fwhTables.Navigate(page);
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            Role? role = DbConnect.Db.Roles.FirstOrDefault(r => r.Id == (int)RoleKey.Client);
            CntrContractsPage page = new(role!);
            page.Title = "ClientsContracts";
            _fwhTables.Navigate(page);
        }
    }
}
