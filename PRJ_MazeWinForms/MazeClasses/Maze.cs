using MazeConsole.MyDataStructures;
using PRJ_MazeWinForms.MazeClasses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeConsole
{
    public class Maze
    {
        // Constants
        private const char WALL_CHAR = '█';
        private const char SPACE_CHAR = ' ';

        private const char START_CHAR = 'S';
        private const char END_CHAR = 'E';
        private const char HIGHLIGHT_CHAR = '?';


        protected Graph _graph;
        protected Player _player;
        protected MazeDisplayer _mazeDisplayer;

        protected MyList<Node> _solution;
        public int Width { private set; get; }
        public int Height { private set; get; }

        public Maze(int width, int height, GenAlgorithm algorithm, bool showGeneration)
        {
            MazeSettings Settings = new MazeSettings(Width, Height, algorithm, showGeneration);
            SetupMaze(Settings);
        }

        public Maze(MazeSettings Settings)
        {
            SetupMaze(Settings);
        }

        public void SetupMaze(MazeSettings Settings)
        {
            Width = Settings.Width;
            Height = Settings.Height;
            _graph = new Graph(Width, Height);
            MazeGen.GenerateMaze(this, _graph, Settings.Algorithm, Settings.ShowGeneration);
            _solution = null;
        }


        public NodeLocation StartNodeLocation { get { return _graph.StartNode.Location; } }
        public NodeLocation EndNodeLocation { get { return _graph.EndNode.Location; } }
        public NodeLocation PlayerLocation { get { return _player.Location; } }
      
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

        public void Display()
        {

        }

        public void DisplayConsole(bool ShowSolution = false, bool ShowHint = false)
        {
            string Display = "";
            if (ShowSolution)
            {
                Display = GetStringDisplay(Solution);
            }
            else if (ShowHint)
            {
                Display = GetStringDisplay(GetHint());
            }
            else
            {
                Display = GetStringDisplay();
            }
            Console.WriteLine(Display);
        }

        private string GetStringDisplay(MyList<Node> highlightNodes = null)
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
                    NodeLocation ThisNodeLocation = new NodeLocation(x, y);
                    bool[] Walls = GetWalls(ThisNodeLocation);

                    if (ThisNodeLocation == PlayerLocation)
                    {
                        currEastWalls += 'C';
                    }
                    else if (ThisNodeLocation == StartNodeLocation)
                    {
                        currEastWalls += START_CHAR;
                    }
                    else if (ThisNodeLocation == EndNodeLocation)
                    {
                        currEastWalls += END_CHAR;
                    }

                    else if (highlightNodes != null && highlightNodes.Contains(_graph.GetNodeFromLocation(ThisNodeLocation)))
                    {
                        currEastWalls += HIGHLIGHT_CHAR;
                    }
                    else
                    {
                        currEastWalls += SPACE_CHAR;
                    }

                    // Check east edge
                    if (Walls[(int)Direction.East])
                        currEastWalls += WALL_CHAR;
                    else
                        currEastWalls += SPACE_CHAR;

                    // Check south edge
                    if (Walls[(int)Direction.South])
                        currSouthWalls += WALL_CHAR;
                    else
                        currSouthWalls += SPACE_CHAR;

                    currSouthWalls += WALL_CHAR;
                }
                mazeString += currEastWalls;
                mazeString += "\n";
                mazeString += currSouthWalls;
                mazeString += "\n";
            }
            return mazeString;
        }


        protected MyList<Node> GetHint(int count = 4)
        {
            MyList<Node> PathToEnd = MazeSolver.WallFollower(_graph, PlayerLocation, EndNodeLocation);
            MyList<Node> hint = new MyList<Node>();
            for (int i = 0; i < Math.Min(count, PathToEnd.Count); i++)
            {
                hint.Add(PathToEnd[i]);
            }
            return hint;
        }
        public bool CheckAccessibility(NodeLocation A, NodeLocation B)
        {
            return _graph.AreConnected(_graph.GetNodeFromLocation(A), _graph.GetNodeFromLocation(B));
        }

        public bool[] GetWalls(NodeLocation coords)
        {
            bool[] Walls = new bool[4] { false, false, false, false }; 
            Node[] ConnectedNodes = _graph.GetConnectedNodes(_graph.GetNodes()[coords.X, coords.Y]);
            for(int i = 0; i < 4; i++)
            {
                if (ConnectedNodes[i] == null)
                {
                    Walls[i] = true;
                }
            }
            return Walls;
        }
    }

    public class ConsoleMazeDisplayer : IMazeDisplayer
    {
        protected Maze _maze;
        public ConsoleMazeDisplayer()
        {
            
        }

        public void DisplayMaze()
        {

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
                    Width = 10;
                    Height = 10;
                    Algorithm = GenAlgorithm.Sidewinder;
                    break;
                case (Difficulty)1:
                    Width = 25;
                    Height = 25;
                    Algorithm = GenAlgorithm.BinaryTree;
                    break;
                case (Difficulty)2:
                    Width = 50;
                    Height = 50;
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