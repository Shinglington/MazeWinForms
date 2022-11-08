
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
        private FormsMaze _maze;

        // Enums
        private SolutionVisibility _solutionVis;


        // Colours
        private readonly Color SOLUTION_COLOUR = Color.Red;
        private readonly Color WALL_COLOUR = Color.Black;

        

        public MazeForm(MazeSettings MazeSettings)
        {
            InitializeComponent();

            CreateControls();
            CreateMaze(MazeSettings);
            DisplayMaze(_tbl_mazePanel);
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


        }


        private void DisplayMaze(TableLayoutPanel TableContainer)
        {
            _maze.DisplayForms();
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
            HintStrip.DropDownItems.Add("Show full solution").Click += new EventHandler(ShowFullSolution);
        }

        private void ShowFullSolution(object sender, EventArgs e)
        {
            if (_solutionVis != SolutionVisibility.Full)
            {
                _maze.DisplayForms(null, true);
                _solutionVis = SolutionVisibility.Full;
            }
            else
            {
                MessageBox.Show("Solution already being shown");
            }
        }
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
