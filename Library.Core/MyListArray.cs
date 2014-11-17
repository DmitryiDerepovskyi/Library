using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library.Core
{
    [Serializable]
    public class MyListArray<T> : IEnumerable<T> where T : PrintedMatter
    {
        private int _maxLength;
        private int _count;
        private T[] _array;

        public MyListArray()
        {
            _count = 0;
            _maxLength = 10;
            _array = new T[_maxLength];
        }

        public MyListArray(int count)
        {
            _count = 0;
            _maxLength = count;
            _array = new T[_maxLength];
        }
        /// <summary>
        /// The number of elements
        /// </summary>
        public int Length 
        { 
            get { return _count; } 
        }
        /// <summary>
        /// Добавляет элемент в конец очереди
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            if (_count >= _maxLength)
            {
                _maxLength += 10;
                T[] oldArray = _array;
                _array = new T[_maxLength];
                oldArray.CopyTo(_array, 0);
            }
            _array[_count] = element;
            _count++;
        }
        /// <summary>
        /// Удаляет элемент под указанным номером 
        /// </summary>
        /// <param name="count"></param>
        public void Remove(int count)
        {
            if (count > 0)
            {
                if (_count > 0)
                {
                    for (int i = count + 1; i < _count; i++)
                    {
                        _array[i - 1] = _array[i];
                    }
                }
                _count--;
            }
        }

        public void Clear()
        {
            _count = 0;
            _maxLength = 10;
            _array = new T[_maxLength];
        }

        /// <summary>
        /// Обновляет указанный элемент 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="newItem"></param>
        public void Update(int count, T newItem)
        {
            _array[count] = newItem;
        }
        /// <summary>
        /// Возвращает элемент под заданым номером
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public T GetElement(int count)
        {
            if (count > _count)
                throw new ArgumentException(); 
            return _array[count];
        }
        /// <summary>
        /// Поиск элемента по заданному id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int IndexOf(int id)
        {
            int index = -1;
            for (int i = 0; i < _count; i++)
                if (_array[i].id == id)
                {
                    index = i;
                    break;
                }
            return index;
        }
      
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            int current = 0;

            while (current != _count)
            {
                yield return _array[current];
                current++;
            }
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            int current = 0;

            while (current != _count)
            {
                yield return _array[current];
                current++;
            }
        }
    }
}
