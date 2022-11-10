﻿namespace MazeConsole
{
    public class Player
    {
        private Maze _maze;
        public Node CurrentNode { get; private set; }

        private int _movesUsed;
        private int _hintsUsed;
        private bool _solutionUsed;
        public Player(Maze M)
        {
            _maze = M;
            _movesUsed = 0;
            _hintsUsed = 0;
            _solutionUsed = false;
            CurrentNode = M.StartNode;

        }

        public bool SolutionUsed { get { return _solutionUsed; } }
        public int MoveCount { get { return _movesUsed; } }
        public int HintsUsed { get { return _hintsUsed; } }


        public bool Move(Node NextNode)
        {
            bool validMove = false;
            if (_maze.CheckAccessibility(CurrentNode, NextNode))
            {
                validMove = true;
                CurrentNode = NextNode;
                _movesUsed++;
            }
            return validMove;
        }

        public void UseHint()
        {
            _hintsUsed++;
        }

        public void UseSolution()
        {
            _solutionUsed = true;
        }
    }
}