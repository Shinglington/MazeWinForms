using MazeConsole.MyDataStructures;
using System;
using System.Windows.Forms;

namespace MazeConsole
{
    class Graph
    {
        private const char WALL_CHAR = '█';
        private const char SPACE_CHAR = ' ';

        private const char START_CHAR = 'S';
        private const char END_CHAR = 'E';
        private const char HIGHLIGHT_CHAR = '?';

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
            // temporary start and end nodes
            StartNode = _nodes[0, 0];
            EndNode = _nodes[width - 1, height - 1];

        }
        public string GetConsoleDisplay(MyList<Node> highlightNodes = null)
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
                    if (StartNode.Location == (x, y))
                    {
                        currEastWalls += START_CHAR;
                    }
                    else if (EndNode.Location == (x, y))
                    {
                        currEastWalls += END_CHAR;
                    }
                    else if (highlightNodes != null && highlightNodes.Contains(_nodes[x, y]))
                    {
                        currEastWalls += HIGHLIGHT_CHAR;
                    }
                    else
                    {
                        currEastWalls += SPACE_CHAR;
                    }
                    // Check east edge
                    if (_nodes[x, y].EastNode != null)
                    {
                        currEastWalls += SPACE_CHAR;
                    }
                    else
                    {
                        currEastWalls += WALL_CHAR;
                    }
                    // Check south edge
                    if (_nodes[x, y].SouthNode != null)
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

