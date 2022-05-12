using PokupochkaCompany.Classes;
using PokupochkaCompany.Modules.Administrator.Reports;
using PokupochkaCompany.Modules.Administrator.Tables;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PokupochkaCompany.Modules.Administrator
{
    public partial class AdminMainPage : Page
    {
        private FrameWithHistory _fwhMain = null!;
        private ButtonSelector _btnSel = null!;

        public AdminMainPage()
        {
            InitializeComponent();

            TryApplySettings();
        }

        private void TryApplySettings()
        {
            if (_fwhMain == null)
            {
                _fwhMain = new FrameWithHistory(FrmMain);

                List<Button> buttons = new List<Button>();
                foreach (var item in GrButtons.Children)
                {
                    if (item is Button)
                        buttons.Add((Button)item);
                }

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                BtnTables_Click(BtnTables, new RoutedEventArgs());
            }
        }

        private void BtnTables_Click(object sender, RoutedEventArgs e)
        {
            _fwhMain.Navigate(new MainTablePage());
        }

        private void BtnDocs_Click(object sender, RoutedEventArgs e)
        {
            _fwhMain.Navigate(new testPage());
        }
    }
}
