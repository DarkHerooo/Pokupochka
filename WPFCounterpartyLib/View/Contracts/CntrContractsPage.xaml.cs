using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
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

            int countInRow = 10;
            for (int i = 0; i <= contracts.Count / countInRow; i++)
            {
                StackPanel spRow = new();
                spRow.Orientation = Orientation.Horizontal;
                for (int j = i * countInRow; j < i * countInRow + countInRow; j++)
                {
                    ContractTemplate contractTemplate = null!;
                    if (j == contracts.Count)
                    {
                        contractTemplate = new(new Contract());
                        contractTemplate.BrdContract.MouseLeftButtonDown += NewBrdContract_MouseLeftButtonDown;
                    }
                    else
                    {
                        contractTemplate = new(contracts[j]);
                        contractTemplate.BrdContract.MouseLeftButtonDown += BrdContract_MouseLeftButtonDown;
                    }

                    contractTemplate.BrdContract.Margin = new Thickness(5);
                    spRow.Children.Add(contractTemplate.BrdContract);
                    _contractTemplates.Add(contractTemplate);

                    if (j == contracts.Count) break;
                }
                SpContracts.Children.Add(spRow);
            }
        }

        private List<Contract> GetContracts()
        {
            List<Contract> contracts = DbConnect.Db.Contracts.Where(c => c.CounterpartyId == CurrentUser.User.Counterparty!.Id)
                .Include(c => c.Status).ToList();

            return contracts;
        }

        private void BrdContract_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            ContractTemplate? findTemplate = null;
            foreach(var contractTemplate in _contractTemplates)
            {
                if (border == contractTemplate.BrdContract)
                {
                    findTemplate = contractTemplate;
                    break;
                }
            }

            if (findTemplate != null)
            {

            }
        }

        private void NewBrdContract_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentUser.User.RoleId == (int)RoleKey.Supplier)
                NavigationService.Navigate(new SupNewContractPage(CurrentUser.User.Counterparty!));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowContracts();
        }
    }
}
