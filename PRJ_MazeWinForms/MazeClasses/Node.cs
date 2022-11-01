namespace MazeConsole

{
    class Node
    {
        public (int, int) Location { get; private set; }

        public Node SouthNode { get; private set; }
        public Node EastNode { get; private set; }
        public bool Visited { get; private set; }
        public Node(int x, int y)
        {
            Location = (x, y);
            Visited = false;

            SouthNode = null;
            EastNode = null;
        }

        public void UpdateVisited(bool newValue)
        {
            Visited = newValue;
        }

        public bool UpdateSouthEdge(Node node)
        {
            bool success = false;
            if (node != null) // If node exists
            {
                if (node.Location.Item1 == Location.Item1) // If x coord is the same
                {
                    SouthNode = node;
                    success = true;
                }
            }
            else // If node doesnt exist (Updated to remove south edge)
            {
                SouthNode = null;
                success = true;
            }
            return success;
        }
        public bool UpdateEastEdge(Node node)
        {
            bool success = false;
            if (node != null) // If node exists
            {
                if (node.Location.Item2 == Location.Item2) // If y coord is the same
                {
                    EastNode = node;
                    success = true;
                }
            }
            else // If node doesnt exist (Updated to remove south edge)
            {
                EastNode = null;
                success = true;
            }
            return success;
        }
    }
}
