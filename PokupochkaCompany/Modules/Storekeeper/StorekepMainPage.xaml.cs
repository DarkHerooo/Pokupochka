using PokupochkaCompany.Classes;
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
using PokupochkaCompany.Modules.Storekeeper.AcceptRequests;
using PokupochkaCompany.Modules.Storekeeper.Products;

namespace PokupochkaCompany.Storekeeper
{
    /// <summary>
    /// Логика взаимодействия для StorekepMainPage.xaml
    /// </summary>
    public partial class StorekepMainPage : Page
    {
        private FrameWithHistory fwhMain = null!;
        private ButtonSelector btnSel = null!;

        public StorekepMainPage()
        {
            InitializeComponent();

            TryApplySettings();
        }

        private void TryApplySettings()
        {
            if (fwhMain == null)
            {
                fwhMain = new FrameWithHistory(FrmMain);

                List<Button> buttons = new List<Button>();
                foreach (var item in GrButtons.Children)
                {
                    if (item is Button)
                        buttons.Add((Button)item);

                }

                btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnAcceptRequests_Click(BtnAcceptRequests, new RoutedEventArgs());
            }
        }

        private void BtnAcceptRequests_Click(object sender, RoutedEventArgs e)
        {
            fwhMain.Navigate(new AccRequestsPage());
            btnSel.SelectButton((Button)sender);
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            fwhMain.Navigate(new ProductsPage());
            btnSel.SelectButton((Button)sender);
        }
    }
}
