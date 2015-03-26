using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypergraphProject
{
    public class Set<T>
    {
        private List<T> elements;
        private Dictionary<T, int> indices;

        public Set(int capacity)
        {
            elements = new List<T>(capacity);
            indices = new Dictionary<T, int>(capacity);
        }

        public Set()
        {
            elements = new List<T>();
            indices = new Dictionary<T, int>();
        }

        public int Count
        {
            get
            {
                return elements.Count;
            }
        }

        public void Add(T item)
        {
            if (indices.ContainsKey(item))
            {
                return;
            }

            int newIndex = elements.Count;
            elements.Add(item);
            indices.Add(item, newIndex);
        }

        public T Remove()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return RemoveAt(0);
        }

        public void Remove(T item)
        {
            if (!indices.ContainsKey(item))
            {
                return;
            }

            int index = indices[item];
            RemoveAt(index);

        }

        private T RemoveAt(int index)
        {
            if (index < 0 || elements.Count <= index)
            {
                throw new ArgumentOutOfRangeException();
            }

            T item = elements[index];

            if (index != elements.Count - 1)
            {
                elements[index] = elements[elements.Count - 1];
                indices[elements[index]] = index;
            }

            elements.RemoveAt(elements.Count - 1);
            indices.Remove(item);

            return item;
        }
    }
}
