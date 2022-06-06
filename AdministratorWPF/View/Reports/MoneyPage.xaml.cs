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

namespace WPFAdministratorLib.View.Reports
{
    /// <summary>
    /// Логика взаимодействия для MoneyPage.xaml
    /// </summary>
    public partial class MoneyPage : Page
    {
        public MoneyPage()
        {
            InitializeComponent();
        }

    /*    private void Calculate()
        {  
            List<Request> supRequests = DbConnect.Db.Requests
                .Where(c => c.Counterparty!.User!.RoleId == (int)RoleKey.Supplier)
                .Include(c => c.ProductRequests)
                .ToList();

            int supCountProducts = 0;
            decimal supFullPrice = 0;
            foreach(var request in supRequests)
            {
                foreach(var productRequest in request.ProductRequests)
                {
                    supCountProducts += productRequest.Count;
                    supFullPrice += productRequest.Product.Price;
                }
            }
            TblCountProductsBuy.Text = supCountProducts.ToString();
            TblProductsBuyPrice.Text = supFullPrice.ToString();

            List<Request> cliRequests = DbConnect.Db.Requests
                .Where(c => c.Counterparty!.User!.RoleId == (int)RoleKey.Client)
                .Include(c => c.ProductRequests)
                .ToList();

            int cliCountProducts = 0;
            decimal cliFullPrice = 0;
            foreach (var request in cliRequests)
            {
                foreach (var productRequest in request.ProductRequests)
                {
                    cliCountProducts += productRequest.Count;
                    cliFullPrice += productRequest.Product.Price;
                }
            }
            TblCountProductsSell.Text = cliCountProducts.ToString();
            TblProductsSellPrice.Text = cliFullPrice.ToString();

            decimal sum = cliFullPrice - supFullPrice;
            TblSumMoney.Text = sum.ToString();
            TblSum.Foreground = sum <= 0 ? Brushes.Red : Brushes.Green;
        }*/

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Calculate();
        }
    }
}
