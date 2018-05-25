using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using HomeWork.Structures;

namespace HomeWork
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lab = Labyrint.Generate();
            LabyrintPathFinder.VizualizePath(lab, new Point(3, 1), new Point(3, 15), 2);
            Console.ReadLine();
        }
    }
}