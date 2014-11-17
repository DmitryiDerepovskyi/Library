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
            _numberOfEdition = 0;
        }

        public Magazine(string name, int imprintDate, int numberOfEdition)
            : base(name, imprintDate)
        {
            this._numberOfEdition = numberOfEdition;
        }
    
        private int _numberOfEdition;

        public int numberOfEdition
        {
            get 
            {
                return _numberOfEdition;
            }
            set
            {
                _numberOfEdition = value;
            }
         }

        public override string ToString()
        {
            return base.ToString() + String.Format("Number of edition: {0}\n",_numberOfEdition);
        }
    }
}
