using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbandoMap
{
    class MinHeap
    {
        private List<Arista> array = new List<Arista>();

        public void Add(Arista element)
        {
            array.Add(element);
            int c = array.Count - 1;
            while (c > 0 && array[c].peso.CompareTo(array[c / 2].peso) == -1)
            {
                Arista tmp = array[c];
                array[c] = array[c / 2];
                array[c / 2] = tmp;
                c = c / 2;
            }
        }

        public Arista RemoveMin()
        {
            Arista ret = array[0];
            array[0] = array[array.Count - 1];
            array.RemoveAt(array.Count - 1);

            int c = 0;
            while (c < array.Count)
            {
                int min = c;
                if (2 * c + 1 < array.Count && array[2 * c + 1].peso.CompareTo(array[min].peso) == -1)
                    min = 2 * c + 1;
                if (2 * c + 2 < array.Count && array[2 * c + 2].peso.CompareTo(array[min].peso) == -1)
                    min = 2 * c + 2;

                if (min == c)
                    break;
                else
                {
                    Arista tmp = array[c];
                    array[c] = array[min];
                    array[min] = tmp;
                    c = min;
                }
            }

            return ret;
        }

        public Arista Peek()
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

        public void Add(Arista element)
        {
            minHeap.Add(element);
        }

        public Arista RemoveMin()
        {
            return minHeap.RemoveMin();
        }

        public Arista Peek()
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
