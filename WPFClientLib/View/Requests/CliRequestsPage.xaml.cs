using DbLib.DB;
using DbLib.DB.Entity;
using GeneralLib.Usr;
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

namespace WPFClientLib.View.Requests
{
    /// <summary>
    /// Логика взаимодействия для SuppRequestsPage.xaml
    /// </summary>
    public partial class CliRequestsPage : Page
    {
        private List<RequestTemplate> _requestTemplates = new();
        public CliRequestsPage()
        {
            InitializeComponent();
            ShowRequests();
        }

        private void ShowRequests()
        {
            _requestTemplates.Clear();
            SpSuppRequests.Children.Clear();

            List<Request> requests = GetRequests();

            int countInRow = 9;
            for (int i = 0; i <= requests.Count / countInRow; i++)
            {
                StackPanel spRow = new();
                spRow.Orientation = Orientation.Horizontal;

                if (i == 0)
                {
                    RequestTemplate newRequest = new(new Request());
                    newRequest.BrdRequest.Margin = new Thickness(5);
                    newRequest.BrdRequest.MouseLeftButtonDown += NewBrdRequest_MouseLeftButtonDown; ;
                    spRow.Children.Add(newRequest.BrdRequest);
                    _requestTemplates.Add(newRequest);
                    countInRow = 8;
                }
                else countInRow = 9;

                for (int j = i * countInRow; j < i * countInRow + countInRow; j++)
                {
                    if (j == requests.Count) break;

                    RequestTemplate requestTemplate = new(requests[j]);
                    requestTemplate.BrdRequest.Margin = new Thickness(5);
                    requestTemplate.BrdRequest.MouseLeftButtonDown += BrdRequest_MouseLeftButtonDown; ;
                    spRow.Children.Add(requestTemplate.BrdRequest);
                    _requestTemplates.Add(requestTemplate);
                }
                SpSuppRequests.Children.Add(spRow);
            }
        }

        private void BrdRequest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                Border border = (Border)sender;
                RequestTemplate? findTemplate = null;
                foreach (var requestTemplate in _requestTemplates)
                {
                    if (border == requestTemplate.BrdRequest)
                    {
                        findTemplate = requestTemplate;
                        break;
                    }
                }

                if (findTemplate != null)
                    NavigationService.Navigate(new CliShowRequestPage(findTemplate.Request));
            }
        }

        private void NewBrdRequest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
                NavigationService.Navigate(new CliNewRequestPage());
        }

        private List<Request> GetRequests()
        {
            User user = CurrentUser.User!;
            List<Request> requests = DbConnect.Db.Requests
                .Where(r => r.Counterparty! == user.Counterparty!)
                .Include(c => c.Status)
                .Include(c => c.ProductRequests).ToList();

            return requests;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DbConnect.Db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            ShowRequests();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Page_Loaded(null!, null!);
        }
    }
}
