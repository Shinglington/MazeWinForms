﻿using MazeClasses;
using MyDataStructures;
using PRJ_MazeWinForms;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace MazeFormsClasses
{
    class WinFormsMaze : Maze
    {
        // Windows Forms implementation of Maze, inherited from base Maze
        // Overrides and changes some methods and attributes to adapt them to be suitable for a WindowsForms appliation

        // Forms attributes
        private TableLayoutPanel _container;
        public Control Parent { get { return _container; } }


        public WinFormsMaze(MazeSettings Settings, TableLayoutPanel Container, MazeDisplaySettings DisplaySettings, MazeControlSettings ControlSettings) : base(Settings)
        {
            WinFormsMazeSetup(DisplaySettings, ControlSettings, Container);
        }
        private void WinFormsMazeSetup(MazeDisplaySettings DisplaySettings, MazeControlSettings ControlSettings, TableLayoutPanel Container)
        {
            _container = Container;
            _mazeDisplayer = new FormsMazeDisplayer(this, DisplaySettings, Container);
            _mazeInterface = new FormsMazeInterface(this, _player, ControlSettings);

        }
    }

    internal class FormsMazeDisplayer : IMazeDisplayer
    {
        private Maze _maze;
        private MazeDisplaySettings _displaySettings;
        private TableLayoutPanel _container;

        private bool _isDisplaying;

        private NodeLocation _displayedPlayerLocation;
        private PaintEventHandler _playerPaintMethod;

        private MyList<(NodeLocation, PaintEventHandler)> _hintPaintEvents;

        public FormsMazeDisplayer(WinFormsMaze Maze, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container)
        {
            _maze = Maze;

            _displaySettings = DisplaySettings;
            _container = Container;
            _isDisplaying = false;

            _playerPaintMethod = null;
            _displayedPlayerLocation = null;

            _hintPaintEvents = new MyList<(NodeLocation, PaintEventHandler)>();

            SetupContainer();
        }
        private void SetupContainer()
        {
            _container.RowStyles.Clear();
            _container.RowCount = 0;
            _container.ColumnStyles.Clear();
            _container.ColumnCount = 0;
            for (int row = 0; row < _maze.Height; row++)
            {
                _container.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _maze.Height));
                _container.RowCount += 1;
            }
            for (int col = 0; col < _maze.Width; col++)
            {
                _container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _maze.Width));
                _container.ColumnCount += 1;
            }

            _container.Padding = MyFormMethods.ComputePadding(_container, _displaySettings.MinimumPadding);
        }

        public void Display(MyList<NodeLocation> Highlights = null)
        {
            if (!_isDisplaying)
            {
                InitialMazeDisplay();
            }
            UpdateMazeDisplay(Highlights);
        }

        public void RemoveHint(NodeLocation hintLocation)
        {
            foreach ((NodeLocation, PaintEventHandler) pair in _hintPaintEvents)
            {
                // loop through hintPaintEvents to find associated event
                // with the hintLocation given to remove the event from the panel.
                NodeLocation location = pair.Item1;
                PaintEventHandler paintEvent = pair.Item2;
                if ((pair.Item1) == hintLocation)
                {

                    Panel HintCell = (Panel)_container.GetControlFromPosition(location.X, location.Y);
                    HintCell.Paint -= paintEvent;
                    HintCell.Invalidate();
                    break;
                }
            }
        }

        private void InitialMazeDisplay()
        {
            for (int row = 0; row < _maze.Height; row++)
            {
                for (int col = 0; col < _maze.Width; col++)
                {
                    NodeLocation CurrentLocation = new NodeLocation(col, row);
                    Panel Cell = new Panel() { Parent = _container, Dock = DockStyle.Fill, Margin = new Padding(0) };
                    _container.SetCellPosition(Cell, new TableLayoutPanelCellPosition(col, row));
                    bool isStartNode = CurrentLocation == _maze.StartLocation;
                    bool isEndNode = CurrentLocation == _maze.EndLocation;
                    Cell.Paint += new PaintEventHandler((sender, e) =>
                        PaintNode(sender, e, CurrentLocation, isStartNode, isEndNode));
                }
            }
            _isDisplaying = true;
        }
        private void UpdateMazeDisplay(MyList<NodeLocation> HintHighlights = null)
        {
            // Remove existing player paints
            if (_displayedPlayerLocation != null)
            {
                Panel PreviousCell = (Panel)_container.GetControlFromPosition(_displayedPlayerLocation.X, _displayedPlayerLocation.Y);
                PreviousCell.Paint -= _playerPaintMethod;
                PreviousCell.Invalidate();
            }
            // Show new player position
            Panel CurrentCell = (Panel)_container.GetControlFromPosition(_maze.PlayerLocation.X, _maze.PlayerLocation.Y);
            _displayedPlayerLocation = _maze.PlayerLocation;
            _playerPaintMethod = new PaintEventHandler(PaintPlayer);
            CurrentCell.Paint += _playerPaintMethod;
            CurrentCell.Invalidate();

            // Show hint highlights
            if (HintHighlights != null)
            {
                // Remove pre-existing hints
                foreach ((NodeLocation, PaintEventHandler) pair in _hintPaintEvents)
                {
                    RemoveHint(pair.Item1);
                }


                // Add new hints
                foreach (NodeLocation location in HintHighlights)
                {
                    Panel HintCell = (Panel)_container.GetControlFromPosition(location.X, location.Y);

                    PaintEventHandler newPaintEvent = new PaintEventHandler(PaintHint);
                    HintCell.Paint += newPaintEvent;
                    // Add the location, event pair to a list so we can get rid of it later.
                    _hintPaintEvents.Add((location, newPaintEvent));
                    HintCell.Invalidate();
                }
            }
        }

        private void PaintNode(object sender, PaintEventArgs e, NodeLocation location, bool IsStartNode = false, bool IsEndNode = false)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(_displaySettings.CellColour);

            int WALL_RATIO = _displaySettings.WallRatio;
            int yThickness = cell.Height / WALL_RATIO;
            int xThickness = cell.Width / WALL_RATIO;

            // Colour cell
            if (IsStartNode)
                brush = new SolidBrush(_displaySettings.StartColour);
            else if (IsEndNode)
                brush = new SolidBrush(_displaySettings.EndColour);

            g.FillRectangle(brush, 0, 0, cell.Width, cell.Height);

            bool[] Walls = _maze.GetWalls(location);
            Rectangle[] WallAreas = new Rectangle[]
            {
                new Rectangle(xThickness, 0, cell.Width - (2 * xThickness), yThickness),
                new Rectangle(cell.Width - xThickness, yThickness, xThickness, cell.Height - (2 * yThickness)),
                new Rectangle(xThickness, cell.Height - yThickness, cell.Width - (2 * xThickness), yThickness),
                new Rectangle(0, yThickness, xThickness, cell.Height - (2 * yThickness))
            };
            Rectangle[] CornerAreas = new Rectangle[]
            {
                new Rectangle(0, 0, xThickness, yThickness),
                new Rectangle(cell.Width - xThickness, 0, xThickness, yThickness),
                new Rectangle(cell.Width - xThickness, cell.Height - yThickness, xThickness, yThickness),
                new Rectangle(0, cell.Height - yThickness, xThickness, yThickness)
            };
            brush = new SolidBrush(_displaySettings.WallColour);
            // Draw walls
            for (int i = 0; i < Walls.Length; i++)
            {
                if (Walls[i])
                {
                    g.FillRectangle(brush, WallAreas[i]);
                }
                g.FillRectangle(brush, CornerAreas[i]);

            }
        }

        public void PaintPlayer(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(_displaySettings.PlayerColour);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / 3;
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
        public void PaintHint(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(_displaySettings.HintColour);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / _displaySettings.WallRatio;
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
    }

    class FormsMazeInterface : MazeInterface
    {
        public FormsMazeInterface(WinFormsMaze Maze, Player Player, MazeControlSettings ControlSettings) : base(Maze, Player, ControlSettings)
        {

        }

        public override void Play()
        {
            WinFormsMaze Maze = (WinFormsMaze)_maze;
            Form ParentForm = (Form)Maze.Parent.Parent.Parent;
            ParentForm.KeyPress += new KeyPressEventHandler(KeyPressed);

        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            for (int i = 0; i < 4; i++)
                if (keyChar == _movementKeys[i])
                    TryMove((Direction)i);
        }
    }



}


