using DbLib.DB;
using DbLib.DB.Entity;
using DbLib.DB.Enums;
using GeneralLib;
using StylesLib;
using System.Linq;
using System.Windows;

namespace AdministratorWPF.View.Tables
{
    public partial class CntrpartiesWorkWin : Window
    {
        private Counterparty _counterparty;
        public CntrpartiesWorkWin(Counterparty counterparty)
        {
            InitializeComponent();

            _counterparty = counterparty;
            SetWinSettings();
        }

        private void SetWinSettings()
        {
            if (_counterparty.User!.RoleId == (int)RoleKey.Client)
            {
                TbContragentData.Text = "Данные о клиенте";
                if (_counterparty.Id == 0)
                {
                    Title = "Добавить клиента";
                    BtnAddOrChange.Content = "Добавить клиента";
                }
                else
                {
                    Title = "Изменить клиента";
                    BtnAddOrChange.Content = "Изменить клиента";
                }
            }
            else if (_counterparty.User!.RoleId == (int)RoleKey.Supplier)
            {
                TbContragentData.Text = "Данные о поставщике";
                if (_counterparty.Id == 0)
                {
                    Title = "Добавить поставщика";
                    BtnAddOrChange.Content = "Добавить поставщика";
                }
                else
                {
                    Title = "Изменить поставщика";
                    BtnAddOrChange.Content = "Изменить поставщика";
                }
            }

            if (_counterparty.Image == null)
                _counterparty.Image = ImageReader.GetDefaultBytes();

            Style = UserStyles.WindowSyle;
            DataContext = _counterparty;
        } 

        private bool CheckData()
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(TbCompany.Text) || string.IsNullOrWhiteSpace(TbCompany.Text))
                errorMessage += "Не введена компания\n";

            if (string.IsNullOrEmpty(TbAddress.Text) || string.IsNullOrWhiteSpace(TbAddress.Text))
                errorMessage += "Не введён адрес\n";

            if (string.IsNullOrEmpty(TbSecondName.Text) || string.IsNullOrWhiteSpace(TbSecondName.Text))
                errorMessage += "Не введена фамилия\n";

            if (string.IsNullOrEmpty(TbFirstName.Text) || string.IsNullOrWhiteSpace(TbFirstName.Text))
                errorMessage += "Не введено имя\n";

            string clearPhone = AdditionalFields.GetClearPhone(TbPhone.Text);
            if (string.IsNullOrEmpty(clearPhone) || string.IsNullOrWhiteSpace(clearPhone))
                errorMessage += "Не введен телефон\n";

            if (string.IsNullOrEmpty(TbEmail.Text) || string.IsNullOrWhiteSpace(TbEmail.Text))
                errorMessage += "Не введена почта\n";

            if (string.IsNullOrEmpty(TbLogin.Text) || string.IsNullOrWhiteSpace(TbLogin.Text))
                errorMessage += "Не введен логин\n";
            else
            {
                User? user = DbConnect.Db.Users.FirstOrDefault(u => u.Login == TbLogin.Text);
                if (user != null && user != _counterparty.User)
                    errorMessage += "Пользователь с таким логином уже существует\n";
            }

            if (string.IsNullOrEmpty(TbPassword.Text) || string.IsNullOrWhiteSpace(TbPassword.Text))
                errorMessage += "Не введен пароль\n";

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else return true;
        }

        private void BtnAddOrChange_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                _counterparty.User!.AddOrChange();
                _counterparty.AddOrChange();

                DialogResult = true;
                Close();
            }
        }

        private void BrdImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ImageDialog dialog = new();

            if (dialog.Open())
            {
                _counterparty.Image = dialog.ImageBytes!;
                DataContext = null;
                DataContext = _counterparty;
            }
        }
    }
}
