using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Library.Core
{
    public static class ID
    {
        static int _id;
        static string _key = "CountId";
        static string _section = "appSettings";
        public static int GetId()
        {
            Int32.TryParse(ConfigurationManager.AppSettings[_key],out _id);
            _id =  _id >= 100000 ? _id : 100000;
            IncId();
            return _id;
        }
        static void IncId()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[_key].Value = (_id+1).ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_section);  
        }
        public static void SetId(int id)
        {
            _id = id;
            IncId();
        }
    }
}
