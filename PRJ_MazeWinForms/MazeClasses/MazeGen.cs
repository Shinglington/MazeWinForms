﻿using MazeConsole.MyDataStructures;
using System;
using System.Collections.Generic;

namespace MazeConsole
{
    public enum GenAlgorithm
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
    class MazeGen
    {
        public static Random rand = new Random();
        public static bool GenerateMaze(Graph G, String Algorithm, bool ShowGeneration = false)
        {
            bool success = false;
            success = (bool)typeof(MazeGen).GetMethod(Algorithm).Invoke(null, new object[] { G, ShowGeneration });
            if (!success)
            {
                Console.WriteLine("Error in finding algorithm with name {0}", Algorithm);
            }

            return success;
        }
        public static bool GenerateMaze(Graph G, GenAlgorithm Algorithm, bool ShowGeneration = false)
        {
            return GenerateMaze(G, Algorithm.ToString(), ShowGeneration);
        }
        public static bool AldousBroder(Graph G, bool ShowGeneration)
        {
            bool success = true;
            VisitedNodesTracker visited = new VisitedNodesTracker(G);
            Node[,] nodes = G.GetNodes();
            Node currentNode = G.StartNode;
            visited.SetVisited(currentNode);
            Node nextNode;
            while (!visited.AllNodesVisited())
            {
                // Choose random adjacent node
                nextNode = GetRandomAdjacentNode(G, currentNode);
                if (!visited.CheckVisited(nextNode))
                {
                    G.AddEdge(currentNode, nextNode);
                    visited.SetVisited(nextNode);
                    if (ShowGeneration)
                    {
                        Console.WriteLine(G.GetDisplay());
                        Console.ReadLine();
                    }
                }
                currentNode = nextNode;
            }
            return success;
        }

        public static bool Wilsons(Graph G, bool ShowGeneration)
        {
            bool success = true;
            VisitedNodesTracker visited = new VisitedNodesTracker(G);
            Node[,] nodes = G.GetNodes();

            // Add random start node to Maze
            visited.SetVisited(visited.GetRandomUnvisitedNode());

            // Add graphs to maze until full
            while (!visited.AllNodesVisited())
            {
                Node walkStart = visited.GetRandomUnvisitedNode();
                // Track walk path, each dictionary entry linking 2 nodes together
                Dictionary<Node, Node> walkPath = new Dictionary<Node, Node>();

                Node CurrNode = walkStart;
                // Do "drunkards" walk until path between start and maze is found
                while (!visited.CheckVisited(CurrNode))
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
                    visited.SetVisited(CurrNode);
                    if (ShowGeneration)
                    {
                        Console.WriteLine(G.GetDisplay());
                        Console.ReadLine();
                    }
                }
            }
            return success;

        }

        public static bool BinaryTree(Graph G, bool ShowGeneration)
        {
            bool success = true;
            VisitedNodesTracker visited = new VisitedNodesTracker(G);
            Node[,] nodes = G.GetNodes();
            for (int y = 0; y < G.Height; y++)
            {
                for (int x = 0; x < G.Width; x++)
                {
                    Node n = nodes[x, y];
                    // if node isn't bottom right node
                    if ((x != G.Width - 1) || (y != G.Height - 1))
                    {
                        Console.WriteLine("{0}, {1}", x, y);
                        // if node is on right column, can't add east edge
                        if (x == G.Width - 1)
                        {
                            G.AddEdge(n, nodes[x, y + 1]);
                        }
                        // if node is on bottom row, can't add south edge
                        else if (y == G.Height - 1)
                        {
                            G.AddEdge(n, nodes[x + 1, y]);
                        }
                        // otherwise, randomly choose
                        else
                        {
                            if (rand.Next(2) == 1)
                            {
                                // add east edge
                                G.AddEdge(n, nodes[x + 1, y]);
                            }
                            else
                            {
                                // add south edge
                                G.AddEdge(n, nodes[x, y + 1]);
                            }
                        }
                    }

                    if (ShowGeneration)
                    {
                        Console.WriteLine(G.GetDisplay());
                        Console.ReadLine();
                    }
                }

            }
            return success;
        }

