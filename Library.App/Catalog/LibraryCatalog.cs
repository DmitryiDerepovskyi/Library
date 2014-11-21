using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core;
using Library.App;
namespace Library.App.Catalog
{
    public class LibraryCatalog
    {
        public LibraryCatalog()
        {
            list = new MyListArray<PrintedMatter>();
            _fileNameCatalog = "Catalog.txt";
            operationCatalog = new Dictionary<string, FuncCatalog>
            {
                {"Book", this.AddBook},
                {"Magazine", this.AddMagazine},
                {"Change", this.ChangeItem},
                {"Search by id", this.SearchById },
                {"Remove", this.RemoveItem},
                {"Save", this.SaveList},
                {"Load",this.LoadList},
                {"Print",this.PrintList},
            };
        }
        // каталог
        MyListArray<PrintedMatter> list;
        // функции для работы с каталогом
        public Dictionary<string, FuncCatalog> operationCatalog;
        public delegate void FuncCatalog();
        // имя файла, где хранится каталог
        string _fileNameCatalog;

        /// <summary>
        /// Добавить новую книгу
        /// </summary>
        void AddBook()
        {
            Console.Write("Input name: ");
            string name = Console.ReadLine();
            Console.Write("Input imprint date: ");
            int imprintDate = 0;
            try
            {
                imprintDate = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Write("Input author: ");
            string author = Console.ReadLine();
            PrintedMatter newItem = new Book(name, imprintDate, author);
            list.Add(newItem);
        }
        /// <summary>
        /// Добавить новый журнал
        /// </summary>
        void AddMagazine()
        {
            Console.Write("Input name: ");
            string name = Console.ReadLine();
            Console.Write("Input imprint date: ");
            int imprintDate = 0, numberOfEdition = 0;
            try
            {
                imprintDate = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Write("Input number of edition: ");
            try
            {
                numberOfEdition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            PrintedMatter newItem = new Magazine(name, imprintDate, numberOfEdition);
            list.Add(newItem);
        }
        void ChangeItem()
        {
            Console.Write("Input number of element: ");
            int number = 0;
            try
            {
                number = Convert.ToInt32(Console.ReadLine()) - 1;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            PrintedMatter item = null;
            try
            {
                item = list.GetElement(number);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Element doesn't exist");
            }
            Console.WriteLine(item.ToString());
            Console.WriteLine("Change this element?(y/n)");
            char choose = Console.ReadKey(true).KeyChar;
            if (Char.ToLower(choose) == 'y')
            {
                list.Update(number, Change(item));
            }
            else
                return;
        }

        PrintedMatter Change(PrintedMatter item)
        {
            Console.WriteLine("Change name?(y/n)");
            char choose = Console.ReadKey(true).KeyChar;
            if (Char.ToLower(choose) == 'y')
            {
                item.name = InputName();
            }
            Console.WriteLine("Change imprint date?(y/n)");
            choose = Console.ReadKey(true).KeyChar;
            if (Char.ToLower(choose) == 'y')
            {
                item.imprintDate = InputImprintDate();
            }
            if (item is Magazine)
            {
                Console.WriteLine("Change number of edition?(y/n)");
                choose = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(choose) == 'y')
                {
                    ((Magazine)item).numberOfEdition = InputNumberOfEdition();
                }
            }
            if (item is Book)
            {
                Console.WriteLine("Change author?(y/n)");
                choose = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(choose) == 'y')
                {
                    ((Book)item).author = InputAuthor();
                }
            }
            return item;
        }
        string InputName()
        {
            Console.Write("Input name: ");
            return Console.ReadLine();
        }
        int InputImprintDate()
        {
            Console.Write("Input imprint date: ");
            int imprintDate = 0;
            try
            {
                imprintDate = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return imprintDate;
        }
        int InputNumberOfEdition()
        {
            Console.Write("Input number of edition: ");
            int numberOfEdition = 0;
            try
            {
                numberOfEdition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return numberOfEdition;
        }
        string InputAuthor()
        {
            Console.Write("Input author: ");
            return Console.ReadLine();
        }
        /// <summary>
        /// Удалить указанный элемент
        /// </summary>
        void RemoveItem()
        {
            Console.Write("Input number of element: ");
            int number = 0;
            try
            {
                number = Convert.ToInt32(Console.ReadLine()) - 1;
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Console.WriteLine(list.GetElement(number).ToString());
                Console.WriteLine("Delete this element?(y/n)");
                char choose = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(choose) == 'y')
                    list.Remove(number);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Сохранить каталог
        /// </summary>
        void SaveList()
        {
            try
            {
                ListSerialization.DoSerialization(list, _fileNameCatalog);
                Console.WriteLine("Catalog saved");
            }
            catch
            {
                Console.WriteLine("Can't save");
            }
        }

        /// <summary>
        /// Загрузить каталог
        /// </summary>
        void LoadList()
        {
            try
            {
                list = ListSerialization.DoDeserialization( _fileNameCatalog);
                Console.WriteLine("Catalog was loaded");
            }
            catch
            {
                Console.WriteLine("Can't load");
            }
        }

        /// <summary>
        /// Найти по ид
        /// </summary>
        void SearchById()
        {
            Console.Write("Input id of element: ");
            int id = 0;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            int index = list.IndexOf(id);
            if (index == -1)
                Console.WriteLine("Element of the id {0} does not exist", id);
            else
                Console.WriteLine(list.GetElement(index).ToString());
        }
        /// <summary>
        /// Вывести каталог
        /// </summary>
        void PrintList()
        {
            foreach (PrintedMatter item in list)
                Console.WriteLine(item.ToString());
        }
    }
}
