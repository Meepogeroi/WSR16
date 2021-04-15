﻿using System;
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
using System.IO.Packaging;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

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
            dataGridInfo.ItemsSource = db.Country.ToList();
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
                            if (dateElement.DateAndTime == element.DateAndTime)
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
            printing.PageSetup();
            printing.PrintPreview();
            printing.Print(true);

            var asd = printing.PrintDocument;
        }

        public Byte[] ImageToByte(BitmapImage imageSource)
        {
            Stream stream = imageSource.StreamSource;
            Byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }

        private void SaveChartButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();
            dlg.PageRangeSelection = PageRangeSelection.AllPages;
            dlg.UserPageRangeEnabled = false;
            if (dlg.ShowDialog() == true)
            {
                var doc = new FlowDocument();

                doc.ColumnWidth = dlg.PrintableAreaWidth;
                doc.Blocks.Add(new Paragraph(new Run("Some try print chart and info")));
                BitmapImage bimg = new BitmapImage();
                Image image = new Image();
                bimg.BeginInit();
                bimg.UriSource = new Uri(@"C:\Users\User\Desktop\ChartImage.png", UriKind.RelativeOrAbsolute);
                bimg.EndInit();
                image.Source = bimg;
                doc.Blocks.Add(new BlockUIContainer(image));
                var some = dataGridInfo;
                doc.Blocks.Add(new BlockUIContainer(dataGridInfo));
                dlg.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "Simple");
            }
        }
    }
}

/*
var dlg = new PrintDialog();
dlg.PageRangeSelection = PageRangeSelection.AllPages;
dlg.UserPageRangeEnabled = false;
if (dlg.ShowDialog() == true)
{
    var doc = new FlowDocument();

    doc.ColumnWidth = dlg.PrintableAreaWidth;
    doc.Blocks.Add(new Paragraph(new Run("Some try print chart and info")));
    BitmapImage bimg = new BitmapImage();
    Image image = new Image();
    bimg.BeginInit();
    bimg.UriSource = new Uri(@"C:\Users\User\Desktop\ChartImage.png", UriKind.RelativeOrAbsolute);
    bimg.EndInit();
    image.Source = bimg;
    doc.Blocks.Add(new BlockUIContainer(image));
    dlg.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "Simple");
}*/

/*
 var application = new Word.Application();

            Word.Document document = application.Documents.Add();
            Word.Range range;
            //range = 
            //Word.InlineShape imageShape = range.InlineShapes.AddPicture();
 */

/*
 FileStream stream = new FileStream($@"C:\Users\User\Desktop\ChartImage.png", FileMode.Create);
            ChartsTestVar.SaveImage(stream, ChartImageFormat.Png);
            stream.Close();
            var doc = new FlowDocument();
            doc.Blocks.Add(new Paragraph(new Run("Some Text")));
            Image image = new Image();
            BitmapImage bimg = new BitmapImage();
            bimg.BeginInit();
            bimg.UriSource = new Uri(@"C:\Users\User\Desktop\ChartImage.png", UriKind.RelativeOrAbsolute);
            bimg.EndInit();
            image.Source = bimg;
            doc.Blocks.Add(new BlockUIContainer(image));
            FileStream fs = new FileStream(@"C:\Users\User\Desktop\ChartImage.pdf", FileMode.OpenOrCreate, FileAccess.Write);
            var dlg = new SaveFileDialog();
            dlg.Filter = "Text files(*.pdf)|*.pdf|All files(*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                //File.WriteAllBytes(filename, ImageToByte(bimg));
                MessageBox.Show("Файл сохранен");*/
