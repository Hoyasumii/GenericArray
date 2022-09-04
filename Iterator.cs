using System;

namespace GenericsArray.Resources
{
	public class Iterator<T>
	{
		private readonly T[] Iterable;
		private int ActualIndex = 0;

		public Iterator(T[] iterable)
		{
			Iterable = iterable;
		}

		public T Next()
		{
			if (ActualIndex >= Iterable.Length) throw new IndexOutOfRangeException("There is no next element.");
			return Iterable[ActualIndex++];
		}

		public T Previous()
		{
			if (ActualIndex < 0) throw new IndexOutOfRangeException("There is no next element.");
			return Iterable[ActualIndex--];
		}

		public void ResetIterator()
		{
			ActualIndex = 0;
		}

	}
}
