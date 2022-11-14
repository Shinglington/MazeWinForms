using System;
using MazeConsole.MyDataStructures;

namespace MazeConsole
{
    public class Graph
    {
        private readonly Node[,] _nodes;
        private bool _locked;
        public int Width { get; }
        public int Height { get; }
        public Node StartNode { get; }
        public Node EndNode { get; }
        public bool Locked
        {
            get { return _locked; }
            set
            {
                if (!_locked)
                {
                    _locked = value;
                    if (_locked)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            for (int y = 0; y < Width; y++)
                            {
                                _nodes[x, y].Locked = true;
                            }
                        }
                    }
                }
            }
        }

        public Graph(int width, int height)
        {
            _locked = false;
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
            // temporary start and end nodes
            StartNode = _nodes[0, 0];
            EndNode = _nodes[width - 1, height - 1];
        }

        public Node GetNodeFromLocation(NodeLocation Location)
        {
            return _nodes[Location.X, Location.Y];
        }

        public bool AddEdge(Node NodeA, Node NodeB)
        {
            bool success = false;
            // Check if they are adjacent
            if (!AreAdjacent(NodeA, NodeB))
            {
                Console.WriteLine("Nodes at ({0}, {1}) and ({2}, {3}) are not adjacent, so can't add edge", NodeA.Location.X, NodeA.Location.Y, NodeB.Location.X, NodeB.Location.Y);
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
                Console.WriteLine("Failed to connect edges of nodes ({0}, {1}) and ({2}, {3})", NodeA.Location.X, NodeA.Location.Y, NodeB.Location.X, NodeB.Location.Y);
            }

            return success;
        }

        public bool RemoveEdge(Node NodeA, Node NodeB)
        {
            bool success = false;
            // Check if they are adjacent
            if (!AreAdjacent(NodeA, NodeB)) return false;

            // If x coords are next to each other
            if (Math.Abs(NodeA.Location.X - NodeB.Location.X) == 1)
            {
                // If A has greater x coord than B, A is to the east of B
                if (NodeA.Location.Y > NodeB.Location.Y)
                {
                    NodeB.EastNode = null;
                    NodeA.WestNode = null;
                }
                // Otherwise, B is to the east of A
                else
                {
                    NodeA.EastNode = null;
                    NodeB.WestNode = null;
                }
            }
            // If y coords are next to each other
            else if (Math.Abs(NodeA.Location.Y - NodeB.Location.Y) == 1)
            {
                // If A has greater y coord than B, then A is south of B
                if (NodeA.Location.Y > NodeB.Location.Y)
                {
                    NodeB.SouthNode = null;
                    NodeA.NorthNode = null;
                }
                // Otherwise, B south of A
                else
                {
                    NodeA.NorthNode = null;
                    NodeB.SouthNode = null;
                }
            }
            return success;
        }

        public Node[,] GetNodes()
        {
            return _nodes;
        }

        public Node[] GetAdjacentNodes(Node N)
        {
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
            return new Node[] {N.NorthNode, N.EastNode, N.SouthNode, N.WestNode};
        }

        public bool AreAdjacent(Node NodeA, Node NodeB)
        {
            bool adjacent = false;
            if (NodeA == NodeB)
            {
                Console.WriteLine("Node A and B are the same Node");
                return adjacent;
            }
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
            // if they aren't adjacent, they can't be connected
            if (!AreAdjacent(NodeA, NodeB)) return false;
            if (NodeA.NorthNode == NodeB || NodeA.EastNode == NodeB || NodeA.SouthNode == NodeB || NodeA.WestNode == NodeB)
            {
                Connected = true;
            }
            return Connected;
        }
    }
}