using System;
using System.Threading;

namespace HomeWork.Structures
{
    public class Queue<T>
    {
        public int Count => _queue.Count;
        private readonly LinkedList<T> _queue;

        public Queue()
        {
            _queue = new LinkedList<T>();
        }

        public void Enqueue(T item)
        {
            _queue.AddInStart(item);
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new ArgumentException("Queue is empty");
            return _queue.RemoveAt(Count - 1);
        }

    }
}