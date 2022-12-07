using MazeClasses;
using MazeFormsClasses;
using MyDataStructures;
using System;


namespace MazeConsole
{
    public class ConsoleMaze : Maze
    {
        // Console implementation of the Maze, used initially for testing generation and solving algorithms
        public ConsoleMaze(int width, int height, GenAlgorithm algorithm, bool showGeneration) :
            base(new MazeSettings(width, height, algorithm, showGeneration))
        {
            ConsoleMazeSetup();
        }

        public ConsoleMaze(MazeSettings Settings) : base(Settings)
        {
            ConsoleMazeSetup();
        }

        private void ConsoleMazeSetup()
        {
            _mazeDisplayer = new ConsoleMazeDisplayer(this);
            _mazeInterface = new ConsoleMazeInterface(this, _player, MazeControlSettings.Default);
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
        public void Display(MyList<NodeLocation> Highlights = null)
        {
            Console.WriteLine(GetStringDisplay());
        }

        public void RemoveHint(NodeLocation hintLocation)
        {
            // placeholder, does nothing in console maze
        }


        private string GetStringDisplay()
        {
            {
                MyList<NodeLocation> highlightNodes = _maze.VisibleHints;
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

                        else if (ThisNodeLocation == _maze.StartLocation)
                            currEastWalls += START_CHAR;

                        else if (ThisNodeLocation == _maze.EndLocation)
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

    public class ConsoleMazeInterface : MazeInterface
    {
        public ConsoleMazeInterface(Maze Maze, Player Player, MazeControlSettings ControlSettings) : base(Maze, Player, ControlSettings)
        {

        }

        public override void Play()
        {
            while (!_maze.Finished)
            {
                char key = Console.ReadKey(true).KeyChar;
                if (key == _controlSettings.Hint)
                {
                    _maze.ShowHint();
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (_movementKeys[i] == key)
                        {
                            TryMove((Direction)i);
                            break;
                        }
                    }
                }
            }
            Console.WriteLine("You finished!");
        }
    }
}