
using MazeClasses;
using MazeFormsClasses;
using MyDataStructures;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {

        // Controls
        private Form _menuForm;
        private TableLayoutPanel _tbl_formPanel;
        private TableLayoutPanel _tbl_mazePanel;
        private MenuStrip _menuStrip;

        // Classes
        private WinFormsMaze _maze;
        private AppSettings _appSettings;

        public MazeForm(Form  menuForm, MazeSettings MazeSettings, AppSettings AppSettings)
        {
            InitializeComponent();
            CreateControls();
            _menuForm = menuForm;
            _appSettings = AppSettings;
            _maze = new WinFormsMaze(MazeSettings, _tbl_mazePanel, _appSettings.DisplaySettings, _appSettings.ControlSettings);
            SetupMenuStrip(_menuStrip);
            SetupEvents();
            _maze.ShowMaze();
        }

        private void SetupEvents()
        {
            this.FormClosing += new FormClosingEventHandler(MazeFormClosing);
            this.FormClosed += new FormClosedEventHandler(ReturnToMenu);
            _maze.OnMazeFinished += new MazeFinishedEventHandler(MazeFinished);

            this.KeyPress += new KeyPressEventHandler(PlayerKeyPress);

        }
        private void PlayerKeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            for (int i = 0; i < 4; i++)
                if (keyChar == _appSettings.ControlSettings.Movement[i])
                    _maze.MakeMove((Direction)i);

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

        }

        private void SetupMenuStrip(MenuStrip menuStrip)
        {
            // populate menu strip

            ToolStripMenuItem FileMenu = new ToolStripMenuItem("File");
            FileMenu.DropDownItems.Add("Temp");
            menuStrip.Items.Add(FileMenu);

            ToolStripMenuItem HintMenu = new ToolStripMenuItem("Hint");
            HintMenu.DropDownItems.Add("Show Partial Solution").Click += new EventHandler((sender, e) => _maze.ShowHint());
            HintMenu.DropDownItems.Add("Show Full Solution").Click += new EventHandler((sender, e) => _maze.ShowSolution());
            menuStrip.Items.Add(HintMenu);

            ToolStripMenuItem HelpMenu = new ToolStripMenuItem("Help");
            HelpMenu.DropDownItems.Add("Temp");
            menuStrip.Items.Add(HelpMenu);
        }

        private void MazeFinished(Maze maze, MazeFinishedEventArgs e)
        {
            int MESSAGEBOX_RATIO = 3;

            // Show player stats in messag eform
            Form mazeStatsForm = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                ShowInTaskbar = false,
                Size = new Size(this.Size.Width / MESSAGEBOX_RATIO, this.Size.Height / MESSAGEBOX_RATIO),
                Text = "Stats",
            };
            TableLayoutPanel statsTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
            };
            statsTable.ColumnStyles.Clear();
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            statsTable.RowStyles.Clear();
            MyList<(string, string)> Stats = e.GetStats();
            for (int i = 0; i < Stats.Count; i++)
            {
                (string, string) StatPair = Stats[i];
                statsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / Stats.Count));
                Label StatName = new Label() { Text = StatPair.Item1, Dock = DockStyle.Fill };
                statsTable.Controls.Add(StatName, 0, i);

                Label StatValue = new Label() { Text = StatPair.Item2, Dock = DockStyle.Fill };
                statsTable.Controls.Add(StatValue, 1, i);

            }
            mazeStatsForm.Controls.Add(statsTable);

            mazeStatsForm.ShowDialog();
            ReturnToMenu(this, new EventArgs());

        }

        private void ReturnToMenu(object sender, EventArgs e)
        {
            this.Hide();
            _menuForm.Show();
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
                Console.WriteLine("Error raised: {0}", e.GetReason());
                MessageBox.Show("Error raised: {0}", e.GetReason());
            }
        }

    }

}
