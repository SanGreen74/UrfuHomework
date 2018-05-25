using System;
using System.Runtime.InteropServices;

namespace HomeWork.Structures
{
    public class Stack<T>
    {
        private readonly LinkedList<T> _stack;
        public int Count => _stack.Count;
        public Stack()
        {
            _stack = new LinkedList<T>();
        }

        public void Push(T item)
        {
            _stack.AddInStart(item);
        }

        public T Peek()
        {
            if (_stack.Count == 0)
                throw new ArgumentException("Stack is empty");
            return _stack[0];
        }

        public T Pop()
        {
            if (_stack.Count == 0)
                throw new ArgumentException("Stack is empty");
            return _stack.RemoveAt(0);
        }
    }
}