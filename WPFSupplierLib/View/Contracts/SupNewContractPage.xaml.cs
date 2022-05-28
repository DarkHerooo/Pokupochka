using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
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
        private List<ProductTemplate> productTemplates = new();
        public SupNewContractPage(Counterparty counterparty)
        {
            InitializeComponent();

            _counterparty = counterparty;
            DataContext = _counterparty;
            BtnAddNewProduct_Click(null!, null!);
        }

        private void BtnAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductTemplate newProduct = new();
            newProduct.BtnDelete.Click += BtnDelete_Click;
            productTemplates.Add(newProduct);
            SpProducts.Children.Add(newProduct.BrdProduct);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btnDelete = (Button)sender;
            ProductTemplate? findProduct = null;

            foreach(var template in productTemplates)
            {
                if (template.BtnDelete == btnDelete)
                {
                    findProduct = template;
                    break;
                }
            }

            if (findProduct != null)
            {
                SpProducts.Children.Remove(findProduct.BrdProduct);
                productTemplates.Remove(findProduct);
            }
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

            if (productTemplates.Count == 0)
            {
                errorMessage = "В договоре должен быть как минимум 1 товар";
                allTrueData = false;
            }    

            for (int i = 0; i < productTemplates.Count; i++)
            {
                if (!productTemplates[i].CheckData())
                {
                    allTrueData = false;
                    errorMessage += "Продукт #" + i + "\n";
                }
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

                int contractNumber = DbConnect.Db.Contracts.Max(c => c.Number);
                contract.Number = contractNumber != 0 ? contractNumber + 1 : 100000;

                foreach (var productTemplate in productTemplates)
                {
                    Product product = new();
                    product.Title = productTemplate.TbName.Text;
                    product.Price = double.Parse(productTemplate.TbPrice.Text);
                    product.Image = productTemplate.ImageBytes;
                    DbConnect.Db.Products.Add(product);
                    contract.Products.Add(product);
                }

                DbConnect.Db.Contracts.Add(contract);
                DbConnect.Db.SaveChanges();

                BtnCancel_Click(null!, null!);
            }
            else
                MessageBox.Show(errorMessage, "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
