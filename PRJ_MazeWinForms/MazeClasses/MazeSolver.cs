using System;
using MazeConsole;
using MazeConsole.MyDataStructures;
namespace PRJ_MazeConsole
{
    class MazeSolver
    {
        public static Random rand = new Random();

        // "Random Mouse Solver" returns list of Nodes which are part of solution
        public static MyList<Node> RandomMouse(Maze M)
        {
            MyStack<Node> Path = new MyStack<Node>();
            Node EndNode = M.EndNode;
            Path.Push(M.StartNode);
            while (Path.Peek() != EndNode)
            {
                // Randomly traverse maze
                Node CurrentNode = Path.Pull();
                Node nextNode = GetRandomAccessibleNode(M, CurrentNode);

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
        public static MyList<Node> WallFollower(Maze M)
        {
            // Reset visited attribute for re-use in the depth-first search
            M.ResetVisited();
            MyStack<Node> Path = new MyStack<Node>();
            DepthFirstSearch(M, M.StartNode, Path, M.EndNode);
            return Path.ToList();
        }

        private static void DepthFirstSearch(Maze M, Node CurrNode, MyStack<Node> Path, Node Target)
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
            Node[] ChildNodes = M.GetAccessibleNodes(CurrNode);
            foreach (Node NextNode in ChildNodes)
            {
                if (NextNode != PrevNode)
                {
                    DepthFirstSearch(M, NextNode, Path, Target);
                    if (Path.Peek() == Target) return;
                }
            }
            Path.Pull();
        }

        // Commonly used functions
        private static Node GetRandomAccessibleNode(Maze M, Node N)
        {
            if (N is null)
            {
                return null;
            }
            Node[] AccessibleNodes = M.GetAccessibleNodes(N);
            return AccessibleNodes[rand.Next(AccessibleNodes.Length)];
        }

    }
}
