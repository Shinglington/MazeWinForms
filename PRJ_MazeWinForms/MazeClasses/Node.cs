namespace MazeClasses
{
    public class Node
    {
        // private attributes
        private Node[] _adjNodes;
        private bool _locked;
        // public attributes
        public NodeLocation Location { get; private set; }
        public Node(int x, int y)
        {
            Location = new NodeLocation(x, y);
            _adjNodes = new Node[] { null, null, null, null };
            _locked = false;
        }

        // Attributes with custom getters / setters
        public bool Locked
        {
            get { return _locked; }
            set { if (!_locked) _locked = value; }
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
            if (!_locked)
            {
                if (node == null)
                {
                    _adjNodes[(int)direction] = null;
                }
                else
                {
                    if (node.Location.X == node.Location.X &&
                        (direction == Direction.North || direction == Direction.South))
                    {
                        _adjNodes[(int)direction] = node;
                    }
                    else if (node.Location.Y == node.Location.Y &&
                        (direction == Direction.East || direction == Direction.West))
                    {
                        _adjNodes[(int)direction] = node;
                    }
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

        public static bool operator == (NodeLocation a, NodeLocation b)
        {
            if ((object)a == null) 
                return (object)b == null;
            return a.Equals(b);
        }

        public static bool operator != (NodeLocation a, NodeLocation b)
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