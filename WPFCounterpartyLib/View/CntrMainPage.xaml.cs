using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using GeneralLib.Usr;
using StylesLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFClientLib.View.Requests;
using WPFCounterpartyLib.View.Contracts;
using WPFSupplierLib.View.Requests;

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
            Role role = CurrentUser.User.Role!;

            switch(role.Id)
            {
                case (int)RoleKey.Supplier: _fwhMain.Navigate(new SupRequestsPage()); break;
                case (int)RoleKey.Client: _fwhMain.Navigate(new CliRequestsPage()); break;
            }
        }
    }
}
