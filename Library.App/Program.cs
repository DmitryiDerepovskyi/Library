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
            Console.ReadLine();
        }
    }
}
