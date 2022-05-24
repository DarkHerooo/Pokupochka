using AdministratorWPF.View.Tables;
using GeneralLib;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AdministratorWPF.View
{
    public partial class AdminMainPage : Page
    {
        private FrameWithHistory _fwhMain = null!;
        private ButtonSelector _btnSel = null!;

        public AdminMainPage()
        {
            InitializeComponent();
            SetSettings();
        }

        /// <summary>
        /// Устанавливает необходимые настройки для страницы
        /// </summary>
        private void SetSettings()
        {
            if (_fwhMain == null)
            {
                Style = UserStyles.WindowSyle;

                List<Button> buttons = new();
                foreach (var item in GrButtons.Children)
                    if (item is Button) buttons.Add((Button)item);

                _btnSel = new(buttons.ToArray(), UserStyles.DefaultButtonStyle, UserStyles.SelectButtonStyle);
                _fwhMain = new FrameWithHistory(FrmMain);
                BtnTables_Click(null!, null!);
            }
        }

        private void BtnTables_Click(object sender, RoutedEventArgs e)
        {
            _fwhMain.Navigate(new MainTablePage());
        }

        private void BtnDocs_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
