using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.Usr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFClientLib.View.Requests
{
    /// <summary>
    /// Логика взаимодействия для AgentNewRequestPage.xaml
    /// </summary>
    public partial class CliNewRequestPage : Page
    {
        private List<CliProductItemTemplate> _cliProductItemTemplates = new();
        private Counterparty _counterparty = null!;
        public CliNewRequestPage()
        {
            InitializeComponent();
            ShowProducts();
        }

        private void ShowProducts()
        {
            LbAllProducts.ItemsSource = null;
            LbAllProducts.ItemsSource = GetAllProducts();

            LbSelectedProducts.Items.Clear();
            foreach (var cliProductItemTemplate in _cliProductItemTemplates)
                LbSelectedProducts.Items.Add(cliProductItemTemplate.GridProductItemTemplate);
        }

        private List<Product> GetAllProducts()
        {
            List<Product> products = new();
            List<Contract> contracts = DbConnect.Db.Contracts.Where(c =>
                c.StatusId == (int)StatusKey.Active && c.Counterparty! == CurrentUser.User.Counterparty!)
                .Include(c => c.Products)
                .ToList();

            if (_cliProductItemTemplates.Count > 0)
            {
                Product product = _cliProductItemTemplates[0].Product;
                foreach (var contract in contracts)
                {
                    bool findContract = false;
                    foreach (var contractProduct in contract.Products)
                    {
                        if (contractProduct == product)
                        {
                            products.AddRange(contract.Products);
                            findContract = true;
                            break;
                        }
                    }

                    if (findContract)
                    {
                        _counterparty = contract.Counterparty!;
                        break;
                    }
                }

                foreach (var productItemTemplate in _cliProductItemTemplates)
                    products.Remove(productItemTemplate.Product);
            }
            else
            {
                foreach (var conract in contracts)
                    products.AddRange(conract.Products);
            }

            products = products.Where(p => p.CountInStock > 0).ToList();

            if (!String.IsNullOrEmpty(TbFindProducts.Text) && !String.IsNullOrWhiteSpace(TbFindProducts.Text))
                products = products.Where(p => p.Title.ToLower().Contains(TbFindProducts.Text.ToLower())).ToList();

            return products;
        }

        private void TbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal fullPrice = 0;
            foreach (var pit in _cliProductItemTemplates)
                fullPrice += pit.CompanyPrice;

            TblFullPrice.Text = fullPrice.ToString();
        }

        private void BtnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            Product? product = LbAllProducts.SelectedItem as Product;

            if (product != null)
            {
                CliProductItemTemplate productItemTemplate = new(product);
                productItemTemplate.TbCount.TextChanged += TbCount_TextChanged;
                _cliProductItemTemplates.Add(productItemTemplate);
                ShowProducts();
                TbCount_TextChanged(null!, null!);
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
            Grid? gridProductItemTemplate = LbSelectedProducts.SelectedItem as Grid;

            if (gridProductItemTemplate != null)
            {
                CliProductItemTemplate? productItemTemplate = _cliProductItemTemplates.FirstOrDefault(pit => pit.GridProductItemTemplate == gridProductItemTemplate);
                if (productItemTemplate != null)
                {
                    _cliProductItemTemplates.Remove(productItemTemplate);
                    ShowProducts();
                    TbCount_TextChanged(null!, null!);
                }
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
        private void TbFindProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ShowProducts();
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

        private bool CheckData()
        {
            bool trueData = true;
            string errorMessage = "Введены некорректные данные:\n";

            if (_cliProductItemTemplates.Count == 0)
            {
                errorMessage += "Не выбран ни один товар\n";
                trueData = false;
            }

            for (int i = 0; i < _cliProductItemTemplates.Count(); i++)
            {
                if (!_cliProductItemTemplates[i].CheckData())
                {
                    errorMessage += "Товар #" + (i + 1) + "\n";
                    trueData = false;
                }
            }

            if (!trueData)
                MessageBox.Show(errorMessage, "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

            return trueData;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                Request request = new();
                request.Counterparty = _counterparty;
                request.Date = DateTime.Now.Date;
                request.StatusId = (int)StatusKey.Considered;

                int number = 100000;
                if (DbConnect.Db.Requests.Count() > 0)
                    number = DbConnect.Db.Requests.Max(r => r.Number) + 1;
                request.Number = number;

                decimal fullPrice = 0;
                foreach(var productItemTemplate in _cliProductItemTemplates)
                {
                    ProductRequest pr = new();
                    pr.Product = productItemTemplate.Product;
                    pr.Count = int.Parse(productItemTemplate.TbCount.Text);
                    fullPrice += productItemTemplate.CompanyPrice;
                    request.ProductRequests.Add(pr);
                }
                request.Price = fullPrice;
                request.AddOrChange();
                BtnCancel_Click(null!, null!);
            }
        }
    }
}
