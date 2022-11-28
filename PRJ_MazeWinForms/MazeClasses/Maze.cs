using MazeFormsClasses;
using MyDataStructures;
using PRJ_MazeWinForms;
using PRJ_MazeWinForms.Logging;
using System;

namespace MazeClasses
{
    public abstract class Maze
    {

        protected Graph _graph;
        protected Player _player;
        protected IMazeDisplayer _mazeDisplayer;
        protected MazeInterface _mazeInterface;
        protected MyList<Node> _solution;
        public int Width { private set; get; }
        public int Height { private set; get; }
        public NodeLocation StartLocation { get { return _graph.StartNode.Location; } }
        public NodeLocation EndLocation { get { return _graph.EndNode.Location; } }
        public NodeLocation PlayerLocation { get { return _player.Location; } }
        public bool Finished { get { return PlayerLocation == EndLocation; } }

        public MazeFinishedEventHandler OnMazeFinished;
        public Maze(MazeSettings Settings)
        {
            SetupMaze(Settings);
        }

        public void ShowMaze() => _mazeDisplayer.Display();
        public bool MakeMove(Direction direction)
        {
            bool success = _mazeInterface.TryMove(direction);
            return success;
        }

        public bool ShowHint()
        {
            bool success = true;
            _mazeDisplayer.Display(GetHint());
            return success;
        } 
        public bool ShowSolution()
        {
            bool success = true;
            _mazeDisplayer.Display(Solution);
            return success;
        }

        private void SetupMaze(MazeSettings Settings)
        {
            Width = Settings.Width;
            Height = Settings.Height;


            _graph = new Graph(Width, Height);
            _player = new Player(this);

            MazeGen.GenerateMaze(this, _graph, Settings.Algorithm, Settings.ShowGeneration);
            _solution = null;
        }
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


        protected MyList<NodeLocation> GetHint(int count = 4)
        {
            MyList<Node> PathToEnd = MazeSolver.WallFollower(_graph, PlayerLocation, EndLocation);
            MyList<NodeLocation> hint = new MyList<NodeLocation>();
            for (int i = 0; i < Math.Min(count, PathToEnd.Count); i++)
            {
                hint.Add(PathToEnd[i].Location);
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
            for (int i = 0; i < 4; i++)
            {
                if (ConnectedNodes[i] == null)
                {
                    Walls[i] = true;
                }
            }
            return Walls;
        }

        public void EndMaze()
        {
            LogHelper.Log("End Maze called");
            OnMazeFinished.Invoke(this, new MazeFinishedEventArgs(_player));
        }
    }



    public abstract class MazeInterface : IMazeInterface
    {
        protected Maze _maze;
        private Player _player;
        protected MazeControlSettings _controlSettings;

        protected char[] _movementKeys { get { return _controlSettings.Movement; } }

        public MazeInterface(Maze Maze, Player Player, MazeControlSettings ControlSettings)
        {
            _maze = Maze;
            _player = Player;
            _controlSettings = ControlSettings;
        }

        public virtual void Play()
        {

        }

        public bool TryMove(Direction moveDirection)
        {
            // If maze finished, don't move
            if (_maze.Finished) return false;

            bool success = false;
            NodeLocation NextLocation = null;
            NodeLocation CurrentLocation = _maze.PlayerLocation;
            switch (moveDirection)
            {
                case Direction.North:
                    NextLocation = new NodeLocation(CurrentLocation.X, CurrentLocation.Y - 1);
                    break;
                case Direction.East:
                    NextLocation = new NodeLocation(CurrentLocation.X + 1, CurrentLocation.Y);
                    break;
                case Direction.South:
                    NextLocation = new NodeLocation(CurrentLocation.X, CurrentLocation.Y + 1);
                    break;
                case Direction.West:
                    NextLocation = new NodeLocation(CurrentLocation.X - 1, CurrentLocation.Y);
                    break;
            }
            if (_maze.CheckAccessibility(CurrentLocation, NextLocation))
            {
                LogHelper.Log(String.Format("Nodes {0} and {1} accessible, so move success", CurrentLocation, NextLocation));
                _player.Move(NextLocation);
                _maze.ShowMaze();
                success = true;

                if (_maze.Finished)
                {
                    LogHelper.Log("Player reached the end");
                    _maze.EndMaze();
                }
            else
                {
                    LogHelper.Log(String.Format("Player tried to move into wall from {0} to {1}", CurrentLocation, NextLocation));
                }
            }
            return success;
        }


        public void ShowHint()
        {
            _maze.ShowHint();
        }
    }


    public interface IMazeDisplayer
    {
        void Display(MyList<NodeLocation> hints = null);
    }

    public interface IMazeInterface
    {
        bool TryMove(Direction moveDirection);
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

    public delegate void MazeFinishedEventHandler(Maze sender, MazeFinishedEventArgs e);
    public class MazeFinishedEventArgs : EventArgs
    {
        private Player _player;
        public MazeFinishedEventArgs(Player player)
        {
            _player = player;
        }
        public int MoveCount { get { return _player.MoveCount; } }
        public int HintCount { get { return _player.HintsUsed; } }
        public bool SolutionUsed { get { return _player.SolutionUsed; } }

        public double TimeTaken { get { return _player.TimeTaken; } }


        public MyList<(string, string)> GetStats()
        {
            MyList<(string, string)> Stats = new MyList<(string, string)>();
            Stats.Add(("Moves Used", MoveCount.ToString()));
            Stats.Add(("Hints Used", HintCount.ToString()));
            Stats.Add(("Solution Used", SolutionUsed.ToString()));
            Stats.Add(("Time Taken", TimeTaken.ToString() + " seconds"));

            return Stats;

        }
    }



    public delegate void MazeErrorEventHandler(object source, MazeErrorEventArgs e);
    public class MazeErrorEventArgs : EventArgs
    {
        private string _errorReason;
        public MazeErrorEventArgs(string reason)
        {
            _errorReason = reason;
        }
        public string GetReason()
        {
            return _errorReason;
        }
    }

}
