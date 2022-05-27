using GeneralLib;
using StylesLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFStorekeeperLib.View.AcceptRequests;
using WPFStorekeeperLib.View.Products;

namespace WPFStorekeeperLib.View
{
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
