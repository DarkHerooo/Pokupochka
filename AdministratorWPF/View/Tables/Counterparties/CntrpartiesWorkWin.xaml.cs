﻿using DbLib.DB;
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

            if (_counterparty.Company!.Image == null)
                _counterparty.Company.Image = ImageReader.GetDefaultBytes();

            Style = UserStyles.WindowSyle;
            DataContext = _counterparty;
        } 

        private bool CheckCompanyData()
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(TbCompany.Text) || string.IsNullOrWhiteSpace(TbCompany.Text))
                errorMessage += "Не введена компания\n";

            if (string.IsNullOrEmpty(TbAddress.Text) || string.IsNullOrWhiteSpace(TbAddress.Text))
                errorMessage += "Не введён адрес\n";

            string tbINN = TbINN.Text.Replace("_", "");
            if (tbINN.Length < TbINN.MaxLength)
                errorMessage += $"ИНН заполнен неполностью\n";

            string tbKPP = TbKPP.Text.Replace("_", "");
            if (tbKPP.Length < TbKPP.MaxLength)
                errorMessage += $"КПП заполнен неполностью\n";

            string tbOKPO = TbOKPO.Text.Replace("_", "");
            if (tbOKPO.Length < TbOKPO.MaxLength)
                errorMessage += $"ОГРН заполнен неполностью\n";

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else return true;
        }

        public bool CheckCntrData()
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(TbSecondName.Text) || string.IsNullOrWhiteSpace(TbSecondName.Text))
                errorMessage += "Не введена фамилия\n";

            if (string.IsNullOrEmpty(TbFirstName.Text) || string.IsNullOrWhiteSpace(TbFirstName.Text))
                errorMessage += "Не введено имя\n";

            string clearPhone = AdditionalFields.GetClearPhone(TbPhone.Text);
            if (string.IsNullOrEmpty(clearPhone) || string.IsNullOrWhiteSpace(clearPhone))
                errorMessage += "Не введен телефон\n";
            else if (clearPhone.Length < 11)
                errorMessage += "Введите телефон полностью\n";

            errorMessage += WPFChecks.CheckEmail(TbEmail.Text);

            errorMessage += WPFChecks.CheckLogin(TbLogin.Text);

            errorMessage += WPFChecks.CheckPassword(TbPassword.Text);

            if (_counterparty.Id == 0)
            {
                User? user = DbConnect.Db.Users.FirstOrDefault(u => u.Login == TbLogin.Text /*&& u.Id != CurrentUser.User.Id*/);
                if (user != null)
                    errorMessage += "Пользователь с таким логином уже существует\n";
            }

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else return true;
        }

        private void BtnAddOrChange_Click(object sender, RoutedEventArgs e)
        {
            if (CheckCompanyData() && CheckCntrData())
            {
                _counterparty.User!.AddOrChange();
                _counterparty.Company!.AddOrChange();
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
                _counterparty.Company!.Image = dialog.ImageBytes!;
                DataContext = null;
                DataContext = _counterparty;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TbFIO_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text[0]))
                e.Handled = true;
        }

        private void TbEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string email = TbEmail.Text;
            if (email.Contains('@') && e.Text[0] == '@')
                e.Handled = true;
        }
    }
}
