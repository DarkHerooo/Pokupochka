using DbLib.DB;
using DbLib.DB.Enums;
using GeneralLib;
using StylesLib;
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
using WPFAgentLib.View.Requests.Client;
using WPFAgentLib.View.Requests.Supplier;

namespace WPFAgentLib.View.Requests
{
    /// <summary>
    /// Логика взаимодействия для RequestsMainPage.xaml
    /// </summary>
    public partial class RequestsMainPage : Page
    {
        private FrameWithHistory _fwhTables = null!;
        private ButtonSelector _btnSel = null!;
        public RequestsMainPage()
        {
            InitializeComponent();

            TryApplySettings();
        }

        private void TryApplySettings()
        {
            if (_fwhTables == null)
            {
                _fwhTables = new FrameWithHistory(FrmMain);

                List<Button> buttons = new List<Button>();
                foreach (var item in SpButtons.Children)
                {
                    if (item is Button)
                        buttons.Add((Button)item);
                }

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnSuppliers_Click(null!, null!);
            }
        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            _fwhTables.Navigate(new SupRequestsPage());
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            _fwhTables.Navigate(new CliRequestsPage());
        }
    }
}
