using MazeConsole;
using MazeConsole.MyDataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PRJ_MazeWinForms.MazeFormsClasses
{
    class WinFormsMaze : Maze
    {
        // Colour constants
        private MazeDisplaySettings _displaySettings;

        public MazeErrorEventHandler OnMazeError;

        // Forms attributes
        private TableLayoutPanel _container;
        public Control Parent { get { return _container; } }


        public WinFormsMaze(MazeSettings Settings, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container) : base(Settings)
        {
            _container = Container;
            _displaySettings = DisplaySettings;
            _mazeDisplayer = new FormsMazeDisplayer(this, DisplaySettings, Container);
            _mazeInterface = new FormsMazeInterface(this, _player);
        }

        public WinFormsMaze(int height, int width, GenAlgorithm algorithm, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container, bool ShowGeneration = false)
            : base(height, width, algorithm, ShowGeneration)
        {
            _container = Container;
            _displaySettings = DisplaySettings;
            _mazeDisplayer = new FormsMazeDisplayer(this, DisplaySettings, Container);
            _mazeInterface = new FormsMazeInterface(this, _player);
        }


        public override void Display(bool ShowSolution = false, bool ShowHint = false)
        {
            if (ShowSolution)
            {
                _mazeDisplayer.DisplaySolution();
            }
            else if (ShowHint)
            {
                _mazeDisplayer.DisplaySolution();
            }
            else
            {
                _mazeDisplayer.DisplayMaze();
            }
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
                    Cell.Paint += new PaintEventHandler((sender, e) => 
                        PaintNode(sender, e, CurrentLocation, CurrentLocation == _maze.StartNodeLocation, CurrentLocation == _maze.EndNodeLocation));
                }
            }
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
            SolidBrush brush = new SolidBrush(_displaySettings.WallColour);

            int WALL_RATIO = _displaySettings.WallRatio;
            bool[] Walls = _maze.GetWalls(location);

            // Draw walls
            if (Walls[0])
            {
                g.FillRectangle(brush, 0, 0, cell.Width, cell.Height / WALL_RATIO);
            }
            if (Walls[1])
            {
                g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, 0, cell.Width / WALL_RATIO, cell.Height);
            }
            if (Walls[2])
            {
                g.FillRectangle(brush, 0, cell.Height - cell.Height / WALL_RATIO, cell.Width, cell.Height / WALL_RATIO);
            }
            if (Walls[3])
            {
                g.FillRectangle(brush, 0, 0, cell.Width / WALL_RATIO, cell.Height);
            }

            // Draw wall corners
            g.FillRectangle(brush, 0, 0, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);
            g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, 0, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);
            g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, cell.Height - cell.Height / WALL_RATIO, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);
            g.FillRectangle(brush, 0, cell.Height - cell.Height / WALL_RATIO, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);

            // Colour cell
            brush = new SolidBrush(_displaySettings.CellColour);
            if (IsStartNode)
            {
                brush = new SolidBrush(_displaySettings.StartColour);
            }
            else if (IsEndNode)
            {
                brush = new SolidBrush(_displaySettings.EndColour);
            }
            g.FillRectangle(brush, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO, cell.Width - (2 * cell.Width / WALL_RATIO), cell.Height - (2 * cell.Height / WALL_RATIO));
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
            for(int i = 0; i < 4; i++)
            {
                ParentForm.KeyPress += new KeyPressEventHandler((object sender, KeyPressEventArgs e) => KeyPressed(_movementKeys[i], (Direction)i));
            }
        }

        private void KeyPressed(char key, Direction directionBind)
        {
            TryMove(directionBind);
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


