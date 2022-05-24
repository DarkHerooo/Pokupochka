using DbLib.DB;
using System.Linq;
using System.Windows.Controls;

namespace AdministratorWPF.View.Tables
{
    public partial class RolesPage : Page
    {
        public RolesPage()
        {
            InitializeComponent();

            DgRoles.ItemsSource = DbConnect.Db.Roles.ToList();
        }
    }
}
