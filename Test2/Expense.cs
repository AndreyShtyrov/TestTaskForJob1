using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    public class Expense
    {
        public int idx
        { get; }

        private DateTime _date;
        public DateTime Date
            { get { return _date; }
            set { 
                _date = value;
                OnChange();
            } }

        private string _expenseType;
        public string ExpenseType
        { get { return _expenseType.TrimEnd(); }
            set
            { _expenseType = value;
                OnChange();
            } }

        private double _price;
        public double Price
        { get { return _price; }
            set { 
                _price = (double)value;
                OnChange();
            }
        }

        public string _comment;
        public string Comment
        { get { return _comment; } 
            set
            {
                _comment = value;
                OnChange();
            }
        }

        public Expense(int idx, DateTime date, string expenseType, double price, string comment)
        {
            this.idx = idx;
            _date = date;
            _expenseType = expenseType;
            _price = price;
            _comment = comment;
        }

        private void OnChange()
        {
            Controller.instance.ChangeExpense(this);
        }
    }
}
