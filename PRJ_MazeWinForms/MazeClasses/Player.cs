using PRJ_MazeWinForms.Logging;
using System;

namespace MazeClasses
{
    public class Player
    {
        private Maze _maze;
        public NodeLocation Location { get; private set; }

        private MyTimer _timer;

        private int _movesUsed;
        private int _hintsUsed;
        private bool _solutionUsed;
        public Player(Maze M)
        {
            _maze = M;
            _movesUsed = 0;
            _hintsUsed = 0;
            _solutionUsed = false;
            Location = M.StartLocation;
            _timer = new MyTimer();
        }

        public bool SolutionUsed { get { return _solutionUsed; } }
        public int MoveCount { get { return _movesUsed; } }
        public int HintsUsed { get { return _hintsUsed; } }

        public double TimeTaken { get { return Math.Round(_timer.TimeTaken, 3); } }

        public bool Move(NodeLocation Coords)
        {
            bool validMove = false;
            if (_maze.CheckAccessibility(Location, Coords))
            {
                validMove = true;
                Location = Coords;
                _movesUsed++;
                if (!_timer.Started)
                {
                    _timer.Start();
                }
                else if (Location == _maze.EndLocation)
                {
                    _timer.End();
                }
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

    class MyTimer
    {
        private DateTime _start;
        private DateTime _end;


        public bool Started;
        public MyTimer()
        {
            Started = false;
        }

        public void Start()
        {
            _start = DateTime.UtcNow;
            Started = true;
            LogHelper.Log("Timer started");
        }

        public void End()
        {
            _end = DateTime.UtcNow;
            LogHelper.Log("Timer ended");
        }

        private double CalculateTimeElapsed(DateTime start, DateTime end)
        {
            return end.Subtract(start).TotalSeconds;
        }

        public double TimeTaken
        {
            get { return CalculateTimeElapsed(_start, _end); }
        }
    }



}