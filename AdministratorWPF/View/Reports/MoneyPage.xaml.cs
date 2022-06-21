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

        private MoneyInfo CalculateSupProducts()
        {
            List<Request> supRequests = DbConnect.Db.Requests
                .Where(c => c.Counterparty!.User!.RoleId == (int)RoleKey.Supplier &&
                c.StatusId == (int)StatusKey.Delivered)
                .Include(c => c.ProductRequests)
                .ToList();

            decimal supfullPrice = supRequests.Select(sr => sr.Price).Sum();
            int countProducts = supRequests.Select(sr => sr.ProductRequests.Select(pr => pr.Count).Sum()).Sum();

            MoneyInfo moneyInfo = new("Купленные товары", countProducts, supfullPrice);
            return moneyInfo;
        }

        private MoneyInfo CalculateCliProducts()
        {
            List<Request> supRequests = DbConnect.Db.Requests
                .Where(c => c.Counterparty!.User!.RoleId == (int)RoleKey.Client &&
                c.StatusId == (int)StatusKey.Delivered)
                .Include(c => c.ProductRequests)
                .ToList();

            decimal supfullPrice = supRequests.Select(sr => sr.Price).Sum();
            int countProducts = supRequests.Select(sr => sr.ProductRequests.Select(pr => pr.Count).Sum()).Sum();

            MoneyInfo moneyInfo = new("Проданные товары", countProducts, supfullPrice);
            return moneyInfo;
        }

        private MoneyInfo CalculateAll()
        {
            MoneyInfo sup = CalculateSupProducts();
            MoneyInfo cli = CalculateCliProducts();

            decimal fullPrice = cli.FullPrice - sup.FullPrice;
            int fullCount = cli.CountProducts + sup.CountProducts;

            MoneyInfo moneyInfo = new("Итог", fullCount, fullPrice);
            return moneyInfo;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<MoneyInfo> moneyInfos = new();
            moneyInfos.Add(CalculateSupProducts());
            moneyInfos.Add(CalculateCliProducts());
            moneyInfos.Add(CalculateAll());

            DgMoneyInfo.ItemsSource = moneyInfos.ToList();
        }

        private class MoneyInfo
        {
            public string Name { get; set; } = null!;
            public int CountProducts { get; set; }
            public decimal FullPrice { get; set; }

            public MoneyInfo(string name, int countProducts, decimal fullPrice)
            {
                Name = name;
                CountProducts = countProducts;
                FullPrice = fullPrice;
            }
        }
    }
}
