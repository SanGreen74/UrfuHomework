using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace HomeWork.Structures
{
    public static class LabyrintPathFinder
    {
        public static void VizualizePath(
            Labyrint labyrint,
            Point from,
            Point to,
            int bombCount)
        {
            if (!BelongsToLabyrint(labyrint, from) || !BelongsToLabyrint(labyrint, from))
                throw new ArgumentException("Point doesn't belongs to labirynt");
            var path = GetPath(labyrint, from, to, bombCount);
            if (path is null)
            {
                Console.WriteLine("No ways");
                return;
            }
            Console.WriteLine(labyrint);
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var point in path)
            {
                Thread.Sleep(700);
                Console.SetCursorPosition(point.Y, point.X);
                Console.Write("*");
            }
        }

        private static IEnumerable<Point> GetPath(Labyrint labyrint,
            Point from,
            Point to,
            int bombCount)
        {
            var visited = new HashSet<Point>();
            var queue = new System.Collections.Generic.Queue<State>();
            var ways = new Dictionary<Point, Point>();
            queue.Enqueue(new State(from, null, bombCount));
            while (queue.Count != 0)
            {
                var currentState = queue.Dequeue();
                visited.Add(currentState.Point);
                var neighbours = GetNeighbours(currentState.Point)
                    .Where(x => !visited.Contains(x) && BelongsToLabyrint(labyrint, x));
                foreach (var point in neighbours)
                {
                    ways[point] = currentState.Point;
                    if (!labyrint.IsWall(point))
                        queue.Enqueue(new State(point, currentState, currentState.BombCount));
                    else if (currentState.BombCount > 0)
                        queue.Enqueue(new State(point, currentState, currentState.BombCount - 1));
                    if (point.Equals(to))
                    {
                        return BuildPath(currentState);
                    }
                }
            }

            return null;
        }

        private static IEnumerable<Point> BuildPath(State lastState)
        {
            var result = new List<Point>();
            var previuosState = lastState;
            while (true)
            {
                result.Add(previuosState.Point);
                if (previuosState.Previuos == null)
                    break;
                previuosState = previuosState.Previuos;
            }

            result.Reverse();
            return result;
        }

        private static bool BelongsToLabyrint(Labyrint labyrint, Point p)
        {
            return 0 <= p.X && p.X < labyrint.Width && 0 <= p.Y && p.Y < labyrint.Height;
        }

        private static IEnumerable<Point> GetNeighbours(Point p)
        {
            var result = new List<Point>();
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx + dy) != 0 && Math.Abs(dx * dy) == 0)
                        result.Add(new Point(p.X + dx, p.Y + dy));
                }
            }

            return result;
        }

        private class State
        {
            public Point Point;
            public State Previuos;
            public int BombCount;

            public State(Point point, State from, int bombCount)
            {
                Point = point;
                Previuos = from;
                BombCount = bombCount;
            }
        }
    }
}
