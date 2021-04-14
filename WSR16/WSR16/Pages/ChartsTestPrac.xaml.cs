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
using System.IO;

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
            ComboUsers.ItemsSource = db.Blood.ToList();
            ComboChartTypes.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void ComboUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboUsers.SelectedItem is Blood currentEvent &&
                ComboChartTypes.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartsTestVar.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();
                
                var categoriesList = db.Blood.ToList();
                SortedDictionary<DateTime, double> dateDict = new SortedDictionary<DateTime, double> { };
                foreach (var element in categoriesList)
                {
                    if (!dateDict.ContainsKey(element.DateAndTime.Value))
                    {
                        double schet = 0;
                        double sum = 0;
                        foreach (var dateElement in categoriesList)
                        {
                            if(dateElement.DateAndTime == element.DateAndTime)
                            {
                                sum += dateElement.Barcode;
                                schet++;
                            }
                        }
                        dateDict.Add(element.DateAndTime.Value, sum / schet);
                    }
                }
                foreach (var category in dateDict)
                {
                    currentSeries.Points.AddXY(category.Key,
                        category.Value);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*foreach (Blood el in db.Blood.ToList()) {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(el.tmpDate).ToLocalTime();
                el.DateAndTime = dtDateTime;
            }
            db.SaveChanges();*/
        }

        private void printChartButton_Click(object sender, RoutedEventArgs e)
        {
            PrintingManager printing = ChartsTestVar.Printing;
            printing.PrintPreview();
            printing.Print(true);

            var asd = printing.PrintDocument;
        }

        private void SaveChartButton_Click(object sender, RoutedEventArgs e)
        {
            FileStream stream = new FileStream($@"C:\Users\User\Desktop\ChartImage", FileMode.Create);
            ChartsTestVar.SaveImage(stream, ChartImageFormat.Png);
        }
    }
}