        public static bool Sidewinder(Graph G, bool ShowGeneration)
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
                        G.AddEdge(selectedNode, nodes[selectedNode.Location.X, selectedNode.Location.Y - 1]);
                        // clear path
                        path.Clear();
                    }
                    if (ShowGeneration)
                    {
                        Console.WriteLine(G.GetDisplay());
                        Console.ReadLine();
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
                    G.AddEdge(selectedNode, nodes[selectedNode.Location.X, selectedNode.Location.Y - 1]);
                }

            }
            return success;
        }

        public static bool Ellers(Graph G, bool ShowGeneration)
        {
            bool success = false;
            // tbc
            return success;
        }

        public static bool HuntAndKill(Graph G, bool ShowGeneration)
        {
            bool success = true;
            VisitedNodesTracker visited = new VisitedNodesTracker(G);
            Node[,] nodes = G.GetNodes();
            Node CurrentNode = visited.GetRandomUnvisitedNode();
            while (!visited.AllNodesVisited())
            {
                // Kill Mode
                visited.SetVisited(CurrentNode);
                Node NextNode = GetRandomAdjacentNode(G, CurrentNode, true, visited);
                if (NextNode != null)
                {
                    G.AddEdge(CurrentNode, NextNode);
                    CurrentNode = NextNode;
                    if (ShowGeneration)
                    {
                        Console.WriteLine(G.GetDisplay());
                        Console.ReadLine();
                    }
                }
                else
                {
                    // Hunt Mode
                    bool TargetFound = false;
                    Node[] UnvisitedNodes = visited.GetUnvisitedNodes();
                    foreach (Node N in UnvisitedNodes)
                    {
                        Node[] AdjNodes = G.GetAdjacentNodes(N);
                        foreach (Node Neighbour in AdjNodes)
                        {
                            if (visited.CheckVisited(Neighbour))
                            {
                                G.AddEdge(N, Neighbour);
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

        public static bool RecursiveBacktracker(Graph G, bool ShowGeneration)
        {
            bool success = true;
            VisitedNodesTracker visited = new VisitedNodesTracker(G);
            MyStack<Node> Path = new MyStack<Node>();
            Node CurrNode = visited.GetRandomUnvisitedNode();
            visited.SetVisited(CurrNode);

            while (!visited.AllNodesVisited())
            {
                Path.Push(CurrNode);
                Node NextNode = null;
                while (NextNode == null)
                {
                    CurrNode = Path.Peek();
                    NextNode = GetRandomAdjacentNode(G, CurrNode, true, visited);
                    if (NextNode == null)
                    {
                        Path.Pull();
                    }
                }
                G.AddEdge(CurrNode, NextNode);
                visited.SetVisited(NextNode);
                CurrNode = NextNode;
                if (ShowGeneration)
                {
                    Console.WriteLine(G.GetDisplay());
                    Console.ReadLine();
                }
            }
            return success;
        }

        public static bool RecursiveDivision(Graph G, bool ShowGeneration)
        {
            bool success = false;
            // tbc
            return success;
        }

        public static bool Kruskals(Graph G, bool ShowGeneration)
        {
            bool success = false;
            // tbc
            return success;
        }

        public static bool Prims(Graph G, bool ShowGeneration)
        {
            bool success = false;
            // tbc
            return success;
        }


        public static bool GrowingTree(Graph G, bool ShowGeneration)
        {
            bool success = true;
            VisitedNodesTracker visited = new VisitedNodesTracker(G);
            MyList<Node> ActiveSet = new MyList<Node>();
            Node SelectedNode = visited.GetRandomUnvisitedNode();
            ActiveSet.Add(SelectedNode);
            visited.SetVisited(SelectedNode);
            while (ActiveSet.Count > 0)
            {
                SelectedNode = ActiveSet[rand.Next(ActiveSet.Count)];
                // Can customize "way" that neighbour is selected
                Node Neighbour = GetRandomAdjacentNode(G, SelectedNode, true, visited);
                if (Neighbour != null)
                {
                    G.AddEdge(SelectedNode, Neighbour);
                    ActiveSet.Add(Neighbour);
                    visited.SetVisited(Neighbour);
                    if (ShowGeneration)
                    {
                        Console.WriteLine(G.GetDisplay());
                        Console.ReadLine();
                    }
                }
                else
                {
                    ActiveSet.Remove(SelectedNode);
                }
            }

            return success;
        }











        // Other commonly used functions in maze gen

        private static Node GetRandomAdjacentNode(Graph G, Node N, bool Unvisited = false, VisitedNodesTracker visited = null)
        {
            Node[] adjNodes = G.GetAdjacentNodes(N);
            if (Unvisited)
            {
                MyList<Node> unvisitedAdjNodes = new MyList<Node>();
                foreach (Node A in adjNodes)
                {
                    if (!visited.CheckVisited(A))
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

        private class VisitedNodesTracker
        {
            private Graph _graph;
            private bool[,] _visitedStatuses;
            public VisitedNodesTracker(Graph G)
            {
                _graph = G;
                _visitedStatuses = new bool[_graph.Width, _graph.Height];
                for (int x = 0; x < _graph.Width; x++)
                {
                    for (int y = 0; y < _graph.Height; y++)
                    {
                        _visitedStatuses[x, y] = false;
                    }
                }
            }

            public bool CheckVisited(Node N)
            {
                return _visitedStatuses[N.Location.X, N.Location.Y];
            }

            public void SetVisited(Node N)
            {
                _visitedStatuses[N.Location.X, N.Location.Y] = true;
            }

            public bool AllNodesVisited()
            {
                bool AllVisited = true;

                for (int x = 0; x < _graph.Width; x++)
                {
                    for (int y = 0; y < _graph.Height; y++)
                    {
                        if (!_visitedStatuses[x, y])
                        {
                            AllVisited = false;
                            return AllVisited;
                        }
                    }
                }
                return AllVisited;
            }

            public Node GetRandomUnvisitedNode()
            {

                Node[] UnvisitedNodes = GetUnvisitedNodes();
                return UnvisitedNodes[rand.Next(UnvisitedNodes.Length)];

            }


            public Node[] GetUnvisitedNodes()
            {
                MyList<Node> UnvisitedNodes = new MyList<Node>();
                for (int x = 0; x < _graph.Width; x++)
                {
                    for (int y = 0; y < _graph.Height; y++)
                    {
                        if (!_visitedStatuses[x, y])
                        {
                            UnvisitedNodes.Add(_graph.GetNodes()[x, y]);
                        }
                    }
                }
                return UnvisitedNodes.ToArray();
            }
        }



        public static bool TestAllAlgorithms(int x, int y, bool ShowGeneration = false)
        {
            bool success = true;
            foreach (GenAlgorithm algorithm in Enum.GetValues(typeof(GenAlgorithm)))
            {
                Console.WriteLine(algorithm.ToString());
                Maze M = new Maze(x, y, algorithm.ToString(), ShowGeneration);

                M.DisplayMaze();
                Console.WriteLine("\n\n\n");
                Console.ReadLine();
            }


            return success;
        }

    }
}