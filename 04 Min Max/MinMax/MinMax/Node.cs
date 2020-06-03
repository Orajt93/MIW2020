using System;
using DotNetGraph;
using System.Collections.Generic;
using DotNetGraph.Node;
using DotNetGraph.Edge;
using System.Drawing;

namespace ConsoleApp38
{
    public class Node
    {
        public int Value = 0;
        public string Player;
        public Node ParentNode;
        public Node LNode;
        public Node MNode;
        public Node RNode;
        public Node bestMove;
        public DotNode drawNode;
        public int? Wynik = null;
        public bool IsClosed = false;
        public int depth = 0;


        public static int nodeCount = 0;
        public static DotGraph graph = new DotGraph("MyGraph", true);

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
            if (this.Value < 17)
            {
                //Mid
                this.MNode = new Node() { ParentNode = this, Value = this.Value + 5, Player = temp };
            }
            if (this.Value < 16)
            {
                //Right
                this.RNode = new Node() { ParentNode = this, Value = this.Value + 6, Player = temp };
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
        public static void SetNodeValue(Node node)
        {
            if (node.Value == 21)
            {
                node.Wynik = 0;
                return;
            }
            if (node.Player == "ant")
                node.Wynik = 100 - node.depth;
            else
                node.Wynik = -100 + node.depth;

        }
        public static Node SetBestMove(Node node)
        {
            Node bestNode = node.LNode;
            if (node.LNode != null && node.LNode.Wynik == null)
            {
                SetNodeValue(node.LNode);
            }
            if (node.MNode != null && node.MNode.Wynik == null)
            {
                SetNodeValue(node.MNode);
            }
            if (node.RNode != null && node.RNode.Wynik == null)
            {
                SetNodeValue(node.RNode);
            }

            if (node.Player == "prot")
            {
                if (node.MNode != null && node.MNode.Wynik > bestNode.Wynik)
                {
                    bestNode = node.MNode;
                }
                if (node.RNode != null && node.RNode.Wynik > bestNode.Wynik)
                {
                    bestNode = node.RNode;
                }
            }
            else
            {
                if (node.MNode != null && node.MNode.Wynik < bestNode.Wynik)
                {
                    bestNode = node.MNode;
                }
                if (node.RNode != null && node.RNode.Wynik < bestNode.Wynik)
                {
                    bestNode = node.RNode;
                }
            }

            if (bestNode != null)
            {
                node.Wynik = bestNode.Wynik;
            }
            return bestNode;
        }
        public static void MinMaxAlgorithm(Node node)
        {
            {
                if (node.bestMove == null)
                {
                    if (node.LNode != null)
                    {
                        node.LNode.depth = node.depth + 1;
                        MinMaxAlgorithm(node.LNode);
                    }
                    if (node.MNode != null)
                    {
                        node.MNode.depth = node.depth + 1;
                        MinMaxAlgorithm(node.MNode);
                    }
                    if (node.RNode != null)
                    {
                        node.RNode.depth = node.depth + 1;
                        MinMaxAlgorithm(node.RNode);
                    }
                }
                if (node.ParentNode != null)
                {
                    node.bestMove = SetBestMove(node);
                }
            }
        }
        public static void ShowResult(Node node)
        {
            if (node.bestMove != null)
            {
                Console.WriteLine($"Jestem wierzcholkiem {node.Player} o wartosci {node.Value}");
                Console.WriteLine($"Wybieram ruch w {node.bestMove.Player} wartosc {node.bestMove.Value}");
                ShowResult(node.bestMove);
            }
        }

        public static void CreateDrawNode(Node node, string val)
        {
            string label = "";
            Color color;
            if (node.Value > 21)
            {
                label = $"Wygrywa {node.ParentNode.Player} \nWartosc: {node.Wynik}";
                color = (node.Player == "prot") ? Color.Blue : Color.DeepPink;
            }
            else if (node.Value == 21)
            {
                label = $"Remis \n Wartosc: {node.Wynik}";
                color = Color.Green;
            }
            else
            {
                label = $"{node.Player} {node.Value} \n W:{node.Wynik}";
                color = Color.Black;
            }
            node.drawNode = new DotNode($"Node{Node.nodeCount}") { Label = label, Color = color };
            nodeCount++;
            color = (node == node.ParentNode.bestMove) ? Color.Red : Color.Black;
            graph.Elements.Add(node.drawNode);
            graph.Elements.Add(new DotEdge(node.ParentNode.drawNode, node.drawNode)
            { Label = val, Color = color });

        }
        public static void CreateGraph(Node node)
        {
            if (node.LNode == null && node.ParentNode != null)
                CreateGraph(node.ParentNode);
            if (node.LNode != null && node.LNode.drawNode == null)
            {
                CreateDrawNode(node.LNode, 4.ToString());
                CreateGraph(node.LNode);
            }
            if (node.MNode != null && node.MNode.drawNode == null)
            {
                CreateDrawNode(node.MNode, 5.ToString());
                CreateGraph(node.MNode);
            }
            if (node.RNode != null && node.RNode.drawNode == null)
            {
                CreateDrawNode(node.RNode, 6.ToString());
                CreateGraph(node.RNode);
            }
        }
    }
}