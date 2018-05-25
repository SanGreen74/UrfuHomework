using System;

namespace HomeWork.Structures
{
    public class AvlTree<T>
        where T : IComparable
    {
        public int Count { get; private set; }

        private AvlNode<T> _root;

        public bool Contains(T key)
        {
            var avlNode = _root;
            while (true)
            {
                if (avlNode == null) return false;
                if (avlNode.Key.CompareTo(key) == 0) return true;
                avlNode = key.CompareTo(avlNode.Key) < 0 ? avlNode.Left : avlNode.Right;
            }
        }

        public void Insert(T key)
        {
            _root = Insert(_root, key);
            Count++;
        }

        public void Remove(T key)
        {
            if (!Contains(key))
                return;
            _root = Remove(_root, key);
            Count--;
        }

        private static AvlNode<T> RotateRight(AvlNode<T> node)
        {
            var tmp = node.Left;
            if (tmp == null || tmp.Key.CompareTo(node.Key) == 0)
                return node;
            node.Left = tmp.Right;
            tmp.Right = node;
            FixHeight(node);
            FixHeight(tmp);
            return tmp;
        }

        private static AvlNode<T> RotateLeft(AvlNode<T> node)
        {
            var tmp = node.Right;
            if (tmp == null || tmp.Key.CompareTo(node.Key) == 0)
                return node;
            node.Right = tmp.Left;
            tmp.Left = node;
            FixHeight(node);
            FixHeight(tmp);
            return tmp;
        }


        private static AvlNode<T> Balance(AvlNode<T> node)
        {
            FixHeight(node);
            if (CalculateBalanceFactor(node) == 2)
            {
                if (CalculateBalanceFactor(node.Right) < 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            if (CalculateBalanceFactor(node) == -2)
            {
                if (CalculateBalanceFactor(node.Left) > 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            return node;
        }

        private static AvlNode<T> Insert(AvlNode<T> node, T key)
        {
            if (node == null)
                return new AvlNode<T>(key);
            if (key.CompareTo(node.Key) < 0)
                node.Left = Insert(node.Left, key);
            else
                node.Right = Insert(node.Right, key);
            return Balance(node);
        }

        private static AvlNode<T> FindMin(AvlNode<T> node)
        {
            if (node == null)
                return null;
            while (true)
            {
                if (node?.Left == null)
                    return node;
                node = node.Left;
            }
        }

        private static AvlNode<T> RemoveMin(AvlNode<T> node)
        {
            if (node.Left == null)
                return node.Right;
            node.Left = RemoveMin(node.Left);
            return Balance(node);
        }

        private static AvlNode<T> Remove(AvlNode<T> node, T key)
        {
            if (node == null)
                return null;
            if (key.CompareTo(node.Key) < 0)
                node.Left = Remove(node.Left, key);
            else if (key.CompareTo(node.Key) > 0)
                node.Right = Remove(node.Right, key);
            else
            {
                var nodeLeft = node.Left;
                var nodeRight = node.Right;
                if (nodeRight == null)
                    return nodeLeft;
                var min = FindMin(nodeRight);
                min.Right = RemoveMin(nodeRight);
                min.Left = nodeLeft;
                return Balance(min);
            }

            return Balance(node);
        }


        private static uint GetHeight(AvlNode<T> node)
        {
            return node == null ? 0 : node.Height;
        }

        private static int CalculateBalanceFactor(AvlNode<T> node)
        {
            return (int) (GetHeight(node.Right) - GetHeight(node.Left));
        }

        private static void FixHeight(AvlNode<T> node)
        {
            var heightLeft = GetHeight(node.Left);
            var heightRight = GetHeight(node.Right);
            node.Height = (heightLeft > heightRight ? heightLeft : heightRight) + 1;
        }
    }

    internal class AvlNode<T>
    {
        public readonly T Key;
        public uint Height;
        public AvlNode<T> Left;
        public AvlNode<T> Right;

        public AvlNode(T key)
        {
            Height = 1;
            Key = key;
        }

        public override string ToString()
        {
            return $"Key : {Key} : Height: {Height}";
        }
    }
}