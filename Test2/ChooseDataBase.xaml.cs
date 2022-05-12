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
using System.Windows.Shapes;

namespace Test2
{
    /// <summary>
    /// Логика взаимодействия для ChooseDataBase.xaml
    /// </summary>
    public partial class ChooseDataBase : Window
    {
        public ChooseDataBase()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string p1 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=";
            p1 += FileName.Text.ToString();
            p1 += ";Integrated Security = True";
            var sqlConnection = new SqlConnection(p1);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
                return;
            }
            var controller = new Controller(p1);
            controller.LoadDataBase();
            string path1 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Andronet\\source\\repos\\Test2\\Test2\\TestDB.mdf;Integrated Security=True";
            string path2 = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\WorkPlace\\source\\repos\\Test2\\Test2\\TestDB.mdf;Integrated Security=True";

            Close();
        }
    }
}
