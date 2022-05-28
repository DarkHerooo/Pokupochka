using GeneralLib;
using StylesLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFCounterpartyLib.View.Contracts;

namespace WPFCounterpartyLib.View
{
    public partial class CntrMainPage : Page
    {
        private FrameWithHistory _fwhMain = null!;
        private ButtonSelector _btnSel = null!;

        public CntrMainPage()
        {
            InitializeComponent();
            SetSettings();
        }

        private void SetSettings()
        {
            if (_fwhMain == null)
            {
                List<Button> buttons = new();
                foreach (var item in GrButtons.Children)
                    if (item is Button) buttons.Add((Button)item);

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                _fwhMain = new FrameWithHistory(FrmMain);
                BtnContracts_Click(null!, null!);
            }
        }

        private void BtnContracts_Click(object sender, RoutedEventArgs e)
        {
            _fwhMain.Navigate(new CntrContractsPage());
        }

        private void BtnRequests_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
