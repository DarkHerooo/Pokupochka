using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.Usr;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WPFClientLib.View.Contracts;
using WPFSupplierLib.View.Contracts;

namespace WPFCounterpartyLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для CntrContractsPage.xaml
    /// </summary>
    public partial class CntrContractsPage : Page
    {
        private List<ContractTemplate> _contractTemplates = new();
        public CntrContractsPage()
        {
            InitializeComponent();

            ShowContracts();
        }

        private void ShowContracts()
        {
            _contractTemplates.Clear();
            SpContracts.Children.Clear();

            List<Contract> contracts = GetContracts();

            int countInRow = 9;
            for (int i = 0; i <= contracts.Count / countInRow; i++)
            {
                StackPanel spRow = new();
                spRow.Orientation = Orientation.Horizontal;

                if (i == 0)
                {
                    ContractTemplate newContract = new(new Contract());
                    newContract.BrdContract.Margin = new Thickness(5);
                    newContract.BrdContract.MouseLeftButtonDown += NewBrdContract_MouseLeftButtonDown;
                    spRow.Children.Add(newContract.BrdContract);
                    _contractTemplates.Add(newContract);
                    countInRow = 8;
                }
                else countInRow = 9;

                for (int j = i * countInRow; j < i * countInRow + countInRow; j++)
                {
                    if (j == contracts.Count) break;

                    ContractTemplate contractTemplate = new(contracts[j]);
                    contractTemplate.BrdContract.Margin = new Thickness(5);
                    contractTemplate.BrdContract.MouseLeftButtonDown += BrdContract_MouseLeftButtonDown;
                    spRow.Children.Add(contractTemplate.BrdContract);
                    _contractTemplates.Add(contractTemplate);
                }
                SpContracts.Children.Add(spRow);
            }
        }

        private List<Contract> GetContracts()
        {
            User user = CurrentUser.User!;
            List<Contract> contracts = DbConnect.Db.Contracts.Where(c => c.CounterpartyId == user.Counterparty!.Id)
                .Include(c => c.Status).Include(c => c.Products).ToList();

            return contracts;
        }

        private void BrdContract_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                Border border = (Border)sender;
                ContractTemplate? findTemplate = null;
                foreach (var contractTemplate in _contractTemplates)
                {
                    if (border == contractTemplate.BrdContract)
                    {
                        findTemplate = contractTemplate;
                        break;
                    }
                }

                if (findTemplate != null)
                    NavigationService.Navigate(new CntrShowContract(findTemplate.Contract));
            }
        }

        private void NewBrdContract_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            User user = CurrentUser.User!;
            switch(user.RoleId)
            {
                case (int)RoleKey.Supplier:
                    NavigationService.Navigate(new SupNewContractPage(user.Counterparty!)); break;
                case (int)RoleKey.Client:
                    NavigationService.Navigate(new CliNewContractPage(user.Counterparty!)); break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowContracts();
        }
    }
}
