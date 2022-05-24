using GeneralLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFStorekeeperLib.View.AcceptRequests
{
    /// <summary>
    /// Логика взаимодействия для AccRequestsPage.xaml
    /// </summary>
    public partial class AccRequestsPage : Page
    {
        private FrameWithHistory fwhMain = null!;
        private ButtonSelector btnSel = null!;

        public AccRequestsPage()
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
                foreach (var item in SpButtons.Children)
                {
                    if (item is Button)
                        buttons.Add((Button)item);
                }

                btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnCliReq_Click(BtnCliReq, new RoutedEventArgs());
            }
        }

        private void BtnCliReq_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSuppReq_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
