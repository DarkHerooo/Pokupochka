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

namespace PokupochkaCompany.Modules.Storekeeper.AcceptRequests
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
