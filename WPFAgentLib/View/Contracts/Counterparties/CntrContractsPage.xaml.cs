using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WPFAgentLib.View.Contracts
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
            CreateFilterList();
        }

        private List<Contract> GetContracts()
        {
            List<Contract> contracts = DbConnect.Db.Contracts.Include(c => c.Counterparty).Include(c => c.Products).Include(c => c.Status).ToList();

            if (!string.IsNullOrEmpty(TbFinder.Text) && !string.IsNullOrWhiteSpace(TbFinder.Text))
                contracts = FindContracts(contracts);

            contracts = FilterContracts(contracts);

            return contracts;
        }
        
        private List<Contract> FindContracts(List<Contract> contracts)
        {
            contracts = contracts.Where(c =>
                c.Number.ToString().ToLower().Contains(TbFinder.Text.ToLower()) ||
                c.Counterparty!.Company.ToLower().Contains(TbFinder.Text.ToLower())).ToList();

            return contracts;
        }

        private List<Contract> FilterContracts(List<Contract> contracts)
        {
            switch (CbFilter.SelectedIndex)
            {
                case 0:
                    contracts = contracts.Where(c =>
                    c.StatusId == (int)StatusKey.Considered &&
                    c.Counterparty!.User!.Role! == _role).ToList(); break;
                case 1: contracts = contracts.Where(c => 
                    (c.StatusId == (int)StatusKey.Active ||
                    c.StatusId == (int)StatusKey.Cancel) &&
                    c.Counterparty!.User!.Role! == _role).ToList(); break;
            }

            return contracts;
        }

        private void CreateFilterList()
        {
            CbFilter.Items.Add("Новые");
            CbFilter.Items.Add("Активные/Отклонённые");

            CbFilter.SelectedIndex = 0;
        }

        private void BtnOpenContract_Click(object sender, RoutedEventArgs e)
        {
            Contract? contract = DgNewContracts.SelectedItem as Contract;

            if (contract != null)
            {
                NavigationService.Navigate(new ShowContractPage(contract));
                return;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DgNewContracts.ItemsSource = GetContracts();
        }

        private void Dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnOpenContract_Click(null!, null!);
        }

        private void TbFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DgNewContracts.ItemsSource = GetContracts();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DgNewContracts.ItemsSource = GetContracts();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbFinder.Text = "";
            CbFilter.SelectedIndex = 0;
            DgNewContracts.ItemsSource = GetContracts();
        }
    }
}
