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
    /// Логика взаимодействия для ExpensesTableWindow.xaml
    /// </summary>
    public partial class ExpensesTableWindow : Window
    {
        public ExpensesTableWindow()
        {
            InitializeComponent();
            ExpensesTable.DataContext =  Controller.instance.Expenses;
        }

        private void NewExpense(object sender, RoutedEventArgs e)
        {
            var createNew = new NewSpExpense();
            createNew.ShowDialog();
        }

        private void DeleteExpense(object sender, RoutedEventArgs e)
        {
            if (ExpensesTable.SelectedIndex < 0) return;
            Controller.instance.DeleteExpense(ExpensesTable.SelectedIndex);
        }

        private void UpdateList(object sender, RoutedEventArgs e)
        {
            Controller.instance.LoadAllExpenses();
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            if (ExpensesTable.SelectedIndex < 0)
                return;

            var expense = Controller.instance.Expenses[ExpensesTable.SelectedIndex];
            var createNew = new NewSpExpense();
            createNew.choosenExpense = expense;
            createNew.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExpensesTable.DataContext = Controller.instance.Expenses;
            if(FirstDate.SelectedDate == null || SecondDate.SelectedDate == null)
                return;
            var t = Controller.instance.LoadExpensesByPeriod(FirstDate.SelectedDate.Value, SecondDate.SelectedDate.Value);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExpensesTable.DataContext = Controller.instance.Expenses;
            Controller.instance.LoadAllExpenses();
        }
    }
}
