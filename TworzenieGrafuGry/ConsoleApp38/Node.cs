using DotNetGraph.Edge;
using DotNetGraph.Node;
using System;

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
        public DotNode drawNode;

        public bool IsClosed = false;

        public static int nodeCount = 0;

        public bool CheckIsClosed()
        {
            if (this.LNode.Value > 21 && this.MNode.Value > 21 && this.RNode.Value > 21)
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
            this.LNode = new Node() { ParentNode = this, Value = this.Value + 4, Player=temp };
            var tempNode = this.LNode;
            CreateDrawNode(tempNode, 4);
            this.MNode = new Node() { ParentNode = this, Value = this.Value + 5, Player=temp };
            tempNode = this.MNode;
            CreateDrawNode(tempNode, 5);
            this.RNode = new Node() { ParentNode = this, Value = this.Value + 6, Player=temp };
            tempNode = this.RNode;
            CreateDrawNode(tempNode, 6);
            Console.WriteLine($"Dodaje wierzchołki do {this.Player} {this.Value}");
        }
        public static void CreateTree(Node node)
        {
            if (node.Value < 21)
            {
                if (node.LNode == null)
                {
                    node.CreateNodes();
                    if (node.CheckIsClosed() == true && node.ParentNode!=null)
                        CreateTree(node.ParentNode);
                }
                if (node.LNode.IsClosed == false)
                    CreateTree(node.LNode);
                if (node.MNode.IsClosed == false)
                    CreateTree(node.MNode);
                if (node.RNode.IsClosed == false)
                    CreateTree(node.RNode);
            }
            else
            {
                node.IsClosed = true;
                if (node.ParentNode != null)
                    CreateTree(node.ParentNode);
            }
        }
        public static void CreateDrawNode(Node node, int val)
        {
            nodeCount++;
            node.drawNode = new DotNode($"Node{Node.nodeCount}")
            { Label = $"{node.Player}{node.Value}" };
            StaticGraph.graph.Elements.Add(node.drawNode);
            StaticGraph.graph.Elements.Add(new DotEdge(node.ParentNode.drawNode, node.drawNode) {Label=val.ToString()});
        }
    }
}
