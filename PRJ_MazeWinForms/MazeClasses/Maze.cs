// System Modules
using System;
using System.Windows.Forms;

// My Modules
using MazeConsole.MyDataStructures;
using PRJ_MazeConsole;

namespace MazeConsole
{
    public class Maze
    {
        private Graph _graph;
        private MyList<Node> _solution;
        public int Width { private set; get; }
        public int Height { private set; get; }

        public Maze(int width, int height, GenAlgorithm GenType)
        {
            Width = width;
            Height = height;
            _graph = new Graph(Width, Height);
            _solution = null;
            MazeGen.GenerateMaze(_graph, GenType);
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

        private MyList<Node> Solution 
        {
            get
            {
                if (_solution == null)
                {
                    _solution = MazeSolver.WallFollower(this);
                }
                return _solution;
            }
        }

        private bool GenerateMaze(GenAlgorithm GenType)
        {
            return MazeGen.GenerateMaze(_graph, GenType);
        }

        public string ConsoleDisplay(bool ShowSolution = false)
        {
            string display = "";
            if (ShowSolution)
            {
                _graph.GetConsoleDisplay(Solution);
            }
            else
            {
                display = _graph.GetConsoleDisplay();
            }
            return display;
        }

        public void FormsDisplay(TableLayoutPanel MazePanel)
        {
            _graph.GetFormsDisplay(MazePanel);
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
