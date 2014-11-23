using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core;
using Library.App;
using System.Text.RegularExpressions;
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
            string name = InputName();
            int imprintDate = InputImprintDate();
            string author = InputAuthor();
            PrintedMatter newItem = new Book(name, imprintDate, author);
            _list.AddAsync(newItem);
            SaveAsync(serial);
        }

        /// <summary>
        /// Добавить новый журнал
        /// </summary>
        void AddMagazine()
        {
            string name = InputName();
            int imprintDate = InputImprintDate();
            int numberOfEdition = InputNumberOfEdition();
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
            string name = String.Empty;
            string pattern = @"\w";
            Regex regex = new Regex(pattern);
            bool isWrongName = true;
            while (isWrongName)
            {
                Console.Write("Input name: ");
                name = Console.ReadLine();
                isWrongName = !regex.IsMatch(name, 0);
                if(isWrongName)
                    Console.WriteLine("Incorrect data!");
            }
            return name;
        }
        int InputImprintDate()
        {
            int imprintDate = 0;
            bool isNumber = false;
            while (!isNumber)
            {
                Console.Write("Input imprint date: ");
                isNumber = Int32.TryParse(Console.ReadLine(), out imprintDate);
                if(!isNumber)
                    Console.WriteLine("Incorrect data!");
            } 
            return imprintDate;
        }
        int InputNumberOfEdition()
        {
            int numberOfEdition = 0;
            bool isNumber = false;
            while (!isNumber)
            {
                Console.Write("Input number of edition: ");
                isNumber = Int32.TryParse(Console.ReadLine(), out numberOfEdition);
                isNumber = isNumber && (numberOfEdition < 1);
                if (!isNumber)
                    Console.WriteLine("Incorrect data!");
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
            int number = 0;
            bool isNumber = false;
            while (!isNumber)
            {
                Console.Write("Input number of element: ");
                isNumber = Int32.TryParse(Console.ReadLine(), out number);
                isNumber = isNumber && (number < 1);
                if (!isNumber)
                    Console.WriteLine("Incorrect data!");
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
            _fileNameCatalog = InputFileName();
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

        private string InputFileName()
        {
            string name = String.Empty;
            string pattern = @"\w";
            Regex regex = new Regex(pattern);
            bool isWrongName = true;
            while (isWrongName)
            {
                Console.Write("Input name catalog: ");
                name = Console.ReadLine();
                isWrongName = !(name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1 && regex.IsMatch(name,0));
                if (isWrongName)
                    Console.WriteLine("Incorrect data!");
            }
            return name + ".txt";
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
            int id = 0;
            bool isNumber = false;
            while (!isNumber)
            {
                Console.Write("Input id of element: ");
                isNumber = Int32.TryParse(Console.ReadLine(), out id);
                if (!isNumber)
                    Console.WriteLine("Incorrect data!");
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
