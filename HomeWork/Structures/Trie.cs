using System;
using System.Collections.Generic;
using Stack = System.Collections.Stack;

namespace HomeWork.Structures
{
    public class Trie
    {
        public TrieNode Root;

        public void Add(string key)
        {
            if (Root == null)
                Root = new TrieNode();

            var node = Root;
            foreach (var letter in key)
            {
                if (!node.ContainsEdgeWithKey(letter))
                    node.AddEdge(letter);
                node = node.Next(letter);
            }
            node.IsTerminal = true;
        }

        public bool Exists(string key)
        {
            if (Root == null)
                return false;
            var node = Root;
            for (var i = 0; i < key.Length; i++)
            {
                if (!node.ContainsEdgeWithKey(key[i]))
                    return false;
                node = node.Next(key[i]);
            }
            return node.IsTerminal;
        }

        public IEnumerable<string> GetByPrefix(string prefix)
        {
            var result = new List<string>();
            if (Root == null)
                return result;
            var startNode = Root;
            foreach (var letter in prefix)
            {
                if (!startNode.ContainsEdgeWithKey(letter))
                    return result;
                startNode = startNode.Next(letter);
            }
            Traverse(result, startNode);
            return result;
        }

        public void RemoveKey(string key)
        {
            if (!Exists(key))
                return;
            var route = new Stack<TrieNode>();
            var node = Root;
            foreach (var letter in key)
            {
                route.Push(node);
                node = node.Next(letter);
            }
            node.IsTerminal = false;
            foreach (var letter in key)
            {
                var parentNode = route.Pop();
                if (parentNode.Next(key[route.Count]).IsTerminal ||
                    parentNode.Next(key[route.Count]).Edges.Count > 0)
                    return;
                parentNode.Edges.Remove(key[route.Count]);
            }
        }

        private void Traverse(ICollection<string> list, TrieNode startTrieNode)
        {
            foreach (var startNodeEdge in startTrieNode.Edges)
            {
                if (startNodeEdge.Value.IsTerminal)
                    list.Add(startNodeEdge.Value.Content);
                Traverse(list, startNodeEdge.Value);
            }
        }
    }

    public class TrieNode
    {
        public IDictionary<char, TrieNode> Edges { get; }
        public string Content;
        public bool IsTerminal;

        public TrieNode Next(char key)
        {
            return Edges[key];
        }

        public bool ContainsEdgeWithKey(char key)
        {
            return Edges.ContainsKey(key);
        }

        public void AddEdge(char key)
        {
            if (Edges.ContainsKey(key))
                throw new ArgumentException("//todo");
            Edges[key] = new TrieNode
            {
                Content = Content + key,
            };
        }

        public TrieNode()
        {
            Edges = new Dictionary<char, TrieNode>();
        }
    }
}