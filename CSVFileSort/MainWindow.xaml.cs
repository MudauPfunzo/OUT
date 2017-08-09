using CSVFileSort.FileProcessing;
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

namespace CSVFileSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileProcessor fp = new FileProcessor(@"resources\file\data.csv", "", "");
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void orderFilByFrequency()
        {
         
            fp.SortFileByFrequency();
            
        }
        private void OrderByStreetName()
        {
            fp.SortFileByStreetNameAsc();
        }
        private void EnableButtons()
        {
            cmdOpenFile1.IsEnabled = true;
            cmdOpenFile2.IsEnabled = true;

        }

        private void cmdCreateFiles_Click(object sender, RoutedEventArgs e)
        {
            orderFilByFrequency();
            OrderByStreetName();
            EnableButtons();

        }

        private void cmdOpenFile1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileInNotePad("OrderedByFrequency.txt");
        }

        private void cmdOpenFile2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileInNotePad("OrderedByStreetName.txt");
        }

        private void OpenFileInNotePad(string fileName)
        {
            string ProjectPath = System.IO.Path.GetDirectoryName(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString());
            string PathToFile = @"resources\file\" + fileName;
            string fullPath = System.IO.Path.Combine(ProjectPath, PathToFile);
            System.Diagnostics.Process.Start("notepad.exe", fullPath);
        }
    }
}
