using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core
{
    [Serializable]
    public class Magazine : PrintedMatter
    {
        public Magazine():base()
        {
            NumberOfEdition = 0;
        }

        public Magazine(string name, int imprintDate, int numberOfEdition)
            : base(name, imprintDate)
        {
            NumberOfEdition = numberOfEdition;
        }
    

        public int NumberOfEdition { get; set; }

        public override string ToString()
        {
            return base.ToString() + String.Format("Number of edition: {0}\n",NumberOfEdition);
        }
    }
}
