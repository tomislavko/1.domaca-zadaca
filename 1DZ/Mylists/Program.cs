using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLists
{
    public interface IIntegerList
    {
        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        void Add(int item);

        /// <summary>
        /// Removes the first occurrence of an item from the collection.
        /// If the item was not found, method does nothing.
        /// </summary>
        bool Remove(int index);

        /// <summary>
        /// Removes the item at the given index in the collection.
        /// </summary>
        bool RemoveAt(int index);

        /// <summary>
        /// Returns the item at the given index in the collection.
        /// </summary>
        int GetElement(int index);

        /// <summary>
        /// Returns the index of the item in the collection.
        /// If item is nor found in the collection, method returns -1.
        /// </summary>
        int IndexOf(int item);

        /// <summary>
        /// Read only property. Gets the number of items contained in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether the collections contains a specific value
        /// </summary>
        bool Contains(int item);
    }


    public interface IGenericList<X> : IEnumerable<X>
    {
        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        void Add(X item);

        /// <summary>
        /// Removes the first occurrence of an item from the collection.
        /// If the item was not found, method does nothing.
        /// </summary>
        bool Remove(X index);

        /// <summary>
        /// Removes the item at the given index in the collection.
        /// </summary>
        bool RemoveAt(int index);

        /// <summary>
        /// Returns the item at the given index in the collection.
        /// </summary>
        X GetElement(int index);

        /// <summary>
        /// Returns the index of the item in the collection.
        /// If item is nor found in the collection, method returns -1.
        /// </summary>
        int IndexOf(X item);

        /// <summary>
        /// Read only property. Gets the number of items contained in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether the collections contains a specific value
        /// </summary>
        bool Contains(X item);
    }

    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private int _arraySize;
        private int _count;

        public IntegerList()
        {
            _internalStorage = new int[4];
            _arraySize = 4;
            _count = -1;
        }

        public IntegerList(int initialSize)
        {
            _internalStorage = new int[initialSize];
            _arraySize = initialSize;
            _count = -1;
        }

        public void Add(int item)
        {
            if (_count + 1 >= _arraySize)
            {
                int[] tmp = new int[_arraySize *= 2];
                int index = 0;
                foreach (int value in _internalStorage)
                {
                    tmp[index++] = value;
                }
                _internalStorage = tmp;
            }
            _internalStorage[++_count] = item;
        }


        public bool Remove(int item)
        {
            return RemoveAt(IndexOf(item));
        }


        public bool RemoveAt(int index)
        {
            if (index < 0 || index > _count)
            {
                return false;
            }
            while (index < _count)
            {
                _internalStorage[index] = _internalStorage[index + 1];
                index++;
            }
            _count--;
            return true;
        }


        public int GetElement(int index)
        {
            if (index >= 0 && index <= _arraySize)
            {
                return _internalStorage[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        public int IndexOf(int item)
        {
            for (int i = 0; i <= _count; i++)
            {
                if (_internalStorage[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public int Count
        {
            get
            {
                return _count + 1;
            }
        }


        public void Clear()
        {
            _internalStorage = null;
            _count = -1;
        }

        public bool Contains(int item)
        {
            if (IndexOf(item) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public class GenericList<X> : IGenericList<X> where X : IComparable<X>
    {
        private X[] _internalStorage;       // bolje je koristiti ArrayList _innerArray;
        private int _arraySize;
        private int _count;

        public GenericList()
        {
            _internalStorage = new X[4];
            _arraySize = 4;
            _count = -1;
        }

        public GenericList(int initialSize)
        {
            _internalStorage = new X[initialSize];
            _arraySize = initialSize;
            _count = -1;
        }

        public void Add(X item)
        {
            if (_count + 1 >= _arraySize)
            {
                X[] tmp = new X[_arraySize *= 2];
                int index = 0;
                foreach (X value in _internalStorage)
                {
                    tmp[index++] = value;
                }
                _internalStorage = tmp;
            }
            _internalStorage[++_count] = item;
        }


        public bool Remove(X item)
        {
            return RemoveAt(IndexOf(item));
        }


        public bool RemoveAt(int index)
        {
            if (index < 0 || index > _count)
            {
                return false;
            }
            while (index < _count)
            {
                _internalStorage[index] = _internalStorage[index + 1];
                index++;
            }
            _count--;
            return true;
        }


        public X GetElement(int index)
        {
            if (index >= 0 && index <= _arraySize)
            {
                return _internalStorage[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public int IndexOf(X item)
        {
            int index = 0;
            foreach (X value in _internalStorage)
            {
                if (item.CompareTo(value) == 0)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public int Count
        {
            get
            {
                return _count + 1;
            }
        }


        public void Clear()
        {
            _internalStorage = null;
            _count = -1;
        }

        public bool Contains(X item)
        {
            if (IndexOf(item) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GenericListEnumerator<X> : IEnumerator<X> where X : IComparable<X>
    {
        private GenericList<X> _collection = new GenericList<X>();
        private int index;
        private X _current;

        public GenericListEnumerator()
        {

        }

        public GenericListEnumerator(GenericList<X> collection)
        {
            _collection = collection;
            index = -1;
            _current = default(X);
        }



        public X Current
        {
            get
            {
                return _current;
            }
        }


        object IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }
        public void Dispose()
        {
            _collection = null;
            _current = default(X);
            index = -1;
        }

        public bool MoveNext()
        {
            if (++index >= _collection.Count)
            {
                return false;
            }
            else
            {
                _current = _collection.GetElement(index);
            }
            return true;
        }

        public void Reset()
        {
            _current = default(X);
            index = -1;
        }
    }

    public class Test
    {
        public static void ListExample(IIntegerList listOfIntegers)
        {
            listOfIntegers.Add(1);  // [1]
            listOfIntegers.Add(2);  // [1, 2]
            listOfIntegers.Add(3);  // [1, 2, 3]
            listOfIntegers.Add(4);  // [1, 2, 3, 4]
            listOfIntegers.Add(5);  // [1, 2, 3, 4, 5]

            for (int i = 0; i < listOfIntegers.Count; i++)
            {
                Console.WriteLine(listOfIntegers.GetElement(i));
            }

            listOfIntegers.RemoveAt(0);
            listOfIntegers.Remove(5);

            Console.WriteLine();
            for (int i = 0; i < listOfIntegers.Count; i++)
            {
                Console.WriteLine(listOfIntegers.GetElement(i));
            }
            Console.WriteLine();
            Console.WriteLine(listOfIntegers.Count);
            Console.WriteLine(listOfIntegers.Remove(100));
            Console.WriteLine(listOfIntegers.RemoveAt(5));
            listOfIntegers.Clear();
            Console.WriteLine(listOfIntegers.Count);

            IGenericList<string> stringList = new GenericList<string>();
            stringList.Add("Hello");
            stringList.Add("World");
            stringList.Add("!");

            Console.WriteLine();
            foreach (string value in stringList)
            {
                Console.WriteLine(value);
            }
        }
    }

}
