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
            SetPageSettings();
        }

        public void SetPageSettings()
        {
            if (_contract.StatusId == (int)StatusKey.Сonsidered)
            {
                BtnAccept.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Visible;
            }
            else if (_contract.StatusId == (int)StatusKey.Active)
            {
                BtnAccept.Visibility = Visibility.Hidden;
                Grid.SetColumn(BtnCancel, 1);
                Grid.SetColumnSpan(BtnCancel, 2);
            }
            else if (_contract.StatusId == (int)StatusKey.Cancel)
            {
                BtnCancel.Visibility = Visibility.Hidden;
                Grid.SetColumn(BtnAccept, 1);
                Grid.SetColumnSpan(BtnAccept, 2);
            }
            else
            {
                BtnAccept.Visibility = Visibility.Hidden;
                BtnCancel.Visibility = Visibility.Hidden;
            }

            DgProducts.ItemsSource = _contract.Products;
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
