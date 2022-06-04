using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
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

namespace WPFClientLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для CliNewContractPage.xaml
    /// </summary>
    public partial class CliNewContractPage : Page
    {
        private Counterparty _counterparty = null!;
        private List<Product> _cntrActiveProducts = new();
        private List<Product> _selectProducts = new();
        public CliNewContractPage(Counterparty counterparty)
        {
            _counterparty = counterparty;
            InitializeComponent();

            SetCntrActiveProducts();
            ShowProducts();
        }

        private void SetCntrActiveProducts()
        {
            List<Contract> contracts = 
                DbConnect.Db.Contracts.Where(c => 
                c.CounterpartyId == _counterparty.Id && 
                c.StatusId == (int)StatusKey.Active)
                .Include(c => c.Products)
                .ToList();

            foreach (var contract in contracts)
            {
                foreach (var product in contract.Products)
                    _cntrActiveProducts.Add(product);
            }
        }

        private void ShowProducts()
        {
            LbAllProducts.ItemsSource = null;
            LbAllProducts.ItemsSource = GetAllProducts();

            LbSelectedProducts.ItemsSource = null;
            LbSelectedProducts.ItemsSource = _selectProducts;
        }

        private List<Product> GetAllProducts()
        {
            List<Product> products = DbConnect.Db.Products.Where(p => p.CountInStock > 0).ToList();

            foreach(var product in _cntrActiveProducts)
                products.Remove(product);

            foreach (var selectProduct in _selectProducts)
                products.Remove(selectProduct);

            if (String.IsNullOrEmpty(TbFindProducts.Text) || String.IsNullOrWhiteSpace(TbFindProducts.Text))
                products = products.Where(p => p.Title.ToLower().Contains(TbFindProducts.Text.ToLower())).ToList();

            return products;
        }

        private void BtnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            Product? product = LbAllProducts.SelectedItem as Product;

            if (product != null)
            {
                _selectProducts.Add(product);
                ShowProducts();
            }
        }

        private void LbAllProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnGetProduct_Click(null!, null!);
        }

        private void LbAllProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnGetProduct.IsEnabled = LbAllProducts.SelectedItem != null ? true : false;
        }

        private void BtnCancelProduct_Click(object sender, RoutedEventArgs e)
        {
            Product? product = LbSelectedProducts.SelectedItem as Product;

            if (product != null)
            {
                _selectProducts.Remove(product);
                ShowProducts();
            }
        }

        private void LbSelectedProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnCancelProduct_Click(null!, null!);
        }

        private void LbSelectedProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnCancelProduct.IsEnabled = LbSelectedProducts.SelectedItem != null ? true : false;
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbFindProducts.Text = "";
            ShowProducts();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "Введены некорректные данные:\n";
            bool allTrueData = true;

            if (_selectProducts.Count == 0)
            {
                allTrueData = false;
                errorMessage += "Не выбран ни один товар\n";
            }

            if (!int.TryParse(TbCountYears.Text, out _))
            {
                allTrueData = false;
                errorMessage += "Количество лет\n";
            }

            if (allTrueData)
            {
                Contract contract = new();
                contract.Counterparty = _counterparty;
                contract.StatusId = (int)StatusKey.Сonsidered;
                contract.CountYears = int.Parse(TbCountYears.Text);
                contract.DateStart = DateTime.Now;
                contract.DateOver = DateTime.Now.AddYears(contract.CountYears);

                int contractNumber = 100000;
                if (DbConnect.Db.Contracts.Count() > 0)
                    contractNumber = DbConnect.Db.Contracts.Max(c => c.Number) + 1;
                contract.Number = contractNumber;

                foreach (var product in _selectProducts)
                    contract.Products.Add(product);

                contract.AddOrChange();
                BtnCancel_Click(null!, null!);
            }
            else
                MessageBox.Show(errorMessage, "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TbFindProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ShowProducts();
        }
    }
}
