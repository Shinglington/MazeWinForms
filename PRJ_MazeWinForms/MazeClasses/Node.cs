using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace MazeConsole
{
    class Node
    {
        public (int, int) Location { get; private set; }

        private Node[] _adjNodes;
        public Node NorthNode 
        {
            get { return _adjNodes[(int)Direction.North]; }
            private set { _adjNodes[(int)Direction.North] = value;  }
        }
        public Node EastNode
        {
            get { return _adjNodes[(int)Direction.East]; }
            private set { _adjNodes[(int)Direction.East] = value; }
        }
        public Node SouthNode
        {
            get { return _adjNodes[(int)Direction.South]; }
            private set { _adjNodes[(int)Direction.South] = value; }
        }
        public Node WestNode
        {
            get { return _adjNodes[(int)Direction.West]; }
            private set { _adjNodes[(int)Direction.West] = value; }
        }

        public bool Visited { get; private set; }
        public Node(int x, int y)
        {
            Location = (x, y);
            Visited = false;

            _adjNodes = new Node[4] {null, null, null, null};
        }

        public void UpdateVisited(bool newValue)
        {
            Visited = newValue;
        }
        public bool UpdateNorthEdge(Node node)
        {
            return UpdateEdge(node, Direction.North);
        }
        public bool UpdateEastEdge(Node node)
        {
            return UpdateEdge(node, Direction.East);
        }
        public bool UpdateSouthEdge(Node node)
        {
            return UpdateEdge(node, Direction.South);
        }
        public bool UpdateWestEdge(Node node)
        {
            return UpdateEdge(node, Direction.West);
        }
        private bool UpdateEdge(Node node, Direction direction)
        {
            bool success = false;
            if (node == null)
            {
                _adjNodes[(int)direction] = null;
                success = true;
            }
            else
            {
                if (node.Location.Item1 == node.Location.Item1 && 
                    (direction == Direction.North || direction == Direction.South))
                {
                    _adjNodes[(int)direction] = node;
                    success = true;
                }
                else if (node.Location.Item2 == node.Location.Item2 &&
                    (direction == Direction.East || direction == Direction.West))
                {
                    _adjNodes[(int)direction] = node;
                    success = true;
                }
            }

            return success;
        }

        enum Direction
        {
            North,
            East,
            South,
            West
        }
    }
}
