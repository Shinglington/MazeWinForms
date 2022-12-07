using MyDataStructures;
using System;

namespace MazeClasses
{
    class MazeSolver
    {
        // Stores solving algorithms


        public static Random rand = new Random();

        // "Random Mouse Solver" returns list of Nodes which are part of solution
        public static MyList<Node> RandomMouse(Graph G)
        {
            MyStack<Node> Path = new MyStack<Node>();
            Node EndNode = G.EndNode;
            Path.Push(G.StartNode);
            while (Path.Peek() != EndNode)
            {
                // Randomly traverse maze
                Node CurrentNode = Path.Pull();
                Node nextNode = GetRandomAccessibleNode(G, CurrentNode);

                if (Path.Count == 0)
                {
                    Path.Push(CurrentNode);
                    Path.Push(nextNode);
                }
                else if (nextNode != Path.Peek())
                {
                    Path.Push(CurrentNode);
                    Path.Push(nextNode);
                }
            }
            return Path.ToList();
        }

        // "Wall follower", a depth-first search returning solution
        public static MyList<Node> WallFollower(Graph G, NodeLocation StartLocation = null, NodeLocation EndLocation = null)
        {
            MyStack<Node> Path = new MyStack<Node>();
            if (StartLocation == null)
                StartLocation = G.StartNode.Location;
            if (EndLocation == null)
                EndLocation = G.EndNode.Location;
            // Do a depth first search starting from the start location, ending at the end location
            DepthFirstSearch(G, G.GetNodeFromLocation(StartLocation), Path, G.GetNodeFromLocation(EndLocation));
            return Path.ToList();
        }

        private static void DepthFirstSearch(Graph G, Node CurrNode, MyStack<Node> Path, Node Target)
        {
            // Identify the previous node, to remove from child nodes later
            // this is so algorithm doesn't go back on itself
            Node PrevNode = null;
            if (Path.Count > 0)
                PrevNode = Path.Peek();

            Path.Push(CurrNode);
            // If the current node is the target, break the recursion loop
            if (CurrNode == Target) return;

            // find and add children nodes
            Node[] ChildNodes = G.GetConnectedNodes(CurrNode);
            foreach (Node NextNode in ChildNodes)
            {
                if (NextNode != PrevNode && NextNode != null)
                {
                    DepthFirstSearch(G, NextNode, Path, Target);
                    if (Path.Peek() == Target) return;
                }
            }
            Path.Pull();
        }

        // Commonly used functions
        private static Node GetRandomAccessibleNode(Graph G, Node N)
        {
            // Get a random connected node to specified node
            if (N is null)
                return null;

            Node[] ConnectedNodes = G.GetConnectedNodes(N);
            MyList<Node> AccessibleNodes = new MyList<Node>();
            foreach (Node n in ConnectedNodes)
                if (N != null)
                    AccessibleNodes.Add(N);

            return AccessibleNodes[rand.Next(AccessibleNodes.Count)];
        }

    }
}