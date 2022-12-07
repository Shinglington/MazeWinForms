using MyDataStructures;
using PRJ_MazeWinForms.Logging;
using System;

namespace MazeClasses
{
    public class Graph
    {

        // Stores information about a rectangular "graph"
        // Used in Maze class



        // readonly since nodes should only be changed via UpdateEdge method
        private readonly Node[,] _nodes;
        public int Width { get; }
        public int Height { get; }
        public Node StartNode { get; }
        public Node EndNode { get; }

        public Graph(int width, int height)
        {
            _nodes = new Node[width, height];
            this.Width = width;
            this.Height = height;

            // Add nodes to 2d array
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _nodes[x, y] = new Node(x, y);
                }
            }
            // Start node in top left, end node at bottom right
            StartNode = _nodes[0, 0];
            EndNode = _nodes[width - 1, height - 1];
        }

        public Node GetNodeFromLocation(NodeLocation Location)
        {
            if (Location.X >= 0 && Location.X < Width && Location.Y >= 0 && Location.Y < Height)
                return _nodes[Location.X, Location.Y];
            return null;
        }

        public bool AddEdge(Node NodeA, Node NodeB)
        {
            bool success = false;
            // Check if they are adjacent
            if (!AreAdjacent(NodeA, NodeB))
            {
                LogHelper.ErrorLog(string.Format("Failed to connect edges at nodes {0} and {1}, nodes aren't adjacent", NodeA.Location, NodeB.Location));
                return success;
            }

            // If x coords are next to each other
            if (Math.Abs(NodeA.Location.X - NodeB.Location.X) == 1)
            {
                // If A has greater x coord than B, A is to the east of B
                if (NodeA.Location.X > NodeB.Location.X)
                {
                    NodeB.EastNode = NodeA;
                    NodeA.WestNode = NodeB;
                }
                // Otherwise, B is to the east of A
                else
                {
                    NodeA.EastNode = NodeB;
                    NodeB.WestNode = NodeA;
                }
            }
            // If y coords are next to each other
            else if (Math.Abs(NodeA.Location.Y - NodeB.Location.Y) == 1)
            {
                // If A has greater y coord than B, then A is south of B
                if (NodeA.Location.Y > NodeB.Location.Y)
                {
                    NodeB.SouthNode = NodeA;
                    NodeA.NorthNode = NodeB;
                }
                // Otherwise, B south of A
                else
                {
                    NodeA.SouthNode = NodeB;
                    NodeB.NorthNode = NodeA;
                }
            }
            else
            {
                LogHelper.ErrorLog(string.Format("Failed to connect edges at nodes {0} and {1}", NodeA.Location, NodeB.Location));
            }

            return success;
        }

        public Node[,] GetNodes()
        {
            // Return clone of nodes array, so it can't be edited directly
            Node[,] clone = (Node[,])_nodes.Clone();
            return clone;

        }

        public Node[] GetAdjacentNodes(Node N)
        {
            // Returns adjacent nodes (regardless of whether there is a wall between them or not)
            MyList<Node> AdjacentNodes = new MyList<Node>();
            // Get east and west nodes
            for (int xOffset = -1; xOffset <= 1; xOffset += 2)
            {
                int newX = N.Location.X + xOffset;
                if (newX >= 0 && newX < Width)
                {
                    AdjacentNodes.Add(_nodes[newX, N.Location.Y]);
                }
            }
            // Get north and south nodes
            for (int yOffset = -1; yOffset <= 1; yOffset += 2)
            {
                int newY = N.Location.Y + yOffset;
                if (newY >= 0 && newY < Height)
                {
                    AdjacentNodes.Add(_nodes[N.Location.X, newY]);
                }
            }
            return AdjacentNodes.ToArray();
        }

        public Node[] GetConnectedNodes(Node N)
        {
            // Returns "connected nodes", nodes that are adjacent without a wall between
            return new Node[] { N.NorthNode, N.EastNode, N.SouthNode, N.WestNode };
        }

        public bool AreAdjacent(Node NodeA, Node NodeB)
        {
            bool adjacent = false;
            if (NodeA == NodeB)
                return adjacent;
            if ((NodeA.Location.X == NodeB.Location.X) || (NodeA.Location.Y == NodeB.Location.Y))
            {
                // if difference between x or y coord is 1, they are adjacent
                if (Math.Abs(NodeA.Location.X - NodeB.Location.X) == 1) adjacent = true;
                else if (Math.Abs(NodeA.Location.Y - NodeB.Location.Y) == 1) adjacent = true;
            }
            return adjacent;
        }


        public bool AreConnected(Node NodeA, Node NodeB)
        {
            bool Connected = false;
            if (NodeA == null || NodeB == null) return false;
            if (!AreAdjacent(NodeA, NodeB)) return false;
            if (NodeA.NorthNode == NodeB || NodeA.EastNode == NodeB || NodeA.SouthNode == NodeB || NodeA.WestNode == NodeB)
                Connected = true;
            return Connected;
        }
    }
}