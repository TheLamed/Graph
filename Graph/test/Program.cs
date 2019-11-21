﻿using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string, int> g = new Graph<string, int>();
            g.EdgeValue = v => v;

            var a = g.AddVertex("a");
            var b = g.AddVertex("b");
            var c = g.AddVertex("c");
            var d = g.AddVertex("d");
            var e = g.AddVertex("e");
            var z = g.AddVertex("z");

            g.AddEdge(4, true, a, b);
            g.AddEdge(2, true, a, c);
            g.AddEdge(1, true, c, b);
            g.AddEdge(5, true, b, d);
            g.AddEdge(8, true, c, d);
            g.AddEdge(10, true, c, e);
            g.AddEdge(2, true, d, e);
            g.AddEdge(3, true, e, z);
            g.AddEdge(6, true, d, z);

            Console.WriteLine("Graph:");
            foreach (var item in g)
            {
                Console.WriteLine(item);
                foreach (var edge in item.OutputEdges)
                    Console.WriteLine(" " + edge);
            }

            Console.WriteLine();
            Console.WriteLine($"Shortest way from {a} to {z}:");

            var l1 = g.ShortWayDijkstra(a, z);

            foreach (var item in l1)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            var l2 = g.ShortWayFloid(a, z);

            foreach (var item in l2)
            {
                Console.Write(item + " ");
            }
        }
    }
}