        public void GetFormsDisplay(TableLayoutPanel GraphPanel)
        {
            GraphPanel.RowStyles.Clear();
            GraphPanel.ColumnStyles.Clear();
            for (int row = 0; row < Height; row++)
            {
                GraphPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Height));
            }
            for (int col = 0; col < Width; col++)
            {
                GraphPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Width));
            }
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Panel Cell = new Panel() { Parent = GraphPanel, Dock = DockStyle.Fill };
                    GraphPanel.SetCellPosition(Cell, new TableLayoutPanelCellPosition(col, row));
                    Cell.Paint += new PaintEventHandler(_nodes[col, row].PaintNode);
                }
            }

        }


        // Check if all nodes have been visited
        public bool AllNodesVisited()
        {
            bool allVisited = true;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (_nodes[x, y].Visited == false)
                    {
                        allVisited = false;
                        return allVisited;
                    }
                }
            }
            return allVisited;
        }

        // Get list of unvisited nodes
        public Node[] GetUnvisitedNodes()
        {
            MyList<Node> unvisitedNodes = new MyList<Node>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (_nodes[x, y].Visited == false)
                    {
                        unvisitedNodes.Add(_nodes[x, y]);
                    }
                }
            }
            return unvisitedNodes.ToArray();
        }

        // Reset visited attribute in nodes
        public void ResetVisited()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _nodes[x, y].UpdateVisited(false);
                }
            }
        }

        // Add edge between two nodes
        public bool AddEdge(Node NodeA, Node NodeB)
        {
            bool success = false;
            if (!AreAdjacent(NodeA, NodeB)) return false;
            // If x coords are next to each other
            if (Math.Abs(NodeA.Location.Item1 - NodeB.Location.Item1) == 1)
            {
                // If A has greater x coord than B, A is to the east of B
                if (NodeA.Location.Item1 > NodeB.Location.Item1)
                {
                    success = NodeB.UpdateEastEdge(NodeA) && NodeA.UpdateWestEdge(NodeB);
                }
                // Otherwise, B is to the east of A
                else
                {
                    success = NodeA.UpdateEastEdge(NodeB) && NodeB.UpdateWestEdge(NodeA);
                }
            }
            // If y coords are next to each other
            else if (Math.Abs(NodeA.Location.Item2 - NodeB.Location.Item2) == 1)
            {
                // If A has greater y coord than B, then A is south of B
                if (NodeA.Location.Item2 > NodeB.Location.Item2)
                {
                    success = NodeB.UpdateSouthEdge(NodeA) && NodeA.UpdateNorthEdge(NodeB);
                }
                // Otherwise, B south of A
                else
                {
                    success = NodeA.UpdateSouthEdge(NodeB) && NodeB.UpdateNorthEdge(NodeA);
                }
            }
            return success;
        }

        // Remove edge between two nodes
        public bool RemoveEdge(Node NodeA, Node NodeB)
        {
            bool success = false;
            // Check if they are adjacent
            if (!AreAdjacent(NodeA, NodeB)) return false;

            // If x coords are next to each other
            if (Math.Abs(NodeA.Location.Item1 - NodeB.Location.Item1) == 1)
            {
                // If A has greater x coord than B, A is to the east of B
                if (NodeA.Location.Item2 > NodeB.Location.Item2)
                {
                    success = NodeB.UpdateEastEdge(null) && NodeA.UpdateWestEdge(null);
                }
                // Otherwise, B is to the east of A
                else
                {
                    success = NodeA.UpdateEastEdge(null) && NodeB.UpdateWestEdge(null);
                }
            }
            // If y coords are next to each other
            else if (Math.Abs(NodeA.Location.Item2 - NodeB.Location.Item2) == 1)
            {
                // If A has greater y coord than B, then A is south of B
                if (NodeA.Location.Item2 > NodeB.Location.Item2)
                {
                    success = NodeB.UpdateSouthEdge(null) && NodeA.UpdateNorthEdge(null);
                }
                // Otherwise, B south of A
                else
                {
                    success = NodeA.UpdateSouthEdge(null) && NodeB.UpdateNorthEdge(null);
                }
            }
            return success;
        }

        public Node[,] GetNodes()
        {
            return _nodes;
        }

        // Get adjacent nodes to Node N
        public Node[] GetAdjacentNodes(Node N)
        {
            MyList<Node> AdjacentNodes = new MyList<Node>();
            // Get east and west nodes
            for (int xOffset = -1; xOffset <= 1; xOffset += 2)
            {
                int newX = N.Location.Item1 + xOffset;
                if (newX >= 0 && newX < Width)
                {
                    AdjacentNodes.Add(_nodes[newX, N.Location.Item2]);
                }
            }
            // Get north and south nodes
            for (int yOffset = -1; yOffset <= 1; yOffset += 2)
            {
                int newY = N.Location.Item2 + yOffset;
                if (newY >= 0 && newY < Height)
                {
                    AdjacentNodes.Add(_nodes[N.Location.Item1, newY]);
                }
            }
            return AdjacentNodes.ToArray();
        }

        // Check if given nodes are adjacent
        public bool AreAdjacent(Node NodeA, Node NodeB)
        {
            bool adjacent = false;
            (int, int) LocA = NodeA.Location;
            (int, int) LocB = NodeB.Location;
            // if in same location, return false
            if (LocA == LocB) return false;

            // if  neither coords are the same, return false
            if ((LocA.Item1 != LocB.Item1) && (LocA.Item2 != LocB.Item2)) return false;

            // if difference between x or y coord is 1, they are adjacent
            if (Math.Abs(LocA.Item1 - LocB.Item1) == 1) adjacent = true;
            else if (Math.Abs(LocA.Item2 - LocB.Item2) == 1) adjacent = true;

            return adjacent;
        }

        // Check if given nodes have route between them
        public bool AreConnected(Node NodeA, Node NodeB)
        {
            bool Connected = false;
            // if they aren't adjacent, they can't be connected
            if (!AreAdjacent(NodeA, NodeB)) return false;

            (int, int) LocA = NodeA.Location;
            (int, int) LocB = NodeB.Location;
            // if x coord same, must be adjacent by y coord
            if (LocA.Item1 == LocB.Item1)
            {
                // if NodeA y is greater, A is the south node of B
                if (LocA.Item2 > LocB.Item2)
                {
                    if (NodeB.SouthNode == NodeA) Connected = true;
                }
                else
                {
                    if (NodeA.SouthNode == NodeB) Connected = true;
                }

            }
            // Otherwise y coord same (since they are adjacent), so adjacent by x coord
            else
            {
                if (LocA.Item1 > LocB.Item1)
                {
                    if (NodeB.EastNode == NodeA) Connected = true;
                }
                else
                {
                    if (NodeA.EastNode == NodeB) Connected = true;
                }
            }

            return Connected;
        }

    }
}
