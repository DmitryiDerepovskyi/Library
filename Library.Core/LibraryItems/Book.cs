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
            Author = String.Empty;
        }

        public Book(string name, int imprintDate, string author)
            : base(name, imprintDate)
        {
            Author = author;
        }

        public string Author { get; set; }

        public override string ToString()
        {
            return base.ToString() + String.Format("Author: {0}\n", Author);
        }
    }
}
