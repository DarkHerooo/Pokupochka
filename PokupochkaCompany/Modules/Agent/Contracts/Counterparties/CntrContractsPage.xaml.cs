using DbLib;
using Microsoft.EntityFrameworkCore;
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

namespace PokupochkaCompany.Modules.Agent.Contracts.Counterparties
{
    /// <summary>
    /// Логика взаимодействия для CntrContractsPage.xaml
    /// </summary>
    public partial class CntrContractsPage : Page
    {
        Role _role;
        public CntrContractsPage(Role role)
        {
            InitializeComponent();

            _role = role;
        }

        public Contract[] GetNewContracts()
        {
            List<Contract> contracts = DbConnect.Db.Contracts.Include(c => c.Counterparty).Include(c => c.Products).ToList();
            contracts = DbConnect.Db.Contracts.Where(c => c.Counterparty!.User!.Role! == _role &&
                c.Status!.Id == (int)StatusKey.New).ToList();

            return contracts.ToArray();
        }

        public Contract[] GetTodayContracts()
        {
            List<Contract> contracts = DbConnect.Db.Contracts.Include(c => c.Counterparty).Include(c => c.Status).Include(c => c.Products).ToList();
            contracts = DbConnect.Db.Contracts.Where(c => c.Counterparty!.User!.Role! == _role &&
                c.DateStart!.Value.Date == DateTime.Today.Date && c.Status!.Id != (int)StatusKey.New).ToList();

            return contracts.ToArray();
        }

        private void BtnOpenContract_Click(object sender, RoutedEventArgs e)
        {
            Contract? contract = DgNewContracts.SelectedItem as Contract;

            if (contract != null)
            {
                NavigationService.Navigate(new ShowContractPage(contract));
                return;
            }

            contract = DgTodayContracts.SelectedItem as Contract;

            if (contract != null)
            {
                NavigationService.Navigate(new ShowContractPage(contract));
                return;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DgNewContracts.ItemsSource = GetNewContracts();
            DgTodayContracts.ItemsSource = GetTodayContracts();
        }

        private void Dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            Contract? contract = dg.SelectedItem as Contract;

            if (contract != null)
                NavigationService.Navigate(new ShowContractPage(contract));
        }

        private void DgNewContracts_GotFocus(object sender, RoutedEventArgs e)
        {
            DgTodayContracts.UnselectAll();
        }

        private void DgTodayContracts_GotFocus(object sender, RoutedEventArgs e)
        {
            DgNewContracts.UnselectAll();
        }
    }
}
