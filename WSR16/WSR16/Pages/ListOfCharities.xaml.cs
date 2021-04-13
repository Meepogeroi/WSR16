using System;
using System.Collections.Generic;
using System.IO;
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

namespace WSR16.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfCharities.xaml
    /// </summary>
    public partial class ListOfCharities : Page
    {
        DB db;
        public ListOfCharities()
        {
            InitializeComponent();
            db = new DB();
            charityOrgList.ItemsSource = db.Charity.ToList();
            
        }
    }
}
