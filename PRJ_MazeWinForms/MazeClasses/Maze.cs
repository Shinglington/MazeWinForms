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
            return MazeGen.GenerateMaze(_graph, GenType);
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
