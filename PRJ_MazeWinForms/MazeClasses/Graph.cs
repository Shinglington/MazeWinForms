using System;
using MazeConsole.MyDataStructures;

namespace MazeConsole
{
    class Graph
    {

        // Constants
        private const char WALL_CHAR = '█';
        private const char SPACE_CHAR = ' ';

        private const char START_CHAR = 'S';
        private const char END_CHAR = 'E';
        private const char HIGHLIGHT_CHAR = '?';

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

        public string GetDisplay(Node CurrentNode = null, MyList<Node> highlightNodes = null)
        {
            string mazeString = "";
            mazeString += WALL_CHAR;
            // Generate top wall
            for (int x = 0; x < Width; x++)
            {
                mazeString += string.Format("{0}{1}", WALL_CHAR, WALL_CHAR);
            }
            mazeString += "\n";
            for (int y = 0; y < Height; y++)
            {

                string currEastWalls = "";
                string currSouthWalls = "";

                currEastWalls += WALL_CHAR;
                currSouthWalls += WALL_CHAR;
                // East edges
                for (int x = 0; x < Width; x++)
                {
                    Node thisNode = _nodes[x, y];
                    if (thisNode == CurrentNode)
                    {
                        currEastWalls += 'C';
                    }
                    else if (thisNode == StartNode)
                    {
                        currEastWalls += START_CHAR;
                    }
                    else if (thisNode == EndNode)
                    {
                        currEastWalls += END_CHAR;
                    }
                    else if (highlightNodes != null && highlightNodes.Contains(thisNode))
                    {
                        currEastWalls += HIGHLIGHT_CHAR;
                    }
                    else
                    {
                        currEastWalls += SPACE_CHAR;
                    }

                    // Check east edge
                    if (thisNode.EastNode != null)
                    {
                        currEastWalls += SPACE_CHAR;
                    }
                    else
                    {
                        currEastWalls += WALL_CHAR;
                    }
                    // Check south edge
                    if (thisNode.SouthNode != null)
                    {
                        currSouthWalls += SPACE_CHAR;
                    }
                    else
                    {
                        currSouthWalls += WALL_CHAR;
                    }
                    currSouthWalls += WALL_CHAR;
                }
                mazeString += currEastWalls;
                mazeString += "\n";
                mazeString += currSouthWalls;
                mazeString += "\n";
            }
            return mazeString;

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
            MyList<Node> PossibleAdjacentNodes = new MyList<Node>() { N.NorthNode, N.EastNode, N.SouthNode, N.WestNode };
            MyList<Node> AdjacentNodes = new MyList<Node>();
            foreach (Node node in PossibleAdjacentNodes)
            {
                if (node != null)
                {
                    AdjacentNodes.Add(node);
                }
            }
            return AdjacentNodes.ToArray();
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