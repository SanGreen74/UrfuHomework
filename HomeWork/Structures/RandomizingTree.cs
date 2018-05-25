using System;

namespace HomeWork.Structures
{
public class RandomizingTree<T>
        where T : IComparable
    {
        private static readonly Random _randomizer = new Random();

        public int Count => _root == null ? 0 : _root.Size;

        private RtNode<T> _root;

        public bool Contains(T key)
        {
            return Find(_root, key) != null;
        }

        public void Insert(T key)
        {
            _root = Insert(_root, key);
        }

        public void Remove(T key)
        {
            _root = Remove(_root, key);
        }

        private static RtNode<T> Find(RtNode<T> rtNode, T key)
        {
            while (true)
            {
                if (rtNode == null) return null;
                if (rtNode.Key.CompareTo(key) == 0) return rtNode;
                rtNode = key.CompareTo(rtNode.Key) < 0 ? rtNode.Left : rtNode.Right;
            }
        }

        private static RtNode<T> Insert(RtNode<T> rtNode, T key)
        {
            if (rtNode == null)
                return new RtNode<T>(key);
            if (_randomizer.Next(rtNode.Size + 1) == 0)
                return InsertAsRoot(rtNode, key);
            if (key.CompareTo(rtNode.Key) < 0)
                rtNode.Left = Insert(rtNode.Left, key);
            else
                rtNode.Right = Insert(rtNode.Right, key);
            FixSize(rtNode);
            return rtNode;
        }

        private static RtNode<T> Remove(RtNode<T> rtNode, T key)
        {
            if (rtNode == null)
                return rtNode;

            if (key.CompareTo(rtNode.Key) == 0)
            {
                var node = Join(rtNode.Left, rtNode.Right);
                return node;
            }

            if (key.CompareTo(rtNode.Key) < 0)
                rtNode.Left = Remove(rtNode.Left, key);
            else
                rtNode.Right = Remove(rtNode.Right, key);
            FixSize(rtNode);
            return rtNode;
        }

        private static int GetSize(RtNode<T> rtNode)
        {
            return rtNode == null ? 0 : rtNode.Size;
        }

        private static void FixSize(RtNode<T> rtNode)
        {
            rtNode.Size = GetSize(rtNode.Left) + GetSize(rtNode.Right) + 1;
        }

        private static RtNode<T> RotateRight(RtNode<T> rtNode)
        {
            var nodeLeft = rtNode.Left;
            if (nodeLeft == null) return rtNode;
            rtNode.Left = nodeLeft.Right;
            nodeLeft.Right = rtNode;
            nodeLeft.Size = rtNode.Size;
            FixSize(nodeLeft);
            FixSize(rtNode);
            return nodeLeft;
        }

        private static RtNode<T> RotateLeft(RtNode<T> rtNode)
        {
            var nodeRight = rtNode.Right;
            if (nodeRight == null) return rtNode;
            rtNode.Right = nodeRight.Left;
            nodeRight.Left = rtNode;
            nodeRight.Size = rtNode.Size;
            FixSize(rtNode);
            FixSize(nodeRight);
            return nodeRight;
        }

        private static RtNode<T> InsertAsRoot(RtNode<T> rtNode, T key)
        {
            if (rtNode == null)
                return new RtNode<T>(key);
            if (key.CompareTo(rtNode.Key) < 0)
            {
                rtNode.Left = InsertAsRoot(rtNode.Left, key);
                return RotateRight(rtNode);
            }

            if (key.CompareTo(rtNode.Key) > 0)
            {
                rtNode.Right = InsertAsRoot(rtNode.Right, key);
                return RotateLeft(rtNode);
            }

            return Insert(rtNode, key);
        }

        private static RtNode<T> Join(RtNode<T> first, RtNode<T> second)
        {
            if (first == null)
                return second;
            if (second == null)
                return first;

            if (_randomizer.Next(first.Size + second.Size) < first.Size)
            {
                first.Right = Join(first.Right, second);
                FixSize(first);
                return first;
            }

            second.Left = Join(first, second.Left);
            FixSize(second);
            return second;
        }
    }

    internal class RtNode<T>
    {
        public readonly T Key;
        public int Size;
        public RtNode<T> Left;
        public RtNode<T> Right;

        public RtNode(T key)
        {
            Key = key;
            Size = 1;
        }

        public override string ToString()
        {
            return $"Key : {Key} : Size : {Size}";
        }
    }
}