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
            Id = ID.GetId();
        }
        protected PrintedMatter(string name, int imprintDate)
        {
            Id = ID.GetId();
            Name = name;
            ImprintDate = imprintDate;
        }
      
        public int Id{get;set;}
        public string Name { get; set; }
        public int ImprintDate { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0}\nName: {1}\nImprint date: {2}\n",
                Id, Name, ImprintDate);
        }
    }
}
