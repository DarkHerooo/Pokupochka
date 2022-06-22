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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();

            DgProducts.ItemsSource = Calculate();
        }

        public PopularProduct[] Calculate()
        {
            List<Request> requests = DbConnect.Db.Requests.Where(r => r.Counterparty!.User!.RoleId == (int)RoleKey.Client)
                .Include(r => r.ProductRequests)
                .ToList();

            List<ProductRequest> productRequests = DbConnect.Db.ProductRequests
                .Where(pr => pr.Request.Counterparty!.User!.RoleId == (int)RoleKey.Client)
                .Include(pr => pr.Product)
                .ToList();
            
            List<PopularProduct> popularProducts = new();
            foreach(var pr in productRequests)
            {
                PopularProduct? findPopular = popularProducts.FirstOrDefault(pp => pp.Product.Id == pr.ProductId);
                if (findPopular == null)
                {
                    PopularProduct popularProduct = new(pr.Product!, pr.Count, pr.Request!.Price);
                    popularProducts.Add(popularProduct);
                }
                else
                {
                    findPopular.Count += pr.Count;
                    findPopular.Price += pr.Request!.Price;
                }
            }

            popularProducts = popularProducts.OrderByDescending(pp => pp.Count).ToList();

            int countInTop = 10;
            if (popularProducts.Count > countInTop)
                popularProducts.RemoveRange(countInTop, popularProducts.Count - countInTop);

            return popularProducts.ToArray();
        }

        public class PopularProduct
        {
            public Product Product { get; set; } = null!;
            public int Count { get; set; }
            public decimal Price { get; set; }

            public PopularProduct(Product product, int count, decimal price)
            {
                Product = product;
                Count = count;
                Price = price;
            }
        }
    }
}
