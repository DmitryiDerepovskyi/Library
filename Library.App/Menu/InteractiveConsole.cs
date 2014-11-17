using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library.Core;
using Library.App.Catalog;
using System.Text.RegularExpressions;

namespace Library.App.Menu
{
    class InteractiveConsole
    {
        public InteractiveConsole()
        {
            _pathCurrentMenu = "Catalog.MainMenu";
            _operation = "operation";
            _menuItem = 0;
            _navigationMenu = new Dictionary<ConsoleKey, Menu>
            {
                { ConsoleKey.DownArrow, this.DownCursor},
                { ConsoleKey.UpArrow, this.UpCursor},
                { ConsoleKey.Enter, this.EnterMenuItem},
                { ConsoleKey.Escape, this.EscapeMenu},
            };
          
        }
        #region Variables
        // функции для навигации по меню
        Dictionary<ConsoleKey, Menu> _navigationMenu;
        delegate void Menu();
        // верхний левый угол меню  
        const int LEFT = 7;
        const int TOP = 3;
        // текущая место положение
        string _pathCurrentMenu;
        // текущая операция
        string _operation;
        // имя файла, где хранится footer
        const string _footerOperation = "Press Enter to repeat operation, Esc- exit";
        const string _footerMenu = "Press Enter to skip, Esc - exit";
        // меню
        string _menu;
        // номер выбраного элемента меню
        int _menuItem;
        LibraryCatalog libraryCatalog = new LibraryCatalog();
        // объект курсора
        Cursor cursor = Cursor.Create();
        // введеный символ
        ConsoleKey choose;
        #endregion
        /// <summary>
        /// Запуск консоли
        /// </summary>
        public void Process()
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                // вывод пути
                PrintLocation();
                bool performOperation = libraryCatalog.operationCatalog.ContainsKey(_operation) && choose == ConsoleKey.Enter;
                if (performOperation)
                    PerformOperation();
                else
                {
                    _menu = PrintMainMenu(ref _menuItem);
                }
                PrintFooter(performOperation);
                PrintCursor(_menu);
                choose = Console.ReadKey(true).Key;
                try
                {
                    _navigationMenu[choose]();
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
               
            }
        }
        // Отрисовка курсора
        private void PrintCursor(string menu)
        {
            // определяем к-во строк в меню
            int maxNumberOfItemMenu = new Regex("\n").Matches(menu).Count - 1;
            if (maxNumberOfItemMenu == -1)
            {
                return;
            }
            if (_menuItem < 0)
                _menuItem = 0;
            _menuItem = Math.Max(0, _menuItem);
            _menuItem = Math.Min(maxNumberOfItemMenu, _menuItem);
            // рисуем курсор в указаной позиции
            cursor.SetCursor(TOP + _menuItem, LEFT);
            cursor.Print();
        }

        // Выполнение операции
        private void PerformOperation()
        {
                Console.CursorVisible = true;
                cursor.visible = false;
                libraryCatalog.operationCatalog[_operation]();
                Console.CursorVisible = false;
        }
        // Вывод меню
        string PrintMainMenu(ref int _menuItem)
        {
            cursor.visible = true;
            // поместим сюда меню
            StringBuilder _sbmenu = new StringBuilder();
            // ищем файл с заданным именем
            string path = PathFolder(_pathCurrentMenu);
            if (!File.Exists(path))
            {
                Console.WriteLine("Don't find file MainMenu.txt");
                EscapeMenu();
            }
            else
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    //start position for menu
                    cursor.SetCursor(TOP, LEFT);
                    while (!sr.EndOfStream)
                    {
                        string buffer = sr.ReadLine();
                        _sbmenu.AppendLine(buffer);
                        cursor.SetCursor(cursor.top, LEFT);
                        Console.WriteLine(buffer);
                        cursor.top++;
                    }
                }
            }
            return _sbmenu.ToString();
        }
        // получения пути к файлу
        string PathFolder(string currentFolder)
        {
            int indexPoint = currentFolder.LastIndexOf(".");
            return "Menu//" + currentFolder.Substring(indexPoint + 1, currentFolder.Length - indexPoint - 1) + ".txt";  
        }
        void PrintFooter(bool performOperation)
        {
            cursor.SetCursor(Console.WindowHeight - 1, 0);
            if(performOperation)
                Console.Write(_footerOperation);
            else
                Console.Write(_footerMenu);
        }
        void PrintLocation()
        {
            //position Path
            cursor.SetCursor(0, 2);
            Console.WriteLine(_pathCurrentMenu);
        }
        // переместить курсор вниз
        void DownCursor()
        {
            if (cursor.visible)
                _menuItem++;
        }
        // вверх
        void UpCursor()
        {
            if (cursor.visible)
                _menuItem--;
        }

        //выбрать пункт меню
        void EnterMenuItem()
        {
            if (cursor.visible)
            {
                string nextMenu;
                try
                {
                    nextMenu = _menu.Split('\n')[cursor.top - TOP];
                }
                catch(IndexOutOfRangeException e)
                {
                    return;
                }
                nextMenu = nextMenu.TrimEnd('\r');
                string pathNextMenu = _pathCurrentMenu + "." + nextMenu;
                if (!File.Exists(PathFolder(pathNextMenu)))
                    _menu = String.Empty;
                _pathCurrentMenu = pathNextMenu;
                _operation = nextMenu;
            }
        }
        // вернуться в передыдущее меню
        void EscapeMenu()
        {
            int indexPoint = _pathCurrentMenu.LastIndexOf(".");
            if (indexPoint == _pathCurrentMenu.IndexOf("."))
                Exit();
            _pathCurrentMenu = _pathCurrentMenu.Remove(indexPoint, _pathCurrentMenu.Length - indexPoint);
        }

        void Exit()
        {
            Console.Clear();
            Environment.Exit(0);
        }
      
    }
}
