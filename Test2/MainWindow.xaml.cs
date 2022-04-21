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
            string path1 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Andronet\\source\\repos\\Test2\\Test2\\TestDB.mdf;Integrated Security=True";
            string path2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\WorkPlace\\source\\repos\\Test2\\Test2\\TestDB.mdf;Integrated Security=True";
            var controller = new Controller(path1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var expanseWindow = new CategoryTableWindow();
            expanseWindow.Show();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Controller.instance?.LoadDataBase();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var expensesWindow = new ExpensesTableWindow();
            expensesWindow.Show();
        }
    }
}
