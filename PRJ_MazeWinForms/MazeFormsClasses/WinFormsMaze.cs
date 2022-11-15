using MazeClasses;
using PRJ_MazeWinForms;
using MyDataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace MazeFormsClasses
{
    class WinFormsMaze : Maze
    {

        // Forms attributes
        private TableLayoutPanel _container;
        public Control Parent { get { return _container; } }


        public WinFormsMaze(MazeSettings Settings, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container) : base(Settings)
        {
            WinFormsMazeSetup(DisplaySettings, Container);
        }

        public WinFormsMaze(int height, int width, GenAlgorithm algorithm, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container, bool ShowGeneration = false)
            : base(new MazeSettings(height, width, algorithm, ShowGeneration))
        {
            WinFormsMazeSetup(DisplaySettings, Container);
        }

        private void WinFormsMazeSetup(MazeDisplaySettings DisplaySettings, TableLayoutPanel Container)
        {
            _container = Container;
            _mazeDisplayer = new FormsMazeDisplayer(this, DisplaySettings, Container);
            _mazeInterface = new FormsMazeInterface(this, _player);
            
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


        public FormsMazeDisplayer(WinFormsMaze Maze, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container)
        {
            _maze = Maze;

            _displaySettings = DisplaySettings;
            _container = Container;
            _isDisplaying = false;

            _playerPaintMethod = null;
            _displayedPlayerLocation = null;

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

        public void DisplayMaze() 
        {
            if (!_isDisplaying)
            {
                InitialMazeDisplay();
            }
            UpdateMazeDisplay();
        }

        public void DisplaySolution()
        {
            if (!_isDisplaying)
            {
                InitialMazeDisplay();
            }
            UpdateMazeDisplay(_maze.Solution);
        }



        private void InitialMazeDisplay()
        {
            for(int row = 0; row < _maze.Height; row++)
            {
                for(int col = 0; col < _maze.Width; col++)
                {
                    NodeLocation CurrentLocation = new NodeLocation(col, row);
                    Panel Cell = new Panel() { Parent = _container, Dock = DockStyle.Fill, Margin = new Padding(0) };
                    _container.SetCellPosition(Cell, new TableLayoutPanelCellPosition(col, row));
                    bool isStartNode = (CurrentLocation.X == _maze.StartNodeLocation.X && CurrentLocation.Y == _maze.StartNodeLocation.Y);
                    bool isEndNode = (CurrentLocation.X == _maze.EndNodeLocation.X && CurrentLocation.Y == _maze.EndNodeLocation.Y);
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
                foreach (NodeLocation location in HintHighlights)
                {
                    Panel HintCell = (Panel)_container.GetControlFromPosition(location.X, location.Y);
                    HintCell.Paint += new PaintEventHandler(PaintHint);
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
            {
                brush = new SolidBrush(_displaySettings.StartColour);
            }
            else if (IsEndNode)
            {
                brush = new SolidBrush(_displaySettings.EndColour);
            }
            g.FillRectangle(brush, 0,0, cell.Width, cell.Height);

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
            float radius = Math.Min(cell.Width, cell.Height) / _displaySettings.WallRatio;
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
        public FormsMazeInterface(WinFormsMaze Maze, Player Player) : base(Maze, Player)
        {

        }

        public override void Play()
        {
            WinFormsMaze Maze = (WinFormsMaze)_maze;
            Form ParentForm = (Form) Maze.Parent.Parent.Parent;
            ParentForm.KeyPress += new KeyPressEventHandler(KeyPressed);

        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            for (int i = 0; i < 4; i++)
            {
                if (keyChar == _movementKeys[i]) 
                    TryMove((Direction)i);
            }
        }
    }



}


