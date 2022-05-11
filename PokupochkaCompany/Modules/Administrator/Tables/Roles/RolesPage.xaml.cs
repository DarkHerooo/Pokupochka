using DbLib;
using System.Linq;
using System.Windows.Controls;

namespace PokupochkaCompany.Modules.Administrator.Tables
{
    /// <summary>
    /// Логика взаимодействия для RolesPage.xaml
    /// </summary>
    public partial class RolesPage : Page
    {
        public RolesPage()
        {
            InitializeComponent();

            DgRoles.ItemsSource = DbConnect.Db.Roles.ToList();
        }
    }
}
