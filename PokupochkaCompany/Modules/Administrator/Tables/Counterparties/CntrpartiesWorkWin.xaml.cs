using DbLib;
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
using System.Windows.Shapes;

namespace PokupochkaCompany.Modules.Administrator.Tables
{
    /// <summary>
    /// Логика взаимодействия для AddClientWin.xaml
    /// </summary>
    public partial class CntrpartiesWorkWin : Window
    {
        private Counterparty _counterparty;
        public CntrpartiesWorkWin(Counterparty counterparty)
        {
            InitializeComponent();

            Style = UserStyles.WindowBackground;
            _counterparty = counterparty;
            SetData();
        }

        private void SetData()
        {
            if (_counterparty.User!.Role!.Id == (int)RoleKey.Client)
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
            else if (_counterparty.User!.Role!.Id == (int)RoleKey.Supplier)
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

            TbCompany.Text = _counterparty.Company;
            TbAddress.Text = _counterparty.Address;
            TbSecondName.Text = _counterparty!.SecondName;
            TbFirstName.Text = _counterparty.FirstName;
            TbPatronymic.Text = _counterparty.Patronymic;
            TbPhone.Text = _counterparty.Phone;
            TbEmail.Text = _counterparty.Email;
            TbLogin.Text = _counterparty.User!.Login;
            TbPassword.Text = _counterparty.User!.Password;
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
                _counterparty.User!.SetData(TbLogin.Text, TbPassword.Text);
                _counterparty.User!.AddOrChange();

                _counterparty.SetData(TbCompany.Text, TbAddress.Text, TbSecondName.Text, TbFirstName.Text, TbPatronymic.Text, TbPhone.Text, TbEmail.Text);
                _counterparty.AddOrChange();

                DialogResult = true;
                Close();
            }
        }
    }
}
