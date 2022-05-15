using PokupochkaCompany.Classes;
using PokupochkaCompany.Modules.Agent.Contracts;
using PokupochkaCompany.Modules.Agent.Requests;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PokupochkaCompany.Modules.Agent
{
    public partial class AgentMainPage : Page
    {
        private FrameWithHistory _fwhMain = null!;
        private ButtonSelector _btnSel = null!;
        public AgentMainPage()
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
                BtnContracts_Click(null!, null!);
            }
        }

        private void BtnContracts_Click(object sender, RoutedEventArgs e)
        {
            _fwhMain.Navigate(new ContractsMainPage());
        }

        private void BtnRequests_Click(object sender, RoutedEventArgs e)
        {
            _fwhMain.Navigate(new RequestsMainPage());
        }
    }
}
