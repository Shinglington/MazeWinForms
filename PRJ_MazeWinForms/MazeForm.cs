
using System;
using System.Drawing;
using System.Windows.Forms;

using MazeConsole;
using MazeConsole.MyDataStructures;
using PRJ_MazeWinForms.MazeFormsClasses;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {

        // Controls
        private TableLayoutPanel _tbl_formPanel;

        private TableLayoutPanel _tbl_mazePanel;
        private MenuStrip _menuStrip;

        // Classes
        private MazeSettings _mazeSettings;
        private Maze _maze;

        // Enums
        private SolutionVisibility _solutionVis;


        // Colours
        private readonly Color SOLUTION_COLOUR = Color.Red;
        private readonly Color WALL_COLOUR = Color.Black;

        

        public MazeForm(MazeSettings MazeSettings)
        {
            InitializeComponent();

            CreateMaze(MazeSettings);
            CreateControls();
            _solutionVis = SolutionVisibility.None;

        }

        private void CreateMaze(MazeSettings MazeSettings)
        {
            _mazeSettings = MazeSettings;
            _maze = new FormsMaze(_mazeSettings, _tbl_mazePanel);
        }
        private void CreateControls()
        {
            // Create form table panel
            _tbl_formPanel = new TableLayoutPanel()
            {
                Parent = this,
                Dock = DockStyle.Fill
            };
            _tbl_formPanel.RowStyles.Clear();
            _tbl_formPanel.ColumnStyles.Clear();

            _tbl_formPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            _tbl_formPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));

            // Create maze panel
            _tbl_mazePanel = new TableLayoutPanel()
            {
                Parent = _tbl_formPanel,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)

            };
            _tbl_formPanel.SetCellPosition(_tbl_mazePanel, new TableLayoutPanelCellPosition(0, 1));

            _menuStrip = new MenuStrip()
            {
                Parent = _tbl_formPanel,
                Dock = DockStyle.Fill,
            };
            _tbl_formPanel.SetCellPosition(_menuStrip, new TableLayoutPanelCellPosition(0, 0));
            SetupMenuStrip(_menuStrip);

            DisplayMaze(_tbl_mazePanel);
        }


        private void DisplayMaze(TableLayoutPanel TableContainer)
        {
            _maze.DisplayMaze();
        }

        private void SetupMenuStrip(MenuStrip menuStrip)
        {
            // Populate menu strip
            foreach(object item in Enum.GetValues(typeof(MyMenuItem)))
            {
                menuStrip.Items.Add(new ToolStripMenuItem(item.ToString()));
            }

            // File header

            // Hint header
            ToolStripMenuItem HintStrip = (ToolStripMenuItem) menuStrip.Items[(int)MyMenuItem.Hint];
            /*
            HintStrip.DropDownItems.Add("Show full solution").Click += new EventHandler(ShowFullSolution);
            */
        }

        /*
        private void ShowFullSolution(object sender, EventArgs e)
        {
            if (_solutionVis != SolutionVisibility.Full)
            {
                MyList<Node> FullSolution = MazeSolver.WallFollower(_maze);
                foreach(Node n in FullSolution)
                {
                    int x = n.Location.X;
                    int y = n.Location.Y;

                    Panel cell = (Panel) _tbl_mazePanel.GetControlFromPosition(x, y);
                    cell.Paint += new PaintEventHandler(HighlightCell);
                    cell.Invalidate();
                    

                }

                _solutionVis = SolutionVisibility.Full;
            }
            else
            {
                MessageBox.Show("Solution already being shown");
            }
        }


        // PAINT FUNCTIONS
        private void HighlightCell(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(SOLUTION_COLOUR);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / 12;
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
        */
    }

    public enum MyMenuItem
    {
        File,
        Hint,
        Help
    }

    public enum SolutionVisibility
    {
        None,
        Partial,
        Full
    }
}
