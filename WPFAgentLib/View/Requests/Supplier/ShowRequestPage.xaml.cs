using DbLib.DB.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFAgentLib.View.Requests.Supplier
{
    /// <summary>
    /// Логика взаимодействия для ShowRequestPage.xaml
    /// </summary>
    public partial class ShowRequestPage : Page
    {
        private Request _request = null!;
        public ShowRequestPage(Request request)
        {
            _request = request;
            InitializeComponent();
        }
    }
}
