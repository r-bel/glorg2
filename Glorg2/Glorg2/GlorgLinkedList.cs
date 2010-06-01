using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	public class GlorgLinkedList<T> : IList<T>, IEnumerable<T>
	{

		public class Enumerator : IEnumerator<T>
		{
			Element current;
			GlorgLinkedList<T> owner;

			internal Element CurrentElement { get { return current; } set { current = value; } }

			internal Enumerator(GlorgLinkedList<T> list)
			{
				owner = list;
			}
			#region IEnumerator<T> Members

			public T Current
			{
				get { return current.Value; }
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return current.Value; }
			}

			public bool MoveNext()
			{
				if (current == null)
					return (current = owner.first) != null;
				else
					return (current = current.Next) != null;
			}

			public void Reset()
			{
				current = null;
			}

			#endregion
		}

		public class Element
		{
			T value;

			internal Element next;
			internal Element previous;

			public T Value { get { return value; } set { this.value = value; } }
			public Element Next { get { return next; } }
			public Element Previous { get { return previous; } }
		}

		Element first;
		Element last;
		int count;

		public GlorgLinkedList()
		{
			first = null;
			last = null;
			count = 0;
		}

		public void Sort(Comparison<T> comp)
		{
			bool swapped;
			Enumerator en = GetEnumerator() as Enumerator;
			do
			{
				swapped = false;
				while (en.MoveNext() && en.CurrentElement.next != null)
				{
					if (comp(en.Current, en.CurrentElement.next.Value) > 0)
					{
						T tmp = en.Current;
						en.CurrentElement.Value = en.CurrentElement.next.Value;
						en.CurrentElement.next.Value = tmp;
						swapped = true;
					}
				}
				en.Reset();
			} while (swapped);
		}

		public void AddLast(T value)
		{
			if (first == null)
			{
				first = new Element()
				{
					Value = value,
					next = null,
					previous = null
				};
				last = first;
			}
			else if (last == first)
			{
				last = new Element()
				{
					Value = value,
					next = null,
					previous = first,
				};
				first.next = last;
			}
			else
			{
				last.next = new Element()
				{
					Value = value,
					next = null,
					previous = last
				};
				last = last.next;
			}
			count++;
		}
		public void AddFirst(T value)
		{
			if (first == null)
			{
				first = new Element()
				{
					Value = value,
					next = null,
					previous = null
				};
				last = first;
			}
			else if (last == first)
			{
				last = new Element()
				{
					Value = value,
					next = null,
					previous = first,
				};
				first.next = last;
			}
			else
			{
				first.previous = new Element()
				{
					Value = value,
					next = first,
					previous = null
				};
				first = first.previous;
			}
			++count;
		}

		public void RemoveFirst()
		{
			if (first != null)
			{
				if (first == last)
				{
					first = null;
					last = null;
				}
				else
					first = first.next;
				--count;
			}
		}
		public void RemoveLast()
		{
			if (last != null)
			{
				if (last == first)
				{
					first = null;
					last = null;
				}
				else
					last = last.previous;
				--count;
			}
		}

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return new Enumerator(this);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		#endregion

		#region IList<T> Members

		public int IndexOf(T item)
		{
			var en = GetEnumerator();
			for (int i = 0; en.MoveNext(); i++)
			{
				if (en.Current.Equals(item))
					return i;
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			Enumerator en = GetEnumerator() as Enumerator;
			for (int i = 0; i < index; i++)
				en.MoveNext();
			Insert(en, item);
		}

		public void Insert(Enumerator at, T item)
		{
			if (at.CurrentElement.previous == null)
			{
				at.CurrentElement.previous = new Element()
				{
					Value = item,
					next = at.CurrentElement,
					previous = null
				};
				first = at.CurrentElement.previous;
				at.CurrentElement = first;
				++count;
			}
			else
			{
				var prev = at.CurrentElement.previous;
				prev.next = new Element()
				{
					previous = prev,
					next = at.CurrentElement,
					Value = item
				};
				at.CurrentElement.previous = prev.next;
				at.CurrentElement = prev.Next;
				++count;
			}
		}
		public void RemoveAt(Enumerator at)
		{
			if (at.CurrentElement.previous == null)
			{
				var next = at.CurrentElement.next;
				next.previous = null;
				first = next;
				at.CurrentElement = first;
				--count;
			}
			else if (at.CurrentElement.next == null)
			{
				var prev = at.CurrentElement.previous;
				prev.next = null;
				last = prev;
				at.CurrentElement = last;
				--count;
			}
			else
			{
				var prev = at.CurrentElement.previous;
				var next = at.CurrentElement.next;
				next.previous = prev;
				prev.next = next;
				at.CurrentElement = prev;
				--count;
			}
		}

		public void RemoveAt(int index)
		{
			Enumerator en = GetEnumerator() as Enumerator;

			for (int i = 0; i < index; i++)
				en.MoveNext();

			RemoveAt(en);
		}

		public T this[int index]
		{
			get
			{
				Enumerator en = GetEnumerator() as Enumerator;

				for (int i = 0; i < index; i++)
					en.MoveNext();
				return en.Current;
			}
			set
			{
				Enumerator en = GetEnumerator() as Enumerator;

				for (int i = 0; i < index; i++)
					en.MoveNext();

				en.CurrentElement.Value = value;
			}
		}

		#endregion

		#region ICollection<T> Members

		public void Add(T item)
		{
			AddLast(item);
		}

		public void Clear()
		{
			last = null;
			first = null;
			count = 0;
		}

		public bool Contains(T item)
		{
			Enumerator en = GetEnumerator() as Enumerator;

			while (en.MoveNext())
				if (en.Current.Equals(item))
					return true;
			return false;

		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Enumerator en = GetEnumerator() as Enumerator;

			for (int i = arrayIndex; en.MoveNext(); i++)
				array[i] = en.Current;
		}

		public int Count
		{
			get { return count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(T item)
		{
			Enumerator en = GetEnumerator() as Enumerator;
			bool ret = false;
			while (en.MoveNext())
			{
				if (en.Current.Equals(item))
				{
					RemoveAt(en);
					ret = true;
				}
			}
			return ret;
		}

		#endregion
	}
}
