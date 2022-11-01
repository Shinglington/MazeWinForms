using MazeConsole.MyDataStructures;

namespace MazeConsole
{
    class Maze
    {
        private Graph _graph;
        public int Width { private set; get; }
        public int Height { private set; get; }

        public Maze(int width, int height, string GenType)
        {
            Width = width;
            Height = height;
            _graph = new Graph(Width, Height);
            GenerateMaze(GenType);
        }

        public Node StartNode
        {
            get
            {
                return _graph.StartNode;
            }
        }

        public Node EndNode
        {
            get
            {
                return _graph.EndNode;
            }
        }

        public string DisplayMaze(MyList<Node> highlightNodes = null)
        {
            return _graph.GetDisplay(highlightNodes);
        }

        private bool GenerateMaze(string GenType)
        {
            bool success = false;

            switch (GenType)
            {
                case "AldousBroder":
                    success = MazeGen.AldousBroder(_graph);
                    break;
                case "Wilsons":
                    success = MazeGen.Wilsons(_graph);
                    break;
                case "BinaryTree":
                    success = MazeGen.BinaryTree(_graph);
                    break;
                case "Sidewinder":
                    success = MazeGen.Sidewinder(_graph);
                    break;
                case "HuntAndKill":
                    success = MazeGen.HuntAndKill(_graph);
                    break;
                case "RecursiveBacktracker":
                    success = MazeGen.RecursiveBacktracker(_graph);
                    break;
                case "GrowingTree":
                    success = MazeGen.GrowingTree(_graph);
                    break;
                default:
                    success = false;
                    break;
            }
            return success;
        }

        // Reset visited attribute in nodes
        public void ResetVisited()
        {
            _graph.ResetVisited();
        }

        // Get the nodes accessible to N (i.e. ones with edges connected to N)
        public Node[] GetAccessibleNodes(Node N)
        {
            if (N is null)
            {
                return new Node[0];
            }
            MyList<Node> AccessibleNodes = new MyList<Node>();
            Node[] AdjNodes = _graph.GetAdjacentNodes(N);

            foreach (Node neighbour in AdjNodes)
            {
                if (_graph.AreConnected(N, neighbour))
                {
                    AccessibleNodes.Add(neighbour);
                }
            }
            return AccessibleNodes.ToArray();

        }
    }
}
