using DbLib.DB.Entity;
using DbLib.DB.Enums;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPFAgentLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для ShowContractPage.xaml
    /// </summary>
    public partial class ShowContractPage : Page
    {
        Contract _contract;
        public ShowContractPage(Contract contract)
        {
            InitializeComponent();

            _contract = contract;
            SetData();
        }

        public void SetData()
        {
            int countYears = _contract.CountYears;
            DateTime dateStart = DateTime.Now;
            DateTime dateOver = dateStart.AddYears(countYears);
            TbDateStart.Text = dateStart.ToShortDateString();
            TbDateOver.Text = dateOver.ToShortDateString();
            DgProducts.ItemsSource = _contract.Products;

            _contract.DateStart = dateStart;
            _contract.DateOver = dateOver;
            DataContext = _contract;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _contract.StatusId = (int)StatusKey.Cancel;
            _contract.AddOrChange();

            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            _contract.StatusId = (int)StatusKey.Active;
            _contract.AddOrChange();

            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}
