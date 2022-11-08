using System;
using MazeConsole.MyDataStructures;

namespace MazeConsole
{
    class MazeSolver
    {
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
        public static MyList<Node> WallFollower(Graph G, Node Start = null, Node End = null)
        {
            MyStack<Node> Path = new MyStack<Node>();
            if (Start == null)
            {
                Start = G.StartNode;
            }
            if (End == null)
            {
                End = G.EndNode;
            }
            DepthFirstSearch(G, Start, Path, End);
            return Path.ToList();
        }

        private static void DepthFirstSearch(Graph G, Node CurrNode, MyStack<Node> Path, Node Target)
        {
            // Identify the previous node, to remove from child nodes later
            Node PrevNode = null;
            if (Path.Count > 0)
            {
                PrevNode = Path.Peek();
            }

            Path.Push(CurrNode);
            // If the current node is the target, break the recursion loop
            if (CurrNode == Target) return;

            // find and add children nodes
            Node[] ChildNodes = G.GetConnectedNodes(CurrNode);
            foreach (Node NextNode in ChildNodes)
            {
                if (NextNode != PrevNode)
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
            if (N is null)
            {
                return null;
            }
            Node[] AccessibleNodes = G.GetConnectedNodes(N);
            return AccessibleNodes[rand.Next(AccessibleNodes.Length)];
        }

    }
}