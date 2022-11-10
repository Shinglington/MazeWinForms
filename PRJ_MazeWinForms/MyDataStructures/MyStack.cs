using System;

namespace MazeConsole.MyDataStructures
{
    public class MyStack<T>
    {
        private MyList<T> _items;
        private int _top;
        private int _size;

        public MyStack()
        {
            _items = new MyList<T>();
            _top = -1;
            _size = 0;
        }

        // Add item to top of stack, increment back marker by 1
        public void Push(T item)
        {
            _items.Add(item);
            _top += 1;
            _size += 1;
        }

        // Remove item from top of stack
        public T Pull()
        {
            if (_size <= 0)
            {
                throw new InvalidOperationException();
            }
            T item = _items[_top];
            _items.RemoveAt(_top);
            _top -= 1;
            _size -= 1;
            return item;
        }

        // Access item at top without removing it
        public T Peek()
        {
            if (_size <= 0)
            {
                throw new IndexOutOfRangeException("Stack is empty");
            }
            return _items[_top];
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        // Convert to list
        public MyList<T> ToList()
        {
            return _items;
        }

        public T[] ToArray()
        {
            return _items.ToArray();
        }
    }
}
