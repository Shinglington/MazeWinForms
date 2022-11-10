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
        private readonly Color WALL_COLOUR = Color.Black;
        private readonly Color CELL_COLOUR = Color.White;
        private readonly Color HIGHLIGHT_COLOUR = Color.Red;
        private readonly Color PLAYER_COLOUR = Color.DarkBlue;

        private const int HINT_FACTOR = 5;
        private const int MINIMUM_PADDING = 10;

        public MazeErrorEventHandler OnMazeError;

        // Forms attributes
        private TableLayoutPanel _container;
        public Control Parent { get { return _container; } }
        private bool _formDisplayed;

        // Keep track of which cell is being highlighted (i.e. the cell the player is in)
        private MyList<(Panel, PaintEventHandler)> _highlights;



        public WinFormsMaze(MazeSettings Settings, TableLayoutPanel Container) : base(Settings)
        {
            SetupAttributes(Container);
        }

        public WinFormsMaze(int height, int width, string algorithm, TableLayoutPanel Container, bool ShowGeneration = false)
            : base(height, width, algorithm, ShowGeneration)
        {
            SetupAttributes(Container);
        }


        private void SetupAttributes(TableLayoutPanel Container)
        {
            _formDisplayed = false;
            // Setup container
            _container = Container;
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

            _container.Padding = MyFormMethods.ComputePadding(_container, MINIMUM_PADDING);
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

                    // Figure out which colour cell needs to be
                    Color cell_colour = CELL_COLOUR;
                    if (node == StartNode)
                    {
                        cell_colour = Color.Green;
                    }
                    else if (node == EndNode)
                    {
                        cell_colour = Color.Red;
                    }
                    Cell.Paint += new PaintEventHandler((sender, e) => MazeDisplay.PaintNode(sender, e, node, cell_colour, WALL_COLOUR));

                    if (node == CurrentNode)
                    {
                        PaintEventHandler highlightEvent = new PaintEventHandler((sender, e) => MazeDisplay.HighlightCell(sender, e, PLAYER_COLOUR));
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
                PaintEventHandler highlightEvent = new PaintEventHandler((sender, e) => MazeDisplay.HighlightCell(sender, e, PLAYER_COLOUR));
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
                panel.Paint += new PaintEventHandler((sender, e) => MazeDisplay.HighlightCell(sender, e, HIGHLIGHT_COLOUR));
                panel.Invalidate();
            }
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


