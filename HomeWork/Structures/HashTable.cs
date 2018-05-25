using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Structures
{
    public class HashTable<TKey, TValue>
    {
        private Container<TKey, TValue> _container;
        private uint countOfElemenets;

        public HashTable(int capacity)
        {
            _container = new Container<TKey, TValue>(capacity);
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException($"Argument with key {key} was added earlier");
            var indexInContainer = GetIndexInContainer(key);
            _container[indexInContainer].Add(Pair<TKey, TValue>.Create(key, value));
            countOfElemenets++;
            if (countOfElemenets / _container.Size >= 2)
                Rebuild();
        }

        public bool ContainsKey(TKey key)
        {
            var list = _container[GetIndexInContainer(key)];
            return list.Any(x => x.Key.Equals(key));
        }

        public TValue Get(TKey key)
        {
            var listOfElements = _container[GetIndexInContainer(key)];
            foreach (var bucket in listOfElements)
            {
                if (bucket.Key.Equals(key))
                    return bucket.Value;
            }
            return default(TValue);
        }

        public void Remove(TKey key)
        {
            var list = _container[GetIndexInContainer(key)];
            foreach (var element in list)
            {
                if (element.Key.Equals(key))
                {
                    list.Remove(element);
                    countOfElemenets--;
                    break;
                }
            }
        }

        private int GetIndexInContainer(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % _container.Size;
        }

        public void Rebuild()
        {
            var tmp = _container;
            _container = new Container<TKey, TValue>(_container.Size * 2 + 1);
            foreach (var list in tmp)
                foreach (var element in list)
                    Add(element.Key, element.Value);
        }
    }

    public class Container<TKey, TValue> : IEnumerable<List<Pair<TKey, TValue>>>
    {
        public int Size => listsArray.Length;

        private List<Pair<TKey, TValue>>[] listsArray;
        public Container(int capacity)
        {
            listsArray = new List<Pair<TKey, TValue>>[capacity];
        }

        public List<Pair<TKey, TValue>> this[int index]
        {
            get
            {
                if (listsArray[index] == null)
                    listsArray[index] = new List<Pair<TKey, TValue>>();
                return listsArray[index];
            }
        }

        public IEnumerator<List<Pair<TKey, TValue>>> GetEnumerator()
        {
            for (var i = 0; i < Size; i++)
            {
                if (listsArray[i] == null)
                    continue;
                yield return listsArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Pair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"Key: {Key}, Value: {Value}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Pair<TKey, TValue>)obj;
            return (other.Key.Equals(Key) && other.Value.Equals(Value));
        }

        public static Pair<TKey, TValue> Create(TKey key, TValue value)
        {
            return new Pair<TKey, TValue>(key, value);
        }
    }
}
