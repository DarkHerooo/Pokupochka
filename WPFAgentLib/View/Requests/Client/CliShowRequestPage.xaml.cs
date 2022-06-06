using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib.Usr;
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

namespace WPFAgentLib.View.Requests.Client
{
    /// <summary>
    /// Логика взаимодействия для ShowRequestPage.xaml
    /// </summary>
    public partial class CliShowRequestPage : Page
    {
        private Request _request = null!;
        public CliShowRequestPage(Request request)
        {
            _request = request;
            InitializeComponent();
            SetPageSettings();
        }

        private void SetPageSettings()
        {
            DataContext = _request;
            DgProducts.ItemsSource = _request.ProductRequests.ToList();

            if (_request.StatusId != (int)StatusKey.Considered)
            {
                BtnAccept.Visibility = Visibility.Hidden;
                BtnCancel.Visibility = Visibility.Hidden;
            }

            foreach(var productRequest in _request.ProductRequests)
            {
                if (productRequest.Count > productRequest.Product.CountInStock)
                {
                    BtnAccept.Content = "Товара нет на складе!";
                    BtnAccept.IsEnabled = false;
                    break;
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _request.StatusId = (int)StatusKey.Cancel;
            _request.AddOrChange();
            BtnBack_Click(null!, null!);
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            _request.StatusId = (int)StatusKey.InTheWay;
            foreach (var productRequest in _request.ProductRequests)
                productRequest.Product.CountInStock -= productRequest.Count;

            _request.AddOrChange();
            BtnBack_Click(null!, null!);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}
