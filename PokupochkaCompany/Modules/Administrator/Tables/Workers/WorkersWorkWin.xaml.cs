using DbLib;
using GeneralLib;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PokupochkaCompany.Modules.Administrator.Tables
{
    /// <summary>
    /// Логика взаимодействия для AddOrChangeUser.xaml
    /// </summary>
    public partial class WorkersWorkWin : Window
    {
        private Worker _worker;
        public WorkersWorkWin(Worker worker)
        {
            InitializeComponent();

            _worker = worker;
            Style = UserStyles.WindowBackground;
            SetData();
        }

        private void SetData()
        {
            List<Role> roles = DbConnect.Db.Roles.Where
                (r => r.Id == (int)RoleKey.Administratior ||
                r.Id == (int)RoleKey.Storekeeper ||
                r.Id == (int)RoleKey.Agent).ToList();
            CbRole.ItemsSource = roles;

            if (_worker.Id == 0)
            {
                Title = "Добавить пользователя";
                BtnAddOrChange.Content = "Добавить пользователя";
                _worker.User!.Role = roles.First();
            }
            else
            {
                Title = "Изменить пользователя";
                BtnAddOrChange.Content = "Изменить пользователя";
            }

            DataContext = _worker;
        }

        private bool CheckData()
        {
            string errorMessage = "";
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
                if (user != null && user != _worker.User)
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
            int index = CbRole.SelectedIndex;
            if (CheckData())
            {
                _worker.User!.AddOrChange();
                _worker.AddOrChange();

                DialogResult = true;
                Close();
            }
        }
    }
}
