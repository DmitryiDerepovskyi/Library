﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Library.Core;
using System.Reflection;

namespace Library.App.Catalog
{
    class ListSerialization : ISerializedAsync<PrintedMatter>
    {
        /// <summary>
        /// Сериализация коллекции в заданный файл
        /// </summary>
        /// <param name="items"></param>
        /// <param name="path"></param>
        public async Task SerializedAsync(MyListArray<PrintedMatter> items, string path)
        {
            await Task.Run(() => Serial(items, path));
        }
        void Serial(MyListArray<PrintedMatter> items, string path)
        {
            XmlSerializer xmlS;
            using (FileStream fs = File.Create(path))
            {
                Type type = typeof(PrintedMatter);
                var types = Assembly.GetAssembly(type).GetTypes().Where(w => w.IsSubclassOf(type)).ToArray();
                xmlS = new XmlSerializer(typeof(MyListArray<PrintedMatter>), types);
                xmlS.Serialize(fs, items);
            }
        }
        /// <summary>
        /// Десериализация коллекции из заданного файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public MyListArray<PrintedMatter> Deserialized(string path)
        {
            XmlSerializer xmlS;
            MyListArray<PrintedMatter> items;
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                Type type = typeof(PrintedMatter);
                var types = Assembly.GetAssembly(type).GetTypes().Where(w => w.IsSubclassOf(type)).ToArray();
                xmlS = new XmlSerializer(typeof(MyListArray<PrintedMatter>), types);
                items = xmlS.Deserialize(fs) as MyListArray<PrintedMatter>;
            }
            return items;
        }
    }
}