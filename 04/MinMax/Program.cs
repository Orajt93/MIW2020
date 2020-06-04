using System;
using System.Drawing;
using System.IO;
using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;

namespace ConsoleApp38
{
    class Program
    {
        static void Main(string[] args)
        {

            //Creating first node
            Node firstNode = new Node() { Player = "prot" };
            //Lets go!
            Node.CreateTree(firstNode);
            //Min-Max
            Node.MinMaxAlgorithm(firstNode);
            firstNode.bestMove = Node.SetBestMove(firstNode);
            //Best road
            Console.WriteLine("Zakonczono min max pokazuje droge: ");
            Node.ShowResult(firstNode);
            //Drawing the tree
            string label = $"{firstNode.Player} {firstNode.Value} \n W:{firstNode.Wynik}";
            firstNode.drawNode = new DotNode($"Node{Node.nodeCount}") { Label = label };
            Node.nodeCount++;
            Node.CreateGraph(firstNode);
            //Create and compile graph
            var dot = Node.graph.Compile(true);
            File.WriteAllText("input.dot", dot);
            Console.WriteLine("finito");

        }
        public class Node
        {
            //Aktualny wynik rozgrywki
            public int Value = 0;
            //Gracz
            public string Player;
            //Odniesienia do sasiadujacych wierzcholkow
            public Node ParentNode;
            public Node LNode;
            public Node MNode;
            public Node RNode;
            //Najlepszy ruch
            public Node bestMove;
            //Wierzcholek do zobrazowania
            public DotNode drawNode;
            //Wartosc ruchu
            public int? Wynik = null;
            //To do tworzenia grafu
            public bool IsClosed = false;
            //Glebokosc grafu
            public int depth = 0;

            //Liczba wierzcholkow zeby sie nie powtarzaly
            public static int nodeCount = 0;
            //Zobrazowany graf
            public static DotGraph graph = new DotGraph("MyGraph", true);
            
            //Spradzenie czy warto dalej robic dzieci
            public bool CheckIsClosed()
            {
                if (this.LNode.Value > 21)
                {
                    this.IsClosed = true;
                    return true;
                }
                return false;
            }
            //Tworzenei wierzcholkow
            public void CreateNodes()
            {
                //Wybor gracza
                string temp = "prot";
                if (this.Player == "prot")
                    temp = "ant";
                //Left side
                this.LNode = new Node() { ParentNode = this, Value = this.Value + 4, Player = temp };
                //Mid
                if (this.Value < 17)
                {
                    this.MNode = new Node() { ParentNode = this, Value = this.Value + 5, Player = temp };
                }
                //Right
                if (this.Value < 16)
                {
                    this.RNode = new Node() { ParentNode = this, Value = this.Value + 6, Player = temp };
                }

            }
            ///Tworzenie drzewa
            public static void CreateTree(Node node)
            {
                if (node.Value < 21)
                {
                    if (node.LNode == null)
                    {
                        //Funkcja tworzaca wierzcholki
                        node.CreateNodes();
                        //Skok do gory
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
            //Ustalenie wartosci ruchu
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
            //Wybranie najlepszego ruchu dla aktualnej pozycji
            public static Node SetBestMove(Node node)
            {
                Node bestNode = node.LNode;
                if (node.LNode != null && node.LNode.Wynik == null)
                {
                    //Ustalenie wartosci dla lewego itd..
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
                
                //Wybor najlepszego wierzcholka jezeli gracz prot to wybiera wierzcholek o wiekszej wartosci
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
                //Jezeli ant to wybiera ten o nizszej wartosci
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
                //Przypisanie najlepszej wartosci dla wierzcholka
                if (bestNode != null)
                {
                    node.Wynik = bestNode.Wynik;
                }
                //No i zwrocenie najlepszego ruchu
                return bestNode;
            }
            //Algorytm min-max
            public static void MinMaxAlgorithm(Node node)
            {
                {
                    if (node.bestMove == null)
                    {
                        //Wyowlanie na lewo srodek i dol
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
            //Wyswietlenie sciezki gdyby kazdy wybieral najlepszy ruch
            public static void ShowResult(Node node)
            {
                if (node.bestMove != null)
                {
                    Console.WriteLine($"Jestem wierzcholkiem {node.Player} o wartosci {node.Value}");
                    Console.WriteLine($"Wybieram ruch w {node.bestMove.Player} wartosc {node.bestMove.Value}");
                    ShowResult(node.bestMove);
                }
            }
            
            //Tworzenie wiercholka graficznego
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
            //Tworzenei calego grafu graficznego
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
}
