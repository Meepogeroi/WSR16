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
    /// Логика взаимодействия для RegisterForEvent.xaml
    /// </summary>
    public partial class RegisterForEvent : Page
    {
        public void printPrice()
        {
            fullPrice.Text = $"${variant + vid}";
        }
        int variant = 0;
        int vid = 0;
        public RegisterForEvent()
        {
            InitializeComponent();
            fullPrice.Text = $"${variant + vid}";
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton checkedObj = (RadioButton)sender;
            if(checkedObj.Name == VarOne.Name)
            {
                variant = 0;
                printPrice();
            } else if(checkedObj.Name == VarTwo.Name)
            {
                variant = 20;
                printPrice();
            }
            else
            {
                variant = 45;
                printPrice();
            }
        }

        private void Add42_Unchecked(object sender, RoutedEventArgs e)
        {
            vid -= 145;
            printPrice();
        }

        private void Add42_Checked(object sender, RoutedEventArgs e)
        {
            vid += 145;
            printPrice();
        }

        private void Add21_Checked(object sender, RoutedEventArgs e)
        {
            vid += 75;
            printPrice();
        }

        private void Add21_Unchecked(object sender, RoutedEventArgs e)
        {
            vid -= 75;
            printPrice();
        }

        private void Add5_Checked(object sender, RoutedEventArgs e)
        {
            vid += 20;
            printPrice();
        }

        private void Add5_Unchecked(object sender, RoutedEventArgs e)
        {
            vid -= 20;
            printPrice();
        }
    }
}
