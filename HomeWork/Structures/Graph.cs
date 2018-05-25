using System.Collections.Generic;
using System.Linq;

namespace HomeWork.Structures
{
    public class Grap
    {
        public static IEnumerable<(Node node, int distance)> FindPaths(Node startNode)
        {
            var queue = new System.Collections.Generic.Queue<Node>();
            var visited = new HashSet<Node>();
            var result = new List<(Node, int)>();
            queue.Enqueue(startNode);
            var distance = 1;
            while (queue.Count != 0)
            {
                var nodes = queue.ToList();
                queue.Clear();
                foreach (var node in nodes)
                {
                    visited.Add(node);
                    foreach (var incidentNode in node.Edges.Where(x => !visited.Contains(x)))
                    {
                        queue.Enqueue(incidentNode);
                        result.Add((incidentNode, distance));
                    }
                }

                distance++;
            }

            return result;
        }

        public static IEnumerable<IEnumerable<Node>> FindConnectivityComponents(IList<Node> nodes)
        {
            var visited = new HashSet<Node>();
            var result = new List<List<Node>>();
            while (true)
            {
                var nextNode = nodes.FirstOrDefault(x => !visited.Contains(x));
                if (nextNode == null) break;
                var bs = BreadthSearch(nextNode).ToList();
                result.Add(bs);
                bs.ForEach(x => visited.Add(x));
            }

            return result;
        }

        public static IList<Node> FindCycles(Node startNode)
        {
            var visited = new HashSet<Node>();
            var queue = new System.Collections.Generic.Stack<Node>();
            queue.Push(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Peek();
                if (visited.Contains(node))
                {
                    var list = new List<Node> { node };
                    node = queue.Pop();
                    var nextNode = queue.Pop();
                    while (true)
                    {
                        list.Add(nextNode);
                        if (nextNode == node)
                            break;
                        while (true)
                        {
                            var tmp = queue.Pop();
                            if (tmp.Edges.Contains(nextNode))
                            {
                                nextNode = tmp;
                                break;
                            }
                        }
                    }

                    return list;
                }

                visited.Add(node);
                foreach (var incidentNode in node.Edges)
                    queue.Push(incidentNode);
            }

            return null;
        }

        public static IEnumerable<Node> BreadthSearch(Node startNode)
        {
            var visited = new HashSet<Node>();
            var queue = new System.Collections.Generic.Queue<Node>();
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (visited.Contains(node))
                    continue;
                visited.Add(node);
                yield return node;
                foreach (var incidentNode in node.Edges)
                    queue.Enqueue(incidentNode);
            }
        }

        public class Node
        {
            public readonly int Number;
            public IList<Node> Edges;

            public Node(int number)
            {
                Number = number;
                Edges = new List<Node>();
            }

            public void AddEdges(IEnumerable<Node> nodes)
            {
                foreach (var node in nodes)
                    Edges.Add(node);
            }

            public override string ToString()
            {
                return Number.ToString();
            }

            protected bool Equals(Node other)
            {
                return Number == other.Number;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Node)obj);
            }

            public override int GetHashCode()
            {
                return Number;
            }
        }
    }
}
