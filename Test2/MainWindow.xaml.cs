using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Microsoft.Win32;

namespace Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoadDataBase()
        {
            var openfile = new ChooseDataBase();
            openfile.Title = "Input Path to DataBase";
            openfile.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var expanseWindow = new CategoryTableWindow();
            expanseWindow.Show();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            LoadDataBase();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var expensesWindow = new ExpensesTableWindow();
            expensesWindow.Show();
        }
    }
}
