using System.Collections.Generic;
using System.Linq;

namespace HomeWork.Structures
{
    public class Graph
    {
        public static IEnumerable<(GraphNode node, int distance)> FindPaths(GraphNode startGraphNode)
        {
            var queue = new System.Collections.Generic.Queue<GraphNode>();
            var visited = new HashSet<GraphNode>();
            var result = new List<(GraphNode, int)>();
            queue.Enqueue(startGraphNode);
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

        public static IEnumerable<IEnumerable<GraphNode>> FindConnectivityComponents(IList<GraphNode> nodes)
        {
            var visited = new HashSet<GraphNode>();
            var result = new List<List<GraphNode>>();
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

        public static IEnumerable<GraphNode> FindCycles(GraphNode startGraphNode)
        {
            var visited = new HashSet<GraphNode>();
            var nodesStack = new System.Collections.Generic.Stack<GraphNode>();
            var ways = new Dictionary<GraphNode, GraphNode>();
            nodesStack.Push(startGraphNode);
            while (nodesStack.Count != 0)
            {
                var currentNode = nodesStack.Pop();
                visited.Add(currentNode);
                foreach (var node in currentNode.Edges)
                {
                    if (visited.Contains(node))
                        return BuildPath(ways, currentNode, node);
                    ways[node] = currentNode;
                    nodesStack.Push(node);
                }
            }

            return null;
        }

        private static IEnumerable<GraphNode> BuildPath(
            Dictionary<GraphNode, GraphNode> ways,
            GraphNode startNode, 
            GraphNode endNode)
        {
            var cyclePath = new List<GraphNode>();
            var nextNode = startNode;
            while (ways.ContainsKey(nextNode) && !nextNode.Equals(endNode))
            {
                cyclePath.Add(nextNode);
                nextNode = ways[nextNode];
            }
            cyclePath.Add(nextNode);
            cyclePath.Reverse();
            return cyclePath;
        }

        public static IEnumerable<GraphNode> BreadthSearch(GraphNode startGraphNode)
        {
            var visited = new HashSet<GraphNode>();
            var queue = new System.Collections.Generic.Queue<GraphNode>();
            queue.Enqueue(startGraphNode);
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

    }

    public class GraphNode
    {
        private readonly int Number;
        private readonly IList<GraphNode> _edges;
        public IEnumerable<GraphNode> Edges => _edges;

        public GraphNode(int number)
        {
            Number = number;
            _edges = new List<GraphNode>();
        }

        public void AddEdges(IEnumerable<GraphNode> nodes)
        {
            foreach (var node in nodes)
                _edges.Add(node);
        }

        public void AddEdge(GraphNode node)
        {
            _edges.Add(node);
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        protected bool Equals(GraphNode other)
        {
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GraphNode)obj);
        }

        public override int GetHashCode()
        {
            return Number;
        }
    }
}

//var node = queue.Peek();
//if (visited.Contains(node))
//{
//    var list = new List<GraphNode> { node };
//    node = queue.Pop();
//    var nextNode = queue.Pop();
//    while (true)
//    {
//        list.Add(nextNode);
//        if (nextNode.Equals(node))
//            break;
//        while (true)
//        {
//            var tmp = queue.Pop();
//            if (tmp.Edges.Contains(nextNode))
//            {
//                nextNode = tmp;
//                break;
//            }
//        }
//    }

//    return list;
//}

//visited.Add(node);
//foreach (var incidentNode in node.Edges)
//    queue.Push(incidentNode);