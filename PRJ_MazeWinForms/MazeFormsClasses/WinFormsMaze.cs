using MazeConsole;
using MazeConsole.MyDataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PRJ_MazeWinForms.MazeFormsClasses
{
    class WinFormsMaze : Maze
    {
        // Colour constants
        private MazeDisplay _mazeDisplay;
        private MazeDisplaySettings _displaySettings;

        private const int HINT_FACTOR = 5;
        private const int MINIMUM_PADDING = 10;

        public MazeErrorEventHandler OnMazeError;

        // Forms attributes
        private TableLayoutPanel _container;
        public Control Parent { get { return _container; } }
        private bool _formDisplayed;

        // Keep track of which cell is being highlighted (i.e. the cell the player is in)
        private MyList<(Panel, PaintEventHandler)> _highlights;



        public WinFormsMaze(MazeSettings Settings, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container) : base(Settings)
        {
            _formDisplayed = false;
            _container = Container;
            _displaySettings = DisplaySettings;
            _mazeDisplay = new MazeDisplay(_displaySettings);
            SetupContainer();
        }

        public WinFormsMaze(int height, int width, string algorithm, MazeDisplaySettings DisplaySettings, TableLayoutPanel Container, bool ShowGeneration = false)
            : base(height, width, algorithm, ShowGeneration)
        {
            _formDisplayed = false;
            _container = Container;
            _displaySettings = DisplaySettings;
            _mazeDisplay = new MazeDisplay(DisplaySettings);
            SetupContainer();
        }


        private void SetupContainer()
        {
            _container.RowStyles.Clear();
            _container.RowCount = 0;
            _container.ColumnStyles.Clear();
            _container.ColumnCount = 0;
            for (int row = 0; row < Height; row++)
            {
                _container.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / Height));
                _container.RowCount += 1;
            }
            for (int col = 0; col < Width; col++)
            {
                _container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / Width));
                _container.ColumnCount += 1;
            }

            _container.Padding = MyFormMethods.ComputePadding(_container, _displaySettings.MinimumPadding);
        }


        public void DisplayForms(Node CurrentNode = null, bool ShowSolution = false, bool ShowHint = false)
        {
            if (!_formDisplayed)
            {
                GetFormsDisplay(CurrentNode);
            }

            if (ShowSolution)
            {
                ShowFormsHint(Solution);
            }
            else if (ShowHint)
            {
                ShowFormsHint(GetHint(CurrentNode, HINT_FACTOR));
            }
            else
            {
                UpdateFormsDisplay(CurrentNode);
            }
        }

        private void GetFormsDisplay(Node CurrentNode = null)
        {
            _highlights = new MyList<(Panel, PaintEventHandler)>();
            Node[,] nodes = _graph.GetNodes();
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Node node = nodes[col, row];
                    // Create new panel for cell
                    Panel Cell = new Panel() { Parent = _container, Dock = DockStyle.Fill, Margin = new Padding(0) };
                    _container.SetCellPosition(Cell, new TableLayoutPanelCellPosition(col, row));

                    // Paint cell
                    Cell.Paint += new PaintEventHandler((sender, e) => _mazeDisplay.PaintNode(sender, e, node,  node == StartNode, node == EndNode));



                    if (node == CurrentNode)
                    {
                        PaintEventHandler highlightEvent = new PaintEventHandler(_mazeDisplay.PaintPlayer);
                        Cell.Paint += highlightEvent;
                        _highlights.Add((Cell, highlightEvent));
                    }

                }
            }
            _formDisplayed = true;
        }

        // Removes event handlers from previous display (i.e. removes last location highlight)
        private void UpdateFormsDisplay(Node CurrentNode = null)
        {
            foreach ((Panel, PaintEventHandler) pair in _highlights)
            {
                Panel p = pair.Item1;
                PaintEventHandler highlightEvent = pair.Item2;
                p.Paint -= highlightEvent;
                p.Invalidate();
            }
            _highlights = new MyList<(Panel, PaintEventHandler)>();

            // Highlight current node
            if (CurrentNode != null)
            {
                Panel Cell = (Panel)_container.GetControlFromPosition(CurrentNode.Location.X, CurrentNode.Location.Y);
                PaintEventHandler highlightEvent = new PaintEventHandler(_mazeDisplay.PaintPlayer);
                Cell.Paint += highlightEvent;
                _highlights.Add((Cell, highlightEvent));
                Cell.Invalidate();
            }

        }

        private void ShowFormsHint(MyList<Node> hintNodes)
        {
            foreach(Node n in hintNodes)
            {
                Panel panel = (Panel)_container.GetControlFromPosition(n.Location.X, n.Location.Y);
                PaintEventHandler highlightEvent = new PaintEventHandler(_mazeDisplay.PaintHint);
                panel.Invalidate();
            }
        }


    }

    internal class MazeDisplay
    {
        private MazeDisplaySettings _displaySettings;
        private float WALL_RATIO;
        public MazeDisplay(MazeDisplaySettings DisplaySettings, int WallRatio = 6)
        {
            _displaySettings = DisplaySettings;
            WALL_RATIO = WallRatio;
        }

        public void PaintNode(object sender, PaintEventArgs e, Node node, bool IsStartNode = false, bool IsEndNode = false)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(_displaySettings.WallColour);

            // Draw walls
            if (node.NorthNode == null)
            {
                g.FillRectangle(brush, 0, 0, cell.Width, cell.Height / WALL_RATIO);
            }
            if (node.EastNode == null)
            {
                g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, 0, cell.Width / WALL_RATIO, cell.Height);
            }
            if (node.SouthNode == null)
            {
                g.FillRectangle(brush, 0, cell.Height - cell.Height / WALL_RATIO, cell.Width, cell.Height / WALL_RATIO);
            }
            if (node.WestNode == null)
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
                Console.WriteLine("Start node at {0},{1}", node.Location.X, node.Location.Y);
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
            float radius = Math.Min(cell.Width, cell.Height) / (WALL_RATIO);
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
        public void PaintHint(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(_displaySettings.HintColour);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / (WALL_RATIO);
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
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


