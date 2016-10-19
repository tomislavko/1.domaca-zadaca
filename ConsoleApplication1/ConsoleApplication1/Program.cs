using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface IIntegerList
    {
        /// <summary >
        /// Adds an item to the collection .
        /// </ summary >
        void Add(int item);
        /// <summary >
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing .
        /// </ summary >
        bool Remove(int item);
        /// <summary >
        /// Removes the item at the given index in the collection .
        /// </ summary >
        bool RemoveAt(int index);
        /// <summary >
        /// Returns the item at the given index in the collection .
        /// </ summary >
        int GetElement(int index);
        /// <summary >
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </ summary >
        int IndexOf(int item);
        /// <summary >
        /// /// Readonly property . Gets the number of items contained in the collection.
        /// </ summary >
        int Count { get; }
        /// <summary >
        /// Removes all items from the collection .
        /// </ summary >
        void Clear();
        /// <summary >
        /// Determines whether the collection contains a specific value .
        /// </ summary >
        bool Contains(int item);
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private static int _count;

        private IntegerList()
        {
            _internalStorage = new int[4];
            _count = 0;
        }
        private IntegerList(int initialSize)
        {
            if (initialSize > 0)
            {
                _internalStorage = new int[initialSize];
            }
            else
            {
                Console.WriteLine("Krivi unos inicijalizacija na standardnu vrijednost.");
                _internalStorage = new int[4];
            }
            _count = 0;
        }
        int IIntegerList.IndexOf(int item)
        {
            int find = -1;
            for(int i = 0; i < _count; i++)
            {
                if (_internalStorage[i] == item)
                {
                    find = i;
                }
                if (find != -1)
                {
                    break;
                }
            }
            return find;
        }
        int IIntegerList.Count
        {
            get{ return _count; }
        }
        int IIntegerList.GetElement(int index)
        {
            if (index < _count)
            {
                return _internalStorage[index];
            }
            else
            {
                throw IndexOutOfRangeException;
            }
        }
        bool IIntegerList.RemoveAt(int index)
        {
            if (index >= _count)
            {
                return false;
            }
            for (; index < _count; index++)
            {
                _internalStorage[index] = _internalStorage[index + 1];
            }
            _count--;
            return true;
        }
        void IIntegerList.Add(int x)
        {
            if (_count == _internalStorage.Length)
            {
                int[] sidestore = _internalStorage;
                _internalStorage = new int[2 * _internalStorage.Length];
                for(int i=0; i < sidestore.Length; i++)
                {
                    _internalStorage[i] = sidestore[i];
                }
            }
            _internalStorage[_count] = x;
            _count++;
        }
        bool IIntegerList.Remove(int item)
        {
            if (IIntegerList.IndexOf(item) != -1)
            {
                return IIntegerList.RemoveAt(IIntegerList.IndexOf(item));
            }
            return false;
        }
    }
}

