using System;
using System.Collections;
using System.Collections.Generic;

namespace MazeConsole.MyDataStructures
{

    public class MyList<T> : IList<T>
    {
        private const int _initialCapacity = 4;

        private T[] _items;
        private int _size;

        // Initialised empty
        public MyList()
        {
            _items = new T[0];
            _size = 0;
        }

        // Initialised with an array
        public MyList(IEnumerable<T> collection)
        {
            ICollection<T> c = collection as ICollection<T>;
            // if collection exists
            if (c != null)
            {
                int count = c.Count;
                if (count == 0)
                {
                    _items = new T[0];
                    _size = 0;
                }
                else
                {
                    _items = new T[count];
                    c.CopyTo(_items, 0);
                    _size = count;
                }
            }
            // Otherwise, create empty list
            else
            {
                _items = new T[0];
                _size = 0;
            }
        }

        // Capacity
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        // create new size list to copy old items to
                        T[] tempItems = new T[value];
                        for (int i = 0; i < _size; i++)
                        {
                            tempItems[i] = _items[i];
                        }
                        _items = tempItems;
                    }
                    else
                    {
                        _items = new T[0];
                    }
                }
            }
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (_items.Length < minCapacity)
            {
                int newCapacity = _items.Length == 0 ? _initialCapacity : _items.Length * 2;
                if (newCapacity < minCapacity)
                {
                    newCapacity = minCapacity;
                }
                Capacity = newCapacity;
            }
        }

        // Count
        public int Count
        {
            get
            {
                return _size;
            }
        }

        // Sets or gets element in given index
        public T this[int index]
        {
            get
            {
                if (index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _items[index];
            }

            set
            {
                if (index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[index] = value;

            }
        }

        public void Add(T item)
        {
            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }
            _items[_size] = item;
            _size++;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(T item)
        {
            int index = -1;
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void Insert(int index, T item)
        {
            if (Capacity == _size)
            {
                EnsureCapacity(_size + 1);
            }
            T[] newItems = new T[Capacity];
            // add items before index insert
            for (int i = 0; i < index; i++)
            {
                newItems[i] = _items[i];
            }
            newItems[index] = item;
            // add items after index insert
            for (int i = index; i < Count; i++)
            {
                newItems[i + 1] = _items[i];
            }
            _items = newItems;

        }

        public void RemoveAt(int index)
        {
            T[] newItems = new T[Capacity];
            // add items before index remove
            for (int i = 0; i < index; i++)
            {
                newItems[i] = _items[i];
            }
            // add items after index remove
            for (int i = index + 1; i < _size; i++)
            {
                newItems[i - 1] = _items[i];
            }
            _items = newItems;
            _size -= 1;
        }

        public void Clear()
        {
            _items = new T[_initialCapacity];
            _size = 0;
            Capacity = 0;
        }

        public bool Contains(T item)
        {
            bool found = false;
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(item))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < _size; i++)
            {
                array[i + arrayIndex] = _items[i];
            }
        }

        public bool Remove(T item)
        {
            bool removed = false;
            T[] newItems = new T[Capacity];
            // add items before index remove
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(item))
                {
                    removed = true;
                }
                else if (!removed)
                {
                    newItems[i] = _items[i];
                }
                else
                {
                    newItems[i - 1] = _items[i];
                }

            }
            _items = newItems;
            if (removed)
            {
                _size -= 1;
            }
            return removed;

        }

        public T[] ToArray()
        {
            T[] array = new T[_size];
            for (int i = 0; i < _size; i++)
            {
                array[i] = _items[i];
            }
            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public struct Enumerator<T> : IEnumerator<T>
    {
        private MyList<T> list;
        private int index;
        private T current;

        internal Enumerator(MyList<T> list)
        {
            this.list = list;
            index = 0;
            current = default(T);
        }

        public T Current
        {
            get { return current; }

        }

        object IEnumerator.Current
        {
            get { return current; }
        }


        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (index >= list.Count)
            {
                return false;
            }
            else
            {
                current = list[index];
                index++;
                return true;
            }

        }

        public void Reset()
        {
            index = 0;
            current = default(T);
        }
    }

}
