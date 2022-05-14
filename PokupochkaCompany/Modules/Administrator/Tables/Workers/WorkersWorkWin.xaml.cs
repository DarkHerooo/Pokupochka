using DbLib;
using PokupochkaCompany.Classes;
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
            CbRole.ItemsSource = DbConnect.Db.Roles.ToList();

            if (_worker.Id == 0)
            {
                Title = "Добавить пользователя";
                BtnAddOrChange.Content = "Добавить пользователя";

                CbRole.SelectedIndex = 0;
            }
            else
            {
                Title = "Изменить пользователя";
                BtnAddOrChange.Content = "Изменить пользователя";

                CbRole.SelectedItem = _worker.User!.Role;
            }

            TbSecondName.Text = _worker!.SecondName;
            TbFirstName.Text = _worker.FirstName;
            TbPatronymic.Text = _worker.Patronymic;
            TbPhone.Text = _worker.Phone;
            TbEmail.Text = _worker.Email;
            TbLogin.Text = _worker.User!.Login;
            TbPassword.Text = _worker.User!.Password;
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
                if (user!.Id != _worker.User!.Id)
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
                _worker.User!.SetData(TbLogin.Text, TbPassword.Text);
                _worker.User!.Role = CbRole.SelectedItem as Role;
                _worker.User!.AddOrChange();

                _worker.SetData(TbSecondName.Text, TbFirstName.Text, TbPatronymic.Text, TbPhone.Text, TbEmail.Text);
                _worker.AddOrChange();

                DialogResult = true;
                Close();
            }
        }
    }
}
