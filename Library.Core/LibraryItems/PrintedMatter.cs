using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Library.Core
{
    [Serializable]
    public abstract class PrintedMatter
    {
        protected PrintedMatter()
        {
            _id = _countId;
            _countId++;
        }
        protected PrintedMatter(string name, int imprintDate)
        {
            _id = _countId;
            _countId++;
            this._name = name;
            this._imprintDate = imprintDate;
        }
      
        static int _countId = 100000;
        protected int _id;
        protected string _name;
        protected int _imprintDate;
        public int countId
        {
            get
            {
                return _countId;
            }
        }
        public int id { 
            get 
            { 
                return _id; 
            }
            set
            {
                _id = value;
            }
        }
        public string name {
            get 
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int imprintDate
        {
            get
            {
                return _imprintDate;
            }
            set
            {
                _imprintDate = value;
            }
        }

        public override string ToString()
        {
            return String.Format("Id: {0}\nName: {1}\nImprint date: {2}\n",
                _id, _name, _imprintDate);
        }
    }
}
