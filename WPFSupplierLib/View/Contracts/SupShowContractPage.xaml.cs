﻿using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using StylesLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPFSupplierLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для SupShowContractPage.xaml
    /// </summary>
    public partial class SupShowContractPage : Page
    {

        private Contract _contract = null!;
        public SupShowContractPage(Contract contract)
        {
            InitializeComponent();

            _contract = contract;
            DataContext = contract;
            DgProducts.ItemsSource = contract.Products;
            SetPageSettings();
        }

        private void SetPageSettings()
        {
            if (_contract.StatusId == (int)StatusKey.Stop)
            {
                BtnStartOrStop.Style = ButtonStyles.GreenButton;
                BtnStartOrStop.Content = "Возобновить";
            }
            else if (_contract.StatusId == (int)StatusKey.Active)
            {
                BtnStartOrStop.Style = ButtonStyles.YellowButton;
                BtnStartOrStop.Content = "Приостановить";
            }
            else BtnStartOrStop.Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnStartOrStop_Click(object sender, RoutedEventArgs e)
        {
            if (_contract.StatusId == (int)StatusKey.Stop)
                _contract.StatusId = (int)StatusKey.Active;
            else if (_contract.StatusId == (int)StatusKey.Active)
                _contract.StatusId = (int)StatusKey.Stop;

            DbConnect.Db.SaveChanges();
            BtnBack_Click(null!, null!);
        }
    }
}
