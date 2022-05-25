using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
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
using WPFSupplierLib.View.Contracts;

namespace WPFCounterpartyLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для CntrContractsPage.xaml
    /// </summary>
    public partial class CntrContractsPage : Page
    {
        public CntrContractsPage()
        {
            InitializeComponent();
        }

        private void BtnNewContract_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.User.RoleId == (int)RoleKey.Supplier)
                NavigationService.Navigate(new SupNewContractPage(CurrentUser.User.Counterparty!));
        }

        private void BtnOpenContract_Click(object sender, RoutedEventArgs e)
        {
            Contract? contract = DgContracts.SelectedItem as Contract;

            //if (contract != null)
                //NavigationService.Navigate(new ShowContractPage(contract));
        }

        private void DgContracts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnOpenContract_Click(null!, null!);
        }
    }
}
