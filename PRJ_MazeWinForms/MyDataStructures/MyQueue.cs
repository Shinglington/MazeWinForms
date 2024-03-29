﻿using System;

namespace MyDataStructures
{
    public class MyQueue<T>
    {
        // Implementation of queue, which uses previous implementation of list to store items
        // When "Dequeuing", returns the item that was added earliest


        private MyList<T> _items;
        private int _front;
        private int _back;
        private int _size;

        public MyQueue()
        {
            _items = new MyList<T>();
            _front = 0;
            _back = 0;
            _size = 0;
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }

        // Add item to back of queue, increment back marker by 1
        public void Enqueue(T item)
        {
            _items.Add(item);
            _back += 1;
            _size += 1;
        }

        // Remove item from front of queue, increment front marker by 1
        public T Dequeue()
        {
            if (_size <= 0)
            {
                throw new InvalidOperationException();
            }
            T item = _items[_front];
            _front += 1;
            _size -= 1;
            _items.Remove(item);
            return item;
        }

        // Access item from front without removing it
        public T Peek()
        {
            if (_size <= 0)
            {
                throw new InvalidOperationException();
            }
            return _items[_front];
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
