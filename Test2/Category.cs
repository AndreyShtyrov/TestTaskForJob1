using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    public class Category
    {
        public readonly int idx;
        private string _name;
        public string Name
        {
            get { return _name.TrimEnd(); }
            set
            {
                _name = value;
                Controller.instance.ChangeCategory(this);
            }
        }

        public Category(int idx)
        {
            this.idx = idx;
            _name = "New Item" + idx.ToString();
        }

        public Category(int idx, string name)
        {
            _name = name;
            this.idx = idx;
        }
    }
}
