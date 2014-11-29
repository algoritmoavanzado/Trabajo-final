using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbandoMap
{
    class MinHeap
    {
        private List<Nodo> array = new List<Nodo>();

        public void Add(Nodo element)
        {
            array.Add(element);
            int c = array.Count - 1;
            while (c > 0 && array[c].Peso.CompareTo(array[c / 2].Peso) == -1)
            {
                Nodo tmp = array[c];
                array[c] = array[c / 2];
                array[c / 2] = tmp;
                c = c / 2;
            }
        }

        public Nodo RemoveMin()
        {
            Nodo ret = array[0];
            array[0] = array[array.Count - 1];
            array.RemoveAt(array.Count - 1);

            int c = 0;
            while (c < array.Count)
            {
                int min = c;
                if (2 * c + 1 < array.Count && array[2 * c + 1].Peso.CompareTo(array[min].Peso) == -1)
                    min = 2 * c + 1;
                if (2 * c + 2 < array.Count && array[2 * c + 2].Peso.CompareTo(array[min].Peso) == -1)
                    min = 2 * c + 2;

                if (min == c)
                    break;
                else
                {
                    Nodo tmp = array[c];
                    array[c] = array[min];
                    array[min] = tmp;
                    c = min;
                }
            }

            return ret;
        }

        public Nodo Peek()
        {
            return array[0];
        }

        public int Count
        {
            get
            {
                return array.Count;
            }
        }
    }

    class PriorityQueue<T>
    {
        internal class Node : IComparable<Node>
        {
            public int Priority;
            public Nodo O;
            public int CompareTo(Node other)
            {
                return Priority.CompareTo(other.Priority);
            }
        }

        private MinHeap minHeap = new MinHeap();

        public void Add(Nodo element)
        {
            minHeap.Add(element);
        }

        public Nodo RemoveMin()
        {
            return minHeap.RemoveMin();
        }

        public Nodo Peek()
        {
            return minHeap.Peek();
        }

        public int Count
        {
            get
            {
                return minHeap.Count;
            }
        }
    }
}
