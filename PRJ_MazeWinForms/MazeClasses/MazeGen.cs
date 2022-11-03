using MazeConsole.MyDataStructures;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MazeConsole
{
    class MazeGen
    {
        public static Random rand = new Random();

        public static bool GenerateMaze(Graph _graph, GenAlgorithms Algorithm)
        {

            bool success = false;
            try
            {
                success = (bool) typeof(MazeGen).GetMethod(Algorithm.ToString()).Invoke(null, new object[] { _graph });
            }
            catch
            {
                Console.WriteLine("Error in finding algorithm with name {0}", Algorithm);
            }
            return success;
        }

        public enum GenAlgorithms
        {
            AldousBroder,
            Wilsons,
            BinaryTree,
            Sidewinder,
            Ellers,
            HuntAndKill,
            RecursiveBacktracker,
            RecursiveDivision,
            Kruskals,
            Prims,
            GrowingTree
        }


        public static bool AldousBroder(Graph G)
        {
            bool success = true;
            Node[,] nodes = G.GetNodes();

            (int, int) currLocation = G.StartNode.Location;
            (int, int) nextLocation;
            while (!G.AllNodesVisited())
            {
                // Choose random adjacent node
                nextLocation = GetRandomAdjacentNode(G, G.GetNodes()[currLocation.Item1, currLocation.Item2]).Location;
                if (nodes[nextLocation.Item1, nextLocation.Item2].Visited == false)
                {
                    success = G.AddEdge(nodes[currLocation.Item1, currLocation.Item2], nodes[nextLocation.Item1, nextLocation.Item2]);
                    nodes[nextLocation.Item1, nextLocation.Item2].UpdateVisited(true);

                    if (success == false)
                    {
                        return success;
                    }
                }
                currLocation = nextLocation;
            }
            return success;
        }

        public static bool Wilsons(Graph G)
        {
            bool success = true;
            Node[,] nodes = G.GetNodes();

            // Add random start node to Maze
            GetRandomUnvisitedNode(G).UpdateVisited(true);

            // Add graphs to maze until full
            while (!G.AllNodesVisited())
            {
                Node walkStart = GetRandomUnvisitedNode(G);
                // Track walk path, each dictionary entry linking 2 nodes together
                Dictionary<Node, Node> walkPath = new Dictionary<Node, Node>();

                Node CurrNode = walkStart;
                // Do "drunkards" walk until path between start and maze is found
                while (!CurrNode.Visited)
                {
                    Node NextNode = GetRandomAdjacentNode(G, CurrNode);
                    if (!walkPath.ContainsKey(NextNode))
                    {
                        walkPath.Add(NextNode, CurrNode);
                    }
                    CurrNode = NextNode;
                }
                // When path is found, trace path
                while (CurrNode != walkStart)
                {
                    G.AddEdge(CurrNode, walkPath[CurrNode]);
                    CurrNode = walkPath[CurrNode];
                    CurrNode.UpdateVisited(true);
                }
            }
            return success;

        }

        public static bool BinaryTree(Graph G)
        {
            bool success = true;
            Node[,] nodes = G.GetNodes();
            foreach (Node n in nodes)
            {
                (int, int) location = n.Location;
                // if node isn't bottom right node
                if (location != (G.Width - 1, G.Height - 1))
                {
                    // if node is on right column, can't add east edge
                    if (location.Item1 == G.Width - 1)
                    {
                        G.AddEdge(n, nodes[location.Item1, location.Item2 + 1]);
                    }
                    // if node is on bottom row, can't add south edge
                    else if (location.Item2 == G.Height - 1)
                    {
                        G.AddEdge(n, nodes[location.Item1 + 1, location.Item2]);
                    }
                    // otherwise, randomly choose
                    else
                    {
                        if (rand.Next(2) == 1)
                        {
                            // add east edge
                            G.AddEdge(n, nodes[location.Item1, location.Item2 + 1]);
                        }
                        else
                        {
                            // add south edge
                            G.AddEdge(n, nodes[location.Item1 + 1, location.Item2]);
                        }
                    }
                }
                n.UpdateVisited(true);

            }
            return success;
        }

        public static bool Sidewinder(Graph G)
        {
            bool success = true;
            Node[,] nodes = G.GetNodes();
            // loop through each row in graph
            for (int row = 0; row < G.Height; row++)
            {
                MyList<Node> path = new MyList<Node>();
                Node selectedNode;
                Node currNode;
                // loop through nodes in column (excluding last one, since can't add east edge to last node)
                for (int col = 0; col < G.Width - 1; col++)
                {
                    currNode = nodes[col, row];
                    if (path.Count == 0)
                    {
                        path.Add(currNode);
                    }

                    // randomly choose to add edge to east
                    if (row == 0 || rand.Next(2) == 0)
                    {
                        selectedNode = nodes[col + 1, row];
                        G.AddEdge(currNode, selectedNode);
                        path.Add(selectedNode);
                    }
                    // else, add north node to random node in path
                    else
                    {
                        selectedNode = path[rand.Next(path.Count)];
                        // Add edge between selectedNode and north node
                        G.AddEdge(selectedNode, nodes[selectedNode.Location.Item1, selectedNode.Location.Item2 - 1]);
                        // clear path
                        path.Clear();
                    }
                }

                //       handle final node in column        //
                if (row != 0)
                {
                    selectedNode = nodes[G.Width - 1, row]; // select final node by default
                                                            // If path isn't empty, choose random node in path (plus final node)
                    if (path.Count != 0)
                    {
                        path.Add(selectedNode);
                        selectedNode = path[rand.Next(path.Count)];
                    }
                    // Add edge between selected node and north node
                    G.AddEdge(selectedNode, nodes[selectedNode.Location.Item1, selectedNode.Location.Item2 - 1]);
                }

            }
            return success;
        }

        public static bool Ellers(Graph G)
        {
            bool success = false;
            // tbc
            return success;
        }

        public static bool HuntAndKill(Graph G)
        {
            bool success = true;
            Node[,] nodes = G.GetNodes();
            Node CurrentNode = GetRandomUnvisitedNode(G);
            while (!G.AllNodesVisited())
            {
                // Kill Mode
                CurrentNode.UpdateVisited(true);
                Node NextNode = GetRandomAdjacentNode(G, CurrentNode, true);
                if (NextNode != null)
                {
                    G.AddEdge(CurrentNode, NextNode);
                    CurrentNode = NextNode;
                }
                else
                {
                    // Hunt Mode
                    bool TargetFound = false;
                    Node[] UnvisitedNodes = G.GetUnvisitedNodes();
                    foreach (Node N in UnvisitedNodes)
                    {
                        Node[] AdjNodes = G.GetAdjacentNodes(N);
                        foreach (Node Neighbour in AdjNodes)
                        {
                            if (Neighbour.Visited)
                            {
                                G.AddEdge(CurrentNode, Neighbour);
                                CurrentNode = Neighbour;
                                TargetFound = true;
                                break;
                            }
                        }
                        if (TargetFound)
                        {
                            break;
                        }
                    }
                }
            }
            return success;
        }

        public static bool RecursiveBacktracker(Graph G)
        {
            bool success = true;
            MyStack<Node> Path = new MyStack<Node>();
            Node CurrNode = GetRandomUnvisitedNode(G);
            CurrNode.UpdateVisited(true);

            while (!G.AllNodesVisited())
            {
                Console.Write(G.GetConsoleDisplay(new MyList<Node>() { CurrNode }));
                Console.ReadLine();
                Path.Push(CurrNode);
                Node NextNode = null;
                while (NextNode == null)
                {
                    CurrNode = Path.Peek();
                    NextNode = GetRandomAdjacentNode(G, CurrNode, true);
                    if (NextNode == null)
                    {
                        Path.Pull();
                    }
                }
                G.AddEdge(CurrNode, NextNode);
                NextNode.UpdateVisited(true);
                CurrNode = NextNode;
            }
            return success;
        }

        public static bool RecursiveDivision(Graph G)
        {
            bool success = false;
            // tbc
            return success;
        }

        public static bool Kruskals(Graph G)
        {
            bool success = false;
            // tbc
            return success;
        }

        public static bool Prims(Graph G)
        {
            bool success = false;
            // tbc
            return success;
        }


        public static bool GrowingTree(Graph G)
        {
            bool success = true;
            MyList<Node> ActiveSet = new MyList<Node>();
            Node SelectedNode = GetRandomUnvisitedNode(G);
            ActiveSet.Add(SelectedNode);
            SelectedNode.UpdateVisited(true);
            while (ActiveSet.Count > 0)
            {
                SelectedNode = ActiveSet[rand.Next(ActiveSet.Count)];
                // Can customize "way" that neighbour is selected
                Node Neighbour = GetRandomAdjacentNode(G, SelectedNode, true);
                if (Neighbour != null)
                {
                    G.AddEdge(SelectedNode, Neighbour);
                    ActiveSet.Add(Neighbour);
                    Neighbour.UpdateVisited(true);
                }
                else
                {
                    ActiveSet.Remove(SelectedNode);
                }
            }

            return success;
        }











        // Other commonly used functions in maze gen
        private static Node GetRandomUnvisitedNode(Graph G)
        {
            Node[] UnvisitedNodes = G.GetUnvisitedNodes();
            return UnvisitedNodes[rand.Next(UnvisitedNodes.Length)];
        }

        private static Node GetRandomAdjacentNode(Graph G, Node N, bool Unvisited = false)
        {
            Node[] adjNodes = G.GetAdjacentNodes(N);
            if (Unvisited)
            {
                MyList<Node> unvisitedAdjNodes = new MyList<Node>();
                foreach (Node A in adjNodes)
                {
                    if (!A.Visited)
                    {
                        unvisitedAdjNodes.Add(A);
                    }
                }
                adjNodes = unvisitedAdjNodes.ToArray();
            }
            if (adjNodes.Length == 0)
            {
                return null;
            }
            return adjNodes[rand.Next(adjNodes.Length)];

        }
    }
}
