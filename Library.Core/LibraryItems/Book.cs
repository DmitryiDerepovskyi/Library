using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Library.Core
{
    [Serializable]
    public class Book : PrintedMatter
    {
        public Book():base()
        {
            _author = String.Empty;
        }

        public Book(string name, int imprintDate, string author)
            : base(name, imprintDate)
        {
            this._author = author;
        }

        private string _author;

        public string author
        {
            get 
            {
                return _author;
            }
            set 
            {
                _author = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format("Author: {0}\n", _author);
        }
    }
}
