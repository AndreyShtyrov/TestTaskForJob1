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
using System.Windows.Shapes;

namespace Test2
{
    /// <summary>
    /// Логика взаимодействия для ColumnsWindow.xaml
    /// </summary>
    public partial class CategoryTableWindow : Window
    {

        public CategoryTableWindow()
        {
            InitializeComponent();
        }

        private void NewExpense(object sender, RoutedEventArgs e)
        {
            var creWindow = new CreateNewCategory();
            creWindow.ShowDialog();
        }

        private void DeleteExpense(object sender, RoutedEventArgs e)
        {
            Controller.instance.DeleteCategory(ExpensesTable.SelectedIndex);
        }

        private void UpdateList(object sender, RoutedEventArgs e)
        {
            Controller.instance.UpdateCategories();
            ExpensesTable.DataContext = Controller.instance.Categories;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExpensesTable.DataContext = Controller.instance.Categories;
        }

    }
    
}
