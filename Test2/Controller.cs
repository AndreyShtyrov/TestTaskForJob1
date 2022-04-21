using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Test2
{
    internal class Controller
    {
        private string mainTable;

        private object myLock = new object();

        private SqlConnection sqlConnection = null;
        public ObservableCollection<Category> Categories
        { get; }

        public ObservableCollection<Expense>  Expenses
        { get; }

        private static Controller _instance = null;
        public static Controller instance
        { get { return _instance; } }

        public Controller(string MainTable)
        {

            this.mainTable = MainTable;
            _instance = this;
            Categories = new ObservableCollection<Category>();
            Expenses = new ObservableCollection<Expense>();
        }

        public void LoadDataBase()
        {
            sqlConnection = new SqlConnection(mainTable);
            sqlConnection.Open();
            UpdateCategories();
        }

        public void DeleteCategory(int idx)
        {
            var id = Categories[idx].idx;
            var command = new SqlCommand("DELETE FROM [ExpenseType] WHERE id = " + id.ToString(), sqlConnection);
            command.ExecuteNonQuery();
            Categories.RemoveAt(idx);
        }

        public void DeleteExpense(int idx)
        {
            var id = Expenses[idx].idx;
            var command = new SqlCommand("DELETE FROM [Expenses] WHERE id = " + id.ToString(), sqlConnection);
            command.ExecuteNonQuery();
            Expenses.RemoveAt(idx);
        }

        public void ChangeCategory(Category expense)
        {
            var command = new SqlCommand("UPDATE ExpenseType SET Name = @Name WHERE ID = " + expense.idx.ToString(),
                sqlConnection);
            command.Parameters.AddWithValue("Name", expense.Name);
            command.ExecuteNonQuery();
        }

        public void ChangeExpense(Expense expense)
        {
            var command = new SqlCommand("UPDATE Expenses SET Date = @Date, ExpenseType = @ExpenseType," +
                " Price = @Price, Comment = @Comment" +
                " WHERE ID = " + expense.idx.ToString(),
                sqlConnection);
            command.Parameters.AddWithValue("Date", expense.Date);
            command.Parameters.AddWithValue("ExpenseType", expense.ExpenseType);
            command.Parameters.AddWithValue("Price", (float)expense.Price);
            command.Parameters.AddWithValue("Comment", expense.Comment);
            command.ExecuteNonQuery();
        }

        public void CreateCategory(string name)
        {
            var command = new SqlCommand("INSERT INTO [ExpenseType] (Name) VALUES (@Name)", sqlConnection);
            command.Parameters.AddWithValue("Name", name);
            command.ExecuteNonQuery();
            UpdateCategories();
        }

        public void UpdateCategories()
        {
            var command = new SqlCommand("SELECT * FROM ExpenseType", sqlConnection);
            Categories.Clear();
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var expense = new Category((int)dataReader["Id"], (string)dataReader["Name"]);
                    Categories.Add(expense);
                }
            }
        }

        public void CreateExpense(DateTime date, string category, double price, string comment)
        {
            var command = new SqlCommand("INSERT INTO [Expenses] (Date, ExpenseType, Price, Comment) " +
                "VALUES (@Date, @ExpenseType, @Price, @Comment)", sqlConnection);
            command.Parameters.AddWithValue("Date", date);
            command.Parameters.AddWithValue("ExpenseType", category);
            command.Parameters.AddWithValue("Price", (float)price);
            command.Parameters.AddWithValue("Comment", comment);
            command.ExecuteNonQuery();
            var command2 = new SqlCommand("SELECT * FROM  Expenses WHERE Id = (SELECT MAX(Id) FROM Expenses)",
                sqlConnection);
            using (var dr = command2.ExecuteReader())
            {
                dr.Read();
                var _date = dr["Date"];
                if (_date is DateTime tdate)
                {
                    Expenses.Add(new Expense((int)dr["Id"], tdate, (string)dr["ExpenseType"],
                        (double)dr["Price"], (string)dr["Comment"]));
                }
            }
        }

        public async Task LoadExpensesByPeriod(DateTime fromDate, DateTime toDate)
        {
            var command = new SqlCommand("SELECT * FROM Expenses WHERE Date >= @FromDate AND @ToDate >= Date  ", 
                sqlConnection);
            command.Parameters.AddWithValue("FromDate", fromDate);
            command.Parameters.AddWithValue("ToDate", toDate);
            Expenses.Clear();
            List<Expense> tempList = new List<Expense>();
            using (var adr = command.ExecuteReaderAsync())
            {
                await adr;
                while (adr.Result.Read())
                {
                    var _date = adr.Result["Date"];
                    if (_date is DateTime tdate)
                    {
                        tempList.Add(new Expense((int)adr.Result["Id"], tdate, (string)adr.Result["ExpenseType"],
                            (double)adr.Result["Price"], (string)adr.Result["Comment"]));
                    }
                }
            }
            lock (myLock)
            {
                Expenses.Clear();
                foreach (var expense in tempList)
                {
                    Expenses.Add(expense);
                }
            }
        }

        public async Task LoadAllExpenses()
        {
            var command = new SqlCommand("SELECT * FROM Expenses", sqlConnection);
            Expenses.Clear();
            List<Expense> tempList = new List<Expense>();
            using (var adr = command.ExecuteReaderAsync())
            {
                await adr;
                while (adr.Result.Read())
                {
                    var _date = adr.Result["Date"];
                    if (_date is DateTime tdate)
                    {
                        tempList.Add(new Expense((int)adr.Result["Id"], tdate, (string)adr.Result["ExpenseType"],
                            (double)adr.Result["Price"], (string)adr.Result["Comment"]));
                    }
                }
            }
            lock (myLock)
            {
                Expenses.Clear();
                foreach (var expense in tempList)
                {
                    Expenses.Add(expense);
                }
            }

        }
    }


}
