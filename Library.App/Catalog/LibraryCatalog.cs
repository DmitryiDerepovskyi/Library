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
            _fileNameCatalog = "Catalog.txt";
            _fm = new FileManager();
            _operationCatalog = new Dictionary<string, Action>
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
            SaveAsync = this.Serealized;
        }
        public ISerializedAsync<PrintedMatter> serial;
        // каталог
        MyListArray<PrintedMatter> _list;
        // функции для работы с каталогом
        public Dictionary<string, Action> _operationCatalog;
        FileManager _fm;
        // имя файла, где хранится каталог
        string _fileNameCatalog;

        private delegate void SaveCatalog(ISerializedAsync<PrintedMatter> serialized);
        private event SaveCatalog SaveAsync;

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
            _list.AddAsync(newItem);
            SaveAsync(serial);
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
            _list.AddAsync(newItem);
            SaveAsync(serial);
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
                item = _list.GetElement(number);
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
                _list.Update(number, Change(item));
                SaveAsync(serial);
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
                item.Name = InputName();
            }
            Console.WriteLine("Change imprint date?(y/n)");
            choose = Console.ReadKey(true).KeyChar;
            if (Char.ToLower(choose) == 'y')
            {
                item.ImprintDate = InputImprintDate();
            }
            if (item is Magazine)
            {
                Console.WriteLine("Change number of edition?(y/n)");
                choose = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(choose) == 'y')
                {
                    ((Magazine)item).NumberOfEdition = InputNumberOfEdition();
                }
            }
            if (item is Book)
            {
                Console.WriteLine("Change author?(y/n)");
                choose = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(choose) == 'y')
                {
                    ((Book)item).Author = InputAuthor();
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
                return;
            }
            try
            {
                Console.WriteLine(_list.GetElement(number).ToString());
                Console.WriteLine("Delete this element?(y/n)");
                char choose = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(choose) == 'y')
                {
                    _list.RemoveAsync(number);
                    Console.WriteLine("Element deleted");
                    SaveAsync(serial);
                }
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
                Serealized(serial);
                Console.WriteLine("Catalog saved");
            }
            catch
            {
                Console.WriteLine("Can't save");
            }
        }
        void Serealized(ISerializedAsync<PrintedMatter> serialized)
        {
            serialized.SerializedAsync(_list, _fileNameCatalog);
        }
        /// <summary>
        /// Загрузить каталог
        /// </summary>
        void LoadList() 
        {
            _fileNameCatalog = _fm.GetCatalog();
            try
            {
                DeserializedCatalog(serial);
                Console.WriteLine("Catalog was loaded");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Can't load");
            }
        }

        void DeserializedCatalog(ISerializedAsync<PrintedMatter> deserialized)
        {
            _list = deserialized.Deserialized(_fileNameCatalog);
            int MaxId = _list.Max(item => item.Id);
            ID.SetId(MaxId);
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
            Task<int> taskIndex = _list.IndexOfAsync(id);
            if (taskIndex.Result == -1)
                Console.WriteLine("Element of the id {0} does not exist", id);
            else
                Console.WriteLine(_list.GetElement(taskIndex.Result).ToString());
        }
        /// <summary>
        /// Вывести каталог
        /// </summary>
        void PrintList()
        {
            foreach(PrintedMatter item in _list)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
