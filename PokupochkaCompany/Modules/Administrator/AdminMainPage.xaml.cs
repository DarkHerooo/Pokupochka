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
        private FrameWithHistory fwhMain = null!;
        private ButtonSelector btnSel = null!;

        public AdminMainPage()
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
                BtnTables_Click(BtnTables, new RoutedEventArgs());
            }
        }

        private void BtnTables_Click(object sender, RoutedEventArgs e)
        {
            fwhMain.Navigate(new MainTablePage());
            btnSel.SelectButton((Button)sender);
        }

        private void BtnDocs_Click(object sender, RoutedEventArgs e)
        {
            fwhMain.Navigate(new testPage());
            btnSel.SelectButton((Button)sender);
        }
    }
}
