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

namespace WPFAgentLib.View.Requests.Supplier
{
    /// <summary>
    /// Логика взаимодействия для AgentNewRequestPage.xaml
    /// </summary>
    public partial class AgentNewRequestPage : Page
    {
        private List<ProductItemTemplate> _productItemTemplates = new();
        private Counterparty _counterparty = null!;
        public AgentNewRequestPage()
        {
            InitializeComponent();
            ShowProducts();
        }

        private void ShowProducts()
        {
            LbAllProducts.ItemsSource = null;
            LbAllProducts.ItemsSource = GetAllProducts();

            LbSelectedProducts.Items.Clear();
            foreach (var productItemTemplate in _productItemTemplates)
                LbSelectedProducts.Items.Add(productItemTemplate.GridProductItemTemplate);
        }

        private List<Product> GetAllProducts()
        {
            List<Product> products = new();
            List<Contract> contracts = DbConnect.Db.Contracts.Where(c =>
                c.StatusId == (int)StatusKey.Active)
                .Include(c => c.Products)
                .ToList();

            if (_productItemTemplates.Count > 0)
            {
                Product product = _productItemTemplates[0].Product;
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

                foreach (var productItemTemplate in _productItemTemplates)
                    products.Remove(productItemTemplate.Product);
            }
            else
            {
                foreach (var conract in contracts)
                    products.AddRange(conract.Products);
            }

            if (!String.IsNullOrEmpty(TbFindProducts.Text) && !String.IsNullOrWhiteSpace(TbFindProducts.Text))
                products = products.Where(p => p.Title.ToLower().Contains(TbFindProducts.Text.ToLower())).ToList();

            return products;
        }

        private void BtnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            Product? product = LbAllProducts.SelectedItem as Product;

            if (product != null)
            {
                ProductItemTemplate productItemTemplate = new(product);
                productItemTemplate.TbCount.TextChanged += TbCount_TextChanged;
                _productItemTemplates.Add(productItemTemplate);
                ShowProducts();
            }
        }

        private void TbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string 
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
                ProductItemTemplate? productItemTemplate = _productItemTemplates.FirstOrDefault(pit => pit.GridProductItemTemplate == gridProductItemTemplate);
                if (productItemTemplate != null)
                {
                    _productItemTemplates.Remove(productItemTemplate);
                    ShowProducts();
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

            if (_productItemTemplates.Count == 0)
            {
                errorMessage += "Не выбран ни один товар\n";
                trueData = false;
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
                /*Contract contract = new();
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
                BtnCancel_Click(null!, null!);*/
            }
        }
    }
}
