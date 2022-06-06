using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using StylesLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPFAdministratorLib.View.Reports;

namespace AdministratorWPF.View.Reports
{
    public partial class MainReportsPage : Page
    {
        private FrameWithHistory _fwhTables = null!;
        private ButtonSelector _btnSel = null!;

        public MainReportsPage()
        {
            InitializeComponent();

            SetSettings();
        }

        /// <summary>
        /// Устанавливает необходимые настройки для страницы
        /// </summary>
        private void SetSettings()
        {
            if (_fwhTables == null)
            {
                List<Button> buttons = new();
                foreach (var item in SpButtons.Children)
                    if (item is Button) buttons.Add((Button)item);

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                _fwhTables = new FrameWithHistory(FrmMain);
                BtnMoney_Click(null!, null!);
            }
        }

        private void BtnMoney_Click(object sender, RoutedEventArgs e)
        {
            _fwhTables.Navigate(new MoneyPage());
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgents_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
