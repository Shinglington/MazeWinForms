using MazeConsole.MyDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole
{
    public interface IMazeDisplayer
    {
        void DisplayMaze();

        void DisplaySolution();
    }

    public interface IMazeInterface
    {
        bool TryMove(Direction moveDirection);

        void SetupControls(char[] movementKeys);
    }

    public abstract class MazeInterface : IMazeInterface
    {
        protected Maze _maze;
        private Player _player;

        protected Char[] _movementKeys;
        protected char HINT_CONTROL = 'h';

        public MazeInterface(Maze Maze, Player Player)
        {
            _maze = Maze;
            _player = Player;
            SetupControls();
        }

        public virtual void Play()
        {

        }

        public void SetupControls(char[] MoveControls = null)
        {
            _movementKeys = new char[4];
            if (MoveControls == null)
            {
                _movementKeys = new char[] { 'w', 'd', 's', 'a' };
                return;
            }

            if (MoveControls.Length == 4)
            {
                try
                {
                    for (int i = 0; i < 4; i++)
                    {
                        _movementKeys[i] = MoveControls[i];
                    }
                }
                catch
                {
                    _movementKeys = new char[] { 'w', 'd', 's', 'a' };
                }
            }
            else
            {
                _movementKeys = new char[] { 'w', 'd', 's', 'a' };
            }
        }

        public bool TryMove(Direction moveDirection)
        {
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
                _player.Move(NextLocation);
                _maze.Display();
                success = true;
            }
            return success;
        }


        public void ShowHint()
        {
            _maze.Display(false, true);
        }
    }
}
