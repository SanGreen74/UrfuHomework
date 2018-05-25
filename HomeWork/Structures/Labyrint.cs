using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork.Structures
{
    public class Labyrint
    {
        private CellState[,] labirynt;

        public int Width => labirynt.GetLength(0);

        public int Height => labirynt.GetLength(1);

        public bool IsWall(int x, int y)
        {
            return labirynt[x, y] == CellState.Wall;
        }

        public bool IsWall(Point point)
        {
            return IsWall(point.X, point.Y);
        }

        public CellState this[int x, int y]
        {
            get { return labirynt[x, y]; }
            private set { labirynt[x, y] = value; }
        }

        public static Labyrint Generate()
        {
            var labirynt = new Labyrint { labirynt = new CellState[ExampleLabirynt.Length, ExampleLabirynt[0].Length] };
            for (var x = 0; x < ExampleLabirynt.Length; x++)
            {
                for (var y = 0; y < ExampleLabirynt[0].Length; y++)
                {
                    labirynt[x, y] = ExampleLabirynt[x][y] == '#' ? CellState.Wall : CellState.Empty;
                }
            }
            return labirynt;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var x = 0; x < labirynt.GetLength(0); x++)
            {
                for (var y = 0; y < labirynt.GetLength(1); y++)
                    sb.Append(labirynt[x, y] == CellState.Empty ? " " : "#");
                sb.AppendLine();

            }

            return sb.ToString();
        }

        private static readonly string[] ExampleLabirynt =
        {
            "####################",
            "#       #   #      #",
            "#     # # # #      #",
            "#     #   #        #",
            "####################"
        };
    }

    public enum CellState
    {
        Wall,
        Empty
    }
}
