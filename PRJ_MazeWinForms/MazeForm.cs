
using System;

using System.Windows.Forms;

using MazeClasses;
using MazeFormsClasses;
using MyDataStructures;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {

        // Controls
        private Form _settingsForm;
        private TableLayoutPanel _tbl_formPanel;
        private TableLayoutPanel _tbl_mazePanel;
        private MenuStrip _menuStrip;

        // Classes
        private WinFormsMaze _maze;


        public MazeForm(Form settingsForm, MazeSettings MazeSettings, MazeDisplaySettings DisplaySettings)
        {
            InitializeComponent();
            CreateControls();
            _settingsForm = settingsForm;
            _maze = new WinFormsMaze(MazeSettings, DisplaySettings, _tbl_mazePanel);
            SetupEvents();

            _maze.PlayMaze();
        }
        private void SetupEvents()
        {
            this.FormClosing += new FormClosingEventHandler(MazeFormClosing);
            this.FormClosed += new FormClosedEventHandler(ReturnToSettings);

        }
        private void CreateControls()
        {
            // Create form table panel
            _tbl_formPanel = new TableLayoutPanel()
            {
                Parent = this,
                Dock = DockStyle.Fill,
                Padding = new Padding(5)
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

        private void SetupMenuStrip(MenuStrip menuStrip)
        {
            // Populate menu strip
            foreach(object item in Enum.GetValues(typeof(MyMenuItem)))
            {
                menuStrip.Items.Add(new ToolStripMenuItem(item.ToString()));
            }
        
        }


        private void MazeFinished(object source, MazeFinishedEventArgs e)
        {
            MessageBox.Show("You finished the maze!");
            ReturnToSettings(this, new EventArgs());

        }

        private void ReturnToSettings(object sender, EventArgs e)
        {
            this.Hide();
            _settingsForm.Show();
            this.Dispose();
        }

        private void MazeFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Don't Give Up!", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                // If user doesn't want to quit, cancel quit
                e.Cancel = true;
            }
        }

        private void MazeErrorRaised(object sender, MazeErrorEventArgs e)
        {
            if (e.GetReason() != null)
            {
                Console.WriteLine("Error raised: {0}",e.GetReason());
                MessageBox.Show("Error raised: {0}", e.GetReason());
            }
        }

    }

    public delegate void MazeFinishedEventHandler(object source, MazeFinishedEventArgs e);

    // Event arguments about player stats when maze finished
    public class MazeFinishedEventArgs : EventArgs
    {
        private bool _mazeFinished;
        private Player _player;
        public MazeFinishedEventArgs(bool finished, Player player)
        {
            _mazeFinished = finished;
            _player = player;
        }

        public bool Finished { get { return _mazeFinished; } }
        public int MoveCount { get { return _player.MoveCount; } }
        public int HintCount { get { return _player.HintsUsed; } }
        public bool SolutionUsed { get { return _player.SolutionUsed; } }


    }


    public enum MyMenuItem
    {
        File,
        Hint,
        Help
    }
}
