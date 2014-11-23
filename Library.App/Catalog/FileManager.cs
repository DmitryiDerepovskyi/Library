using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Library.App.Catalog
{
    class FileManager
    {
        public string GetCatalog()
        {
            string nameCatalog = string.Empty;
            string path = Directory.GetCurrentDirectory();
            string[] filePaths = Directory.GetFiles(path, "*.txt",
                                         SearchOption.TopDirectoryOnly);
            int choose = 0;
            while (choose > filePaths.Length || choose < 1)
            {
                Console.Clear();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    string str = filePaths[i].Substring(path.Length + 1, filePaths[i].Length - path.Length - 1);
                    Console.WriteLine("{0}.{1}", i + 1, str);
                }
                Console.WriteLine("Choose catalog");
                Int32.TryParse(Console.ReadLine(), out choose);
            }
            nameCatalog = filePaths[Math.Max(choose-1, 0)];
            return nameCatalog;
        }
        private string CreateNewCatalog()
        {
            Console.Clear();
            string nameCatalog = String.Empty;
            while (nameCatalog == String.Empty)
            {
                Console.Write("Input a name new catalog: ");
                nameCatalog = Console.ReadLine();
            }
            return nameCatalog + ".txt";
        }
    }
}
