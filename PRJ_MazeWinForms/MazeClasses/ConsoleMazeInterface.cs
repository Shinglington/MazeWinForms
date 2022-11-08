using System;
using System.Linq;

namespace MazeConsole
{
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

            _maze.DisplayConsole(null, true);
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
            _maze.DisplayConsole(_player.CurrentNode);
            while (_player.CurrentNode != _maze.EndNode)
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
            _maze.DisplayConsole(_player.CurrentNode, false, true);
        }

        private void MakeMove(Direction direction)
        {
            Node NextNode = null;
            switch (direction)
            {
                case Direction.North:
                    NextNode = _player.CurrentNode.NorthNode;
                    break;
                case Direction.East:
                    NextNode = _player.CurrentNode.EastNode;
                    break;
                case Direction.South:
                    NextNode = _player.CurrentNode.SouthNode;
                    break;
                case Direction.West:
                    NextNode = _player.CurrentNode.WestNode;
                    break;
            }
            if (NextNode != null)
            {
                _player.Move(NextNode);
                _maze.DisplayConsole(_player.CurrentNode);
            }
        }

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
}