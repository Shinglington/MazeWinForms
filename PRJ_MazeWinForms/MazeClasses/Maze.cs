using MazeConsole.MyDataStructures;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MazeConsole
{
    public class Maze
    { 

        protected Graph _graph;
        protected Player _player;
        protected IMazeDisplayer _mazeDisplayer;
        protected IMazeInterface _mazeInterface;

        protected MyList<Node> _solution;
        public int Width { private set; get; }
        public int Height { private set; get; }

        public Maze(int width, int height, GenAlgorithm algorithm, bool showGeneration)
        {
            MazeSettings Settings = new MazeSettings(width, height, algorithm, showGeneration);
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
            _mazeDisplayer = new ConsoleMazeDisplayer(this);
        }


        public NodeLocation StartNodeLocation { get { return _graph.StartNode.Location; } }
        public NodeLocation EndNodeLocation { get { return _graph.EndNode.Location; } }
        public NodeLocation PlayerLocation { get { return _player.Location; } }
      
        public MyList<NodeLocation> Solution
        {
            get
            {
                if (_solution == null)
                {
                    _solution = MazeSolver.WallFollower(_graph);
                }
                MyList<NodeLocation> SolutionLocations = new MyList<NodeLocation>();
                foreach (Node n in _solution)
                {
                    SolutionLocations.Add(n.Location);
                }
                return SolutionLocations;
            }
        }

        public virtual void Display(bool ShowSolution = false, bool ShowHint = false)
        {
            if (ShowSolution)
            {
                _mazeDisplayer.DisplaySolution();
            }
            else if (ShowHint)
            {
                _mazeDisplayer.DisplaySolution();
            }
            else
            {
                _mazeDisplayer.DisplayMaze();
            }
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
        // Constants
        private const char WALL_CHAR = '█';
        private const char SPACE_CHAR = ' ';
        private const char START_CHAR = 'S';
        private const char END_CHAR = 'E';
        private const char HIGHLIGHT_CHAR = '?';

        private Maze _maze;
        public ConsoleMazeDisplayer(Maze Maze)
        {
            _maze = Maze;
        }

        public void DisplayMaze()
        {
            string MazeString = GetStringDisplay();
            Console.WriteLine(MazeString);
        }

        public void DisplaySolution()
        {
            string MazeString = GetStringDisplay();
            Console.WriteLine(MazeString);
        }

        private string GetStringDisplay(MyList<NodeLocation> highlightNodes = null)
        {
            {
                string mazeString = "";
                mazeString += WALL_CHAR;
                // Generate top wall
                for (int x = 0; x < _maze.Width; x++)
                {
                    mazeString += string.Format("{0}{1}", WALL_CHAR, WALL_CHAR);
                }
                mazeString += "\n";
                for (int y = 0; y < _maze.Height; y++)
                {
                    string currEastWalls = "" + WALL_CHAR;
                    string currSouthWalls = "" + WALL_CHAR;

                    for (int x = 0; x < _maze.Width; x++)
                    {
                        NodeLocation ThisNodeLocation = new NodeLocation(x, y);
                        bool[] Walls = _maze.GetWalls(ThisNodeLocation);

                        if (ThisNodeLocation == _maze.PlayerLocation)
                            currEastWalls += 'C';

                        else if (ThisNodeLocation == _maze.StartNodeLocation)
                            currEastWalls += START_CHAR;

                        else if (ThisNodeLocation == _maze.EndNodeLocation)
                            currEastWalls += END_CHAR;

                        else if (highlightNodes != null && highlightNodes.Contains(ThisNodeLocation))
                            currEastWalls += HIGHLIGHT_CHAR;
                        else
                            currEastWalls += SPACE_CHAR;

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
        }

        public enum SolutionVisibility
        {
            None,
            Partial,
            Full
        }
    }

    public class ConsoleMazeInterface : IMazeInterface
    {

        
        public void SetupControls(char[] movementKeys)
        {
            throw new NotImplementedException();
        }

        public bool TryMove(Direction moveDirection)
        {
            throw new NotImplementedException();
        }

        class ConsoleMazeInterface
        {
            // CONTROLS
            private readonly Char[] MOVE_CONTROLS = { 'w', 'd', 's', 'a' };
            private readonly char HINT_CONTROL = 'h';

            private Maze _maze;
            private Player _player;

            public ConsoleMazeInterface()
            {
                _maze = new Maze(GetSettings());
                _player = new Player(_maze);
                Play();

                _maze.Display(true);
                Console.ReadLine();

            }

            public MazeSettings GetSettings()
            {
                int width = GetIntegerInput("Enter Width");
                int height = GetIntegerInput("Enter Height");
                GenAlgorithm algorithm = GetGenAlgorithm();
                return new MazeSettings(width, height, algorithm);
            }

            private void Play()
            {
                _maze.Display();
                while (_player.Location != _maze.EndNodeLocation)
                {
                    PlayerTurn();
                }
            }

            private void PlayerTurn()
            {
                char key = GetKeyInput();
                if (key == HINT_CONTROL)
                {
                    ShowHint();
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (MOVE_CONTROLS[i] == key)
                        {
                            MakeMove((Direction)i);
                            break;
                        }
                    }
                }
            }

            private void ShowHint()
            {
                _maze.Display(false, true);
            }
            /*

            private void MakeMove(Direction direction)
            {
                NodeLocation NextLocation = null;
                switch (direction)
                {
                    case Direction.North:
                        NextLocation = new NodeLocation()
                        break;
                    case Direction.East:
                        NextLocation = _player.LocationCurrentNode.EastNode;
                        break;
                    case Direction.South:
                        NextLocation = _player.CurrentNode.SouthNode;
                        break;
                    case Direction.West:
                        NextLocation = _player.CurrentNode.WestNode;
                        break;
                }
                if (NextLocation != null)
                {
                    _player.Move(NextLocation);
                    _maze.DisplayConsole(_player.CurrentNode);
                }
            }
            */
            private char GetKeyInput()
            {
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    char key = cki.KeyChar;
                    if (HINT_CONTROL == key || MOVE_CONTROLS.Contains(key))
                    {
                        return key;
                    }
                }
            }
            private int GetIntegerInput(string prompt)
            {
                int input;
                while (true)
                {
                    Console.WriteLine(prompt);
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine("Invalid integer");
                    }
                }
            }

            private GenAlgorithm GetGenAlgorithm()
            {
                string input;
                GenAlgorithm algorithm;
                while (true)
                {
                    Console.WriteLine("Enter Algorithm Name:");
                    input = Console.ReadLine();
                    if (Enum.TryParse<GenAlgorithm>(input, out algorithm))
                    {
                        return algorithm;
                    }
                    else
                    {
                        Console.WriteLine("Invalid algorithm");
                    }
                }
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