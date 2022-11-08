using MazeConsole.MyDataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeConsole
{
    class Maze
    {
        // Constants
        private const char WALL_CHAR = '█';
        private const char SPACE_CHAR = ' ';

        private const char START_CHAR = 'S';
        private const char END_CHAR = 'E';
        private const char HIGHLIGHT_CHAR = '?';


        protected Graph _graph;
        protected MyList<Node> _solution;
        public int Width { private set; get; }
        public int Height { private set; get; }

        public Maze(int width, int height, string algorithm, bool showGeneration)
        {
            Width = width;
            Height = height;
            _graph = new Graph(Width, Height);
            MazeGen.GenerateMaze(this, _graph, algorithm, showGeneration);
            _solution = null;
        }

        public Maze(MazeSettings Settings)
        {
            Width = Settings.Width;
            Height = Settings.Height;
            _graph = new Graph(Width, Height);
            MazeGen.GenerateMaze(this, _graph, Settings.Algorithm, Settings.ShowGeneration);
            _solution = null;
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

        public MyList<Node> Solution
        {
            get
            {
                if (_solution == null)
                {
                    _solution = MazeSolver.WallFollower(_graph);
                }
                return _solution;
            }
        }


        public void DisplayConsole(Node CurrentNode = null, bool ShowSolution = false, bool ShowHint = false)
        {
            string Display = "";
            if (ShowSolution)
            {
                Display = GetStringDisplay(CurrentNode, Solution);
            }
            else if (ShowHint)
            {
                Display = GetStringDisplay(CurrentNode, GetHint(CurrentNode));
            }
            else
            {
                Display = GetStringDisplay(CurrentNode);
            }
            Console.WriteLine(Display);
        }

        private string GetStringDisplay(Node CurrentNode = null, MyList<Node> highlightNodes = null)
        {
            string mazeString = "";
            Node[,] nodes = _graph.GetNodes();
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
                    Node thisNode = nodes[x, y];
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


        protected MyList<Node> GetHint(Node CurrentNode)
        {
            return MazeSolver.WallFollower(_graph, CurrentNode);
        }

        public bool CheckAccessibility(Node A, Node B)
        {
            return _graph.AreConnected(A, B);
        }
    }



    public class MazeSettings
    {
        public int Width { get; }
        public int Height { get; }
        public GenAlgorithm Algorithm { get; }
        public bool ShowGeneration { get; }


        // Different constructors for difficulty parameters and advanced parameters
        public MazeSettings(int width, int height, GenAlgorithm algorithm, bool showGeneration = false)
        {
            // Advanced
            Width = width;
            Height = height;
            Algorithm = algorithm;
            ShowGeneration = showGeneration;
        }

        public MazeSettings(Difficulty difficulty)
        {
            ShowGeneration = false;
            switch (difficulty)
            {
                // Presets for difficulties
                case (Difficulty)0:
                    Width = 5;
                    Height = 5;
                    Algorithm = GenAlgorithm.Sidewinder;
                    break;
                case (Difficulty)1:
                    Width = 10;
                    Height = 10;
                    Algorithm = GenAlgorithm.BinaryTree;
                    break;
                case (Difficulty)2:
                    Width = 30;
                    Height = 30;
                    Algorithm = GenAlgorithm.GrowingTree;
                    break;
            }
        }

    }
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}