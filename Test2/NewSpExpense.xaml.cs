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
    /// Логика взаимодействия для NewSpExpense.xaml
    /// </summary>
    public partial class NewSpExpense : Window
    {
        public Expense choosenExpense = null;
        public NewSpExpense()
        {
            InitializeComponent();
            ChooseCategorial.DataContext = Controller.instance.Categories;
            Title = "CreateNewExpense";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cat = ChooseCategorial.SelectedValue;
            var date = DateChoosen.SelectedDate;
            if (cat == null || date == null)
                return;
            if (choosenExpense == null)
            {
                Controller.instance.CreateExpense(date.Value, cat.ToString(),
                double.Parse(Price.Text.ToString()), CommentText.Text.ToString());
            }
            this.Close();
        }

        private void enterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CalendarCombo.IsDropDownOpen = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (choosenExpense == null)
                return;
            Title = "ChangeExpense";
            ChooseCategorial.SelectedValue = choosenExpense.ExpenseType;
            DateChoosen.SelectedDate = choosenExpense.Date;
            CalendarCombo.SelectedItem = DateChoosen;
            Price.Text = choosenExpense.Price.ToString();
            CommentText.Text = choosenExpense.Comment.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
