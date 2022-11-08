using MazeConsole;
using MazeConsole.MyDataStructures;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PRJ_MazeWinForms.MazeFormsClasses
{
    class FormsMaze : Maze
    {
        private readonly Color WALL_COLOUR = Color.Black;
        private readonly Color CELL_COLOUR = Color.White;
        private readonly Color HIGHLIGHT_COLOUR = Color.Red;
        private readonly Color PLAYER_COLOUR = Color.DarkBlue;

        private TableLayoutPanel _container;
        
        private bool _formDisplayed;
        private MyList<(Panel, PaintEventHandler)> _highlights;

        public Control Parent { get { return _container; } }

        public FormsMaze(MazeSettings Settings, TableLayoutPanel Container) : base(Settings)
        {
            SetupAttributes(Container);
        }

        public FormsMaze(int height, int width, string algorithm, TableLayoutPanel Container, bool ShowGeneration = false)
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
            _container.ColumnStyles.Clear();
            for (int row = 0; row < Height; row++)
            {
                _container.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Height));
            }
            for (int col = 0; col < Width; col++)
            {
                _container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Width));
            }
        }

        public void DisplayForms(Node CurrentNode = null, bool ShowSolution = false, bool ShowHint = false)
        {
            if (!_formDisplayed)
            {
                GetFormsDisplay(CurrentNode);
            }

            if (ShowSolution)
            {
                ShowFormsHint(GetHint(CurrentNode));
            }
            else if (ShowHint)
            {
                ShowFormsHint(GetHint(CurrentNode));
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
            foreach((Panel, PaintEventHandler) pair in _highlights)
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

        private void ShowFormsHint(MyList<Node> highlightNodes)
        {
            foreach (Node n in highlightNodes)
            {
                Panel panel = (Panel)_container.GetControlFromPosition(n.Location.X, n.Location.Y);
                panel.Paint += new PaintEventHandler((sender, e) => MazeDisplay.HighlightCell(sender, e, HIGHLIGHT_COLOUR));
                panel.Invalidate();
            }
        }

       
    }
}


