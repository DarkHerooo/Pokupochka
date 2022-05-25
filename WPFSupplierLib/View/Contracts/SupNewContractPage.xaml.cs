using DbLib.DB.Entity;
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

namespace WPFSupplierLib.View.Contracts
{
    /// <summary>
    /// Логика взаимодействия для SupNewContractPage.xaml
    /// </summary>
    public partial class SupNewContractPage : Page
    {
        private Counterparty _counterparty = null!;
        public SupNewContractPage(Counterparty counterparty)
        {
            InitializeComponent();

            _counterparty = counterparty;
            DataContext = _counterparty;

            SpProducts.Children.Add(CreateProductTemplate());
            SpProducts.Children.Add(CreateProductTemplate());
            SpProducts.Children.Add(CreateProductTemplate());
        }

        private Border CreateProductTemplate()
        {
            Border borderProductTemplate = new();
            borderProductTemplate.BorderBrush = Brushes.Black;
            borderProductTemplate.BorderThickness = new Thickness(2);
            borderProductTemplate.Margin = new Thickness(5);
            borderProductTemplate.Height = 150;
            {
                Grid grProductTemplate = new();
                grProductTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
                grProductTemplate.ColumnDefinitions.Add(new ColumnDefinition());
                grProductTemplate.Margin = new Thickness(5);

                // Image

                Grid grData = new();
                grData.RowDefinitions.Add(new RowDefinition());
                grData.RowDefinitions.Add(new RowDefinition());
                Grid.SetColumn(grData, 1);
                {
                    Grid grProductName = new();
                    grProductName.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
                    grProductName.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(grProductName, 0);
                    {
                        TextBlock tblProductName = new();
                        tblProductName.Margin = new Thickness(5, 0, 0, 0);
                        tblProductName.Text = "Название продукции";
                        Grid.SetRow(tblProductName, 0);
                        grProductName.Children.Add(tblProductName);

                        TextBox tbProductName = new();
                        tbProductName.Margin = new Thickness(5);
                        Grid.SetRow(tbProductName, 1);
                        grProductName.Children.Add(tbProductName);
                    }
                    grData.Children.Add(grProductName);

                    Grid grProductPrice = new();
                    grProductPrice.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
                    grProductPrice.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(grProductPrice, 1);
                    {
                        TextBlock tblProductPrice = new();
                        tblProductPrice.Margin = new Thickness(5, 0, 0, 0);
                        tblProductPrice.Text = "Цена";
                        Grid.SetRow(tblProductPrice, 0);
                        grProductPrice.Children.Add(tblProductPrice);

                        TextBox tbProductPrice = new();
                        tbProductPrice.Margin = new Thickness(5);
                        Grid.SetRow(tbProductPrice, 1);
                        grProductPrice.Children.Add(tbProductPrice);
                    }
                    grData.Children.Add(grProductPrice);
                }
                grProductTemplate.Children.Add(grData);

                borderProductTemplate.Child = grProductTemplate;
            }

            return borderProductTemplate;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
