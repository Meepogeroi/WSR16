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
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace WSR16.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChartsTestPrac.xaml
    /// </summary>
    public partial class ChartsTestPrac : Page
    {
        DB db;
        public ChartsTestPrac()
        {
            InitializeComponent();
            ChartsTestVar.ChartAreas.Add(new ChartArea("Main"));
            db = new DB();
            var currentSome = new Series("Payments")
            {
                IsValueShownAsLabel = true
            };
            ChartsTestVar.Series.Add(currentSome);
            ComboUsers.ItemsSource = db.Registration.ToList();
            ComboChartTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void ComboUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboUsers.SelectedItem is Registration currentReg &&
                ComboChartTypes.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartsTestVar.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var categoriesList = db.Registration.ToList();
                foreach (var category in categoriesList) 
                {
                    currentSeries.Points.AddXY(category.Runner,
                        db.Registration.ToList().Where(p=> p.Runner == currentReg.Runner
                        && p.Cost == category.Cost).Sum(p=> p.Cost * p.CharityId));
                }
            }
        }
    }
}
