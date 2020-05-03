using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ConsoleApp38
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating first node
            Node firstNode = new Node() { Player = "prot" };
            firstNode.drawNode = new DotNode($"Node{Node.nodeCount}") 
             { Label = $"{firstNode.Player}{firstNode.Value}" };
            StaticGraph.graph.Elements.Add(firstNode.drawNode);
            Node.nodeCount++;

            //Lets go!
            Node.CreateTree(firstNode);

            Console.ReadKey();

            //Create and compile graph
            var dot = StaticGraph.graph.Compile(true);
            File.WriteAllText("input.dot", dot);
            Console.WriteLine("finito");   
        }
    }
}
