using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
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

namespace WPFSupplierLib.View.Requests
{
    /// <summary>
    /// Логика взаимодействия для SupRequestsPage.xaml
    /// </summary>
    public partial class SupRequestsPage : Page
    {
        public SupRequestsPage()
        {
            InitializeComponent();
            CreateFilterList();
        }

        private List<Request> GetRequests()
        {
            Counterparty counterparty = CurrentUser.User.Counterparty!;
            List<Request> requests = DbConnect.Db.Requests.
                Where(r => r.CounterpartyId == counterparty.Id)
                .Include(r => r.Status)
                .Include(r => r.ProductRequests)
                .ToList();

            if (!string.IsNullOrEmpty(TbFinder.Text) && !string.IsNullOrWhiteSpace(TbFinder.Text))
                requests = FindRequests(requests);

            requests = FilterRequests(requests);

            return requests;
        }

        private List<Request> FindRequests(List<Request> requests)
        {
            requests = requests.Where(r =>
                r.Number.ToString().ToLower().Contains(TbFinder.Text.ToLower())).ToList();

            return requests;
        }

        private List<Request> FilterRequests(List<Request> requests)
        {
            switch(CbFilter.SelectedIndex)
            {
                case 0:
                    requests = requests.Where(r =>
                    r.StatusId == (int)StatusKey.Considered).ToList(); break;
                case 1:
                    requests = requests.Where(r =>
                    r.StatusId == (int)StatusKey.InTheWay ||
                    r.StatusId == (int)StatusKey.Delivered).ToList(); break;
            }

            return requests;
        }

        private void CreateFilterList()
        {
            CbFilter.Items.Add("Новые");
            CbFilter.Items.Add("В пути/Доставленные");

            CbFilter.SelectedIndex = 0;
        }

        private void TbFinder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Page_Loaded(null!, null!);
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Page_Loaded(null!, null!);
        }

        private void DgRequests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnOpenRequest_Click(null!, null!);
        }

        private void DgRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnOpenRequest.IsEnabled = DgRequests.SelectedItem != null ? true : false;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TbFinder.Text = "";
            CbFilter.SelectedIndex = 0;
        }

        private void BtnOpenRequest_Click(object sender, RoutedEventArgs e)
        {
            Request? request = DgRequests.SelectedItem as Request;

            if (request != null)
                NavigationService.Navigate(new SupShowRequestPage(request));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DgRequests.ItemsSource = GetRequests();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Page_Loaded(null!, null!);
        }
    }
}
