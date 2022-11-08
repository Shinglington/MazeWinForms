namespace MazeConsole
{
    class Player
    {
        private Maze _maze;
        public Node CurrentNode { get; private set; }

        private int _moveCount;
        private int _hintCount;
        public Player(Maze M)
        {
            _maze = M;
            _moveCount = 0;
            _hintCount = 0;
            CurrentNode = M.StartNode;

        }

        public bool Move(Node NextNode)
        {
            bool validMove = false;
            if (_maze.CheckAccessibility(CurrentNode, NextNode))
            {
                validMove = true;
                CurrentNode = NextNode;
                _moveCount++;
            }
            return validMove;
        }
    }
}