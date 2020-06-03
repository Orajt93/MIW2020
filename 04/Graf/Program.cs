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
            Node.graph.Elements.Add(firstNode.drawNode);
            Node.nodeCount++;

            //Lets go!
            Node.CreateTree(firstNode);

            //Create and compile graph
            var dot = Node.graph.Compile(true);
            File.WriteAllText("input.dot", dot);
            Console.WriteLine("finito");
        }
    }
    public class Node
    {
        public int Value = 0;
        public string Player;
        public Node ParentNode;
        public Node LNode;
        public Node MNode;
        public Node RNode;
        public DotNode drawNode;
        public bool IsClosed = false;


        public static DotGraph graph = new DotGraph("MyGraph", true);
        public static int nodeCount = 0;

        public bool CheckIsClosed()
        {
            if (this.LNode.Value > 21)
            {
                this.IsClosed = true;
                return true;
            }
            return false;
        }
        public void CreateNodes()
        {
            string temp = "prot";
            if (this.Player == "prot")
                temp = "ant";
            //Left side
            this.LNode = new Node() { ParentNode = this, Value = this.Value + 4, Player = temp };
            nodeCount++;
            var tempNode = this.LNode;
            if (this.Value < 18)
            {
                CreateDrawNode(tempNode, 4.ToString());
            }
            else
            {
                CreateDrawNode(tempNode, "loose");
                return;
            }
            //Mid
            this.MNode = new Node() { ParentNode = this, Value = this.Value + 5, Player = temp };
            nodeCount++;
            tempNode = this.MNode;
            if (this.Value < 17)
            {
                CreateDrawNode(tempNode, 5.ToString());
            }
            else
            {
                CreateDrawNode(tempNode, "loose");
                return;
            }
            //Right
            this.RNode = new Node() { ParentNode = this, Value = this.Value + 6, Player = temp };
            nodeCount++;
            tempNode = this.RNode;
            if (this.Value < 16)
            {
                CreateDrawNode(tempNode, 6.ToString());
            }
            else
            {
                CreateDrawNode(tempNode, "loose");
            }

        }
        public static void CreateTree(Node node)
        {
            if (node.Value < 21)
            {
                if (node.LNode == null)
                {
                    node.CreateNodes();
                    if (node.CheckIsClosed() == true && node.ParentNode != null)
                        CreateTree(node.ParentNode);
                }
                if (node.LNode.IsClosed == false)
                    CreateTree(node.LNode);
                if (node.MNode != null && node.MNode.IsClosed == false)
                    CreateTree(node.MNode);
                if (node.RNode != null && node.RNode.IsClosed == false)
                    CreateTree(node.RNode);
            }
            else
            {
                node.IsClosed = true;
                if (node.ParentNode != null)
                    CreateTree(node.ParentNode);
            }
        }
        public static void CreateDrawNode(Node node, string val)
        {
            nodeCount++;
            string label = $"{node.Player}{node.Value}";
            if (node.Value == 21)
                label = "Remis";
            if (val == "loose")
                label = $"{node.Player} porażka";

            node.drawNode = new DotNode($"Node{Node.nodeCount}")
            { Label = label };
            Node.graph.Elements.Add(node.drawNode);
            Node.graph.Elements.Add(new DotEdge(node.ParentNode.drawNode, node.drawNode) { Label = val });
        }

    }
}
