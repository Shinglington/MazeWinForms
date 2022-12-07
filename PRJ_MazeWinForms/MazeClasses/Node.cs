namespace MazeClasses
{
    public class Node
    {
        // Stores information about individual "Node" in a graph


        // Adjacent nodes, in order North, East, South, West (as ordered in the Directions enum)   
        private Node[] _adjNodes;

        // public attributes
        public NodeLocation Location { get; private set; }
        public Node(int x, int y)
        {
            Location = new NodeLocation(x, y);
            _adjNodes = new Node[] { null, null, null, null };
        }

        // Adjacent Nodes
        public Node NorthNode
        {
            get { return _adjNodes[(int)Direction.North]; }
            set { UpdateEdge(value, Direction.North); }
        }
        public Node EastNode
        {
            get { return _adjNodes[(int)Direction.East]; }
            set { UpdateEdge(value, Direction.East); }
        }
        public Node SouthNode
        {
            get { return _adjNodes[(int)Direction.South]; }
            set { UpdateEdge(value, Direction.South); }
        }
        public Node WestNode
        {
            get { return _adjNodes[(int)Direction.West]; }
            set { UpdateEdge(value, Direction.West); }
        }

        private void UpdateEdge(Node node, Direction direction)
        {
            if (node == null)
                _adjNodes[(int)direction] = null;

            else
            {
                // If direction of edge is north or south, check that x coord is same
                if (node.Location.X == Location.X &&
                    (direction == Direction.North || direction == Direction.South))
                {
                    _adjNodes[(int)direction] = node;
                }
                // If direction of edge is east or west, check that y coord is same
                else if (node.Location.Y == Location.Y &&
                    (direction == Direction.East || direction == Direction.West))
                {
                    _adjNodes[(int)direction] = node;
                }
            }
        }
    }

    public class NodeLocation
    {
        private (int, int) _location;

        public NodeLocation(int x, int y)
        {
            _location = (x, y);
        }
        public int X { get { return _location.Item1; } }
        public int Y { get { return _location.Item2; } }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X.ToString(), Y.ToString());
        }

        public override bool Equals(object obj)
        {
            // override for equals
            if (obj is NodeLocation)
            {
                NodeLocation n = (NodeLocation)obj;
                return (n.X == X && n.Y == Y);
            }
            else
                return false;
        }

        public static bool operator ==(NodeLocation a, NodeLocation b)
        {
            if ((object)a == null)
                return b is null;
            return a.Equals(b);
        }

        public static bool operator !=(NodeLocation a, NodeLocation b)
        {
            return !(a == b);
        }


    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}