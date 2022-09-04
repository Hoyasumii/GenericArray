using System;

namespace GenericsArray.Resources
{
    public class GenericArray<T> where T : class
    {
        private T[] Array = null!;
        public int Size { get; private set; }
        public bool Resizable { get; private set; } = true;

        public GenericArray(int size)
        {
            Size = size;
            Array = new T[Size];
        }

        public GenericArray(int size, bool resizable) : this(size)
        {
            Resizable = resizable;
        }

        public GenericArray(T[] array)
        {
            Size = array.Length;
            Array = array;
        }

        public GenericArray(T[] array, int sizeAppend, bool resizable)
        {
            Resizable = resizable;
            Size = array.Length + sizeAppend;
            Array = new T[Size];

            for (int index = 0; index < array.Length; index++)
            {
                Array[index] = array[index];
            }
        }

        private int IndexValidator(int index)
        {
            return index < 0 ? 0 : index >= Array.Length ? Array.Length - 1 : index;
        }

        public T Get(int index)
        {
            return Array[IndexValidator(index)];
        }

        public T[] GetValues()
        {
            return Array;
        }

        public bool HasNull()
        {
            foreach (T item in Array)
            {
                if (item == null)
                {
                    return true;
                }
            }

            return false;

        }

        public bool IsNull(int index)
        {
            return Array[IndexValidator(index)] == null;
        }

        public int NumberOfEmptyItems()
        {
            int counter = 0;

            foreach (T item in Array)
            {
                if (item == null)
                {
                    counter++;
                }
            }

            return counter;

        }

        public int EmptyIndex()
        {
            int value = 0;
            bool hasNull = HasNull();

            if (hasNull)
            {
                for (int index = 0; index < Array.Length; index++)
                {
                    if (Array[index] == null)
                    {
                        value = index;
                        break;
                    }
                }
            }
            else
            {
                value = -1;
            }

            return value;

        }

        public void Add(T item)
        {
            int numberOfEmptyItems = NumberOfEmptyItems();

            if (numberOfEmptyItems == 0 && !Resizable) throw new GenericException("The Array is full and cannot be resized.");

            else if (numberOfEmptyItems == 0) AppendSize(1);

            Array[EmptyIndex()] = item;
        }

        public void Add(params T[] items)
        {
            if (items.Length > NumberOfEmptyItems()) throw new GenericException("Array cannot store all these values and cannot be resized.");

            foreach (T item in items)
            {
                Add(item);
            }

        }

        public void Add(T item, int index)
        {
            Array[IndexValidator(index)] = item;
        }

        public void AddSecure(T item, int index)
        {
            if (Array[IndexValidator(index)] != null) throw new GenericException("This position is occupied.");

            Array[IndexValidator(index)] = item;
        }

        public void Remove(int index)
        {
            Array[IndexValidator(index)] = null!;
        }

        public void Clear()
        {
            Array = new T[Size];
        }

        public void AppendSize(uint sizeToAppend)
        {

            if (!Resizable) throw new GenericException("The Array cannot be resized.");

            Size += (int)sizeToAppend;
            T[] tempArray = new T[Size];

            for (int index = 0; index < Array.Length; index++)
            {
                tempArray[index] = Array[index];
            }

            Array = tempArray;

        }

        public void Resize(uint newSize)
        {
            if (!Resizable) throw new GenericException("The Array cannot be resized.");

            Size = (int)newSize;
            T[] tempArray = new T[Size];

            for (int index = 0; index < tempArray.Length; index++)
            {
                try
                {
                    tempArray[index] = Array[index];
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            Array = tempArray;

        }

        public void Optimize()
        {
            T[] tempArray = new T[Size];
            int index = 0;

            foreach (T item in Array)
            {
                if (item != null)
                {
                    tempArray[index] = item;
                    index++;
                }
            }

            Array = tempArray;
        }

        public T[] Copy()
        {
            T[] tempArray = new T[Size];

            for (int index = 0; index < tempArray.Length; index++)
            {
                tempArray[index] = Array[index];
            }

            return tempArray;
        }

        public T[] Split(int indexStart, int indexEnd)
        {

            indexStart = IndexValidator(indexStart);
            indexEnd = IndexValidator(indexEnd);

            if (indexEnd < indexStart) throw new GenericException("indexEnd must be greater than indexStart");

            T[] tempArray = new T[indexEnd - indexStart];

            for (int index = indexStart; index < indexEnd; index++)
            {
                tempArray[index] = Array[index];
            }

            return tempArray;

        }

        public override string ToString()
        {
            return $"Array<{typeof(T)}>\nSize: {Size}\nEmpty Spaces: {NumberOfEmptyItems()}\nResizable: {Resizable}";
        }

    }
}
