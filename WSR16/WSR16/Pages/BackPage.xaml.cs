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

namespace WSR16.Pages
{
    /// <summary>
    /// Логика взаимодействия для BackPage.xaml
    /// </summary>
    public partial class BackPage : Page
    {
        public BackPage()
        {
            InitializeComponent();
            ForOtherPage.Navigate(new ChartsTestPrac());
            Manager.MainFrame = ForOtherPage;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ForOtherPage.GoBack();
        }

        private void ForOtherPage_ContentRendered(object sender, EventArgs e)
        {
            if (!ForOtherPage.CanGoBack)
            {
                BackButton.Visibility = Visibility.Hidden;
            }
            else
            {
                BackButton.Visibility = Visibility.Visible;
            };
        }
    }
}
