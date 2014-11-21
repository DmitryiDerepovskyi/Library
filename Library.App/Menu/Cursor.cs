using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.App.Menu
{
    public class Cursor
    {
        private Cursor()
        {
            top = 0;
            left = 0;
            viewCursor = "->";
            _lenght = viewCursor.Length;
            visible = true;
        }
        static Cursor cursor;
        int _lenght;
        public string viewCursor;
        public bool visible;
        public int top;
        public int left;
        /// <summary>
        /// Отрисовка указателя
        /// </summary>
        public void Print()
        {
            SetCursor(top, (left - _lenght));
            if(visible)
                Console.Write(viewCursor);
        }
        public static Cursor Create()
        {
            if (cursor != null)
                return null;
            else
                return cursor = new Cursor();
        }
        /// <summary>
        /// Усиановить курсор в указаную позицию 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public void SetCursor(int top, int left)
        {
            this.top = top;
            this.left = left;
            Console.CursorTop = this.top;
            Console.CursorLeft = this.left;
        }
    }
}
