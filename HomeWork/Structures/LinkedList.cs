using System;

namespace HomeWork.Structures
{
    public class LinkedList<T>
    {
        public int Count { get; private set; }
        private LinkedNode<T> _head;
        private LinkedNode<T> _tail;

        public void AddInStart(T item)
        {
            if (_head == null)
            {
                _head = new LinkedNode<T> {Value = item};
                _tail = _head;
            }
            else
                _head = new LinkedNode<T> {Value = item, Next = _head};

            Count++;
        }

        public void AddInEnd(T element)
        {
            if (_tail == null)
            {
                _tail = new LinkedNode<T> {Value = element};
                _head = _tail;
            }
            else
            {
                _tail.Next = new LinkedNode<T> {Value = element};
                _tail = _tail.Next;
            }

            Count++;
        }

        public T RemoveAt(int index)
        {
            if (Count <= index || index < 0) 
                throw new IndexOutOfRangeException();
            var result = _head.Value;
            if (index == 0) 
                _head = _head.Next;
            else
            {
                var previousNode = Find(index - 1);
                if (previousNode.Next == null) 
                    throw new IndexOutOfRangeException();
                result = previousNode.Next.Value;
                previousNode.Next = previousNode.Next.Next;
            }

            Count--;
            return result;
        }

        private LinkedNode<T> Find(int index)
        {
            if (index == Count - 1) return _tail;
            var currentNode = _head;
            var i = 0;
            while (currentNode != null)
            {
                if (i == index) return currentNode;
                i++;
                currentNode = currentNode.Next;
            }

            throw new IndexOutOfRangeException();
        }
        
        private class LinkedNode<T>
        {
            public LinkedNode<T> Next;
            public T Value;
        }

        public T this[int index]
        {
            get => Find(index).Value;
        }
    }
}