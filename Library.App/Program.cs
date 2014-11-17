using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core;
using Library.App.Catalog;
using Library.App.Menu;

namespace Library.App
{
    class Program
    {
        static void Main(string[] args)
        {
            InteractiveConsole console = new InteractiveConsole();
            console.Process();
            //int l = Console.CursorLeft;
            //int t = Console.CursorTop;
            //Console.ReadLine();
            //int width = Console.WindowWidth;
            //int height = Console.WindowHeight;
            //Console.CursorVisible = true;
            //Console.SetCursorPosition(width / 20, height / 20);
            //MyListArray<PrintedMatter> list = new MyListArray<PrintedMatter>();
            //for (int i = 0; i < 5; i++)
            //{
            //    PrintedMatter item;
            //    if (i % 2 == 0)
            //        item = new Book("Book" + i, 1970 + i, "Unknown" + i);
            //    else
            //        item = new Magazine("Magazine" + i, 1970 + i, i);
            //    list.Add(item);
            //}
            //PrintList(list);
            //ListSerialization.DoSerialization(list, "Test.txt");
            //list.Clear();
            //list = ListSerialization.DoDeserialization("Test.txt");
            //PrintList(list);
            //    //  Console.SetCursorPosition(10, 10);
            Console.ReadLine();
        }
    }
}
