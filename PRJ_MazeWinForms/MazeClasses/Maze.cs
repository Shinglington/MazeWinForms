using MazeConsole.MyDataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeConsole
{
    class Maze
    {
        protected Graph _graph;
        protected MyList<Node> _solution;
        public int Width { private set; get; }
        public int Height { private set; get; }

        public Maze(int width, int height, string algorithm, bool showGeneration)
        {
            Width = width;
            Height = height;
            _graph = new Graph(Width, Height);
            MazeGen.GenerateMaze(_graph, algorithm, showGeneration);
            _solution = null;
        }

        public Maze(MazeSettings Settings)
        {
            Width = Settings.Width;
            Height = Settings.Height;
            _graph = new Graph(Width, Height);
            MazeGen.GenerateMaze(_graph, Settings.Algorithm, Settings.ShowGeneration);
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


        public void DisplayMaze(Node CurrentNode = null, bool ShowSolution = false, bool ShowHint = false)
        {
            string Display = "";
            if (ShowSolution)
            {
                Display = _graph.GetDisplay(CurrentNode, Solution);
            }
            else if (ShowHint)
            {
                Display = _graph.GetDisplay(CurrentNode, GetHint(CurrentNode));
            }
            else
            {
                Display = _graph.GetDisplay(CurrentNode);
            }
            Console.WriteLine(Display);
        }


        private MyList<Node> GetHint(Node CurrentNode)
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
        public MazeSettings(int width, int height, GenAlgorithm algorithm, bool showGeneration)
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