using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork.Structures
{
public class BinaryHeap<T>
        where T : IComparable
    {
        private List<T> _heap = new List<T>();

        public BinaryHeap() { /* empty */ }

        public BinaryHeap(T root)
        {
            Add(root);
        }

        public BinaryHeap(IEnumerable<T> array)
        {
            BuildFromCollection(array);
        }

        public void Add(T element)
        {
            _heap.Add(element);
            var i = _heap.Count - 1;
            var parent = (i - 1) / 2;
            while (i > 0 && _heap[parent].CompareTo(_heap[i]) < 0)
            {
                Swap(i, parent);
                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public void Heapify(int index)
        {
            while (true)
            {
                var left = 2 * index + 1;
                var rigth = 2 * index + 2;
                var parent = index;

                if (left < _heap.Count && _heap[left].CompareTo(_heap[parent]) > 0)
                    parent = left;
                if (rigth < _heap.Count && _heap[rigth].CompareTo(_heap[parent]) > 0)
                    parent = rigth;

                if (parent == index)
                    break;
                Swap(index, parent);
                index = parent;
            }
        }

        public T RemoveMax()
        {
            if (_heap.Count == 0)
                throw new ArgumentException("heap is empty");

            var result = _heap[0];
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            Heapify(0);
            return result;
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            var tmp = _heap[firstIndex];
            _heap[firstIndex] = _heap[secondIndex];
            _heap[secondIndex] = tmp;
        }

        private void BuildFromCollection(IEnumerable<T> collection)
        {
            _heap = collection.ToList();
            for (var i = _heap.Count / 2; i >= 0; i--)
                Heapify(i);
        }
    }
}