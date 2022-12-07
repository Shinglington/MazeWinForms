
using MazeClasses;
using MazeFormsClasses;
using MyDataStructures;
using PRJ_MazeWinForms.Authentication;
using PRJ_MazeWinForms.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {

        // Controls
        private MenuForm _menuForm;
        private TableLayoutPanel _tbl_formPanel;
        private TableLayoutPanel _tbl_mazePanel;
        private MenuStrip _menuStrip;

        // Classes
        private WinFormsMaze _maze;
        private AppSettings _appSettings;

        public MazeForm(MenuForm menuForm, MazeSettings MazeSettings, AppSettings AppSettings)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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
            char keyChar = Convert.ToChar(Convert.ToString(e.KeyChar).ToLower());
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
            Form mazeStatsForm = MakeStatsTable(e);
            DialogResult result = mazeStatsForm.ShowDialog();
            if (result == DialogResult.Yes)
            {
                LogHelper.Log("User requested to save");
                if (_menuForm.LoginForm.CurrentUser == null)
                {
                    MessageBox.Show("You need to login");
                    _menuForm.LoginForm.ShowDialog();
                }
                if (_menuForm.LoginForm.CurrentUser != null)
                {
                    DatabaseHelper dbHelper = new DatabaseHelper();
                    dbHelper.AddScore(_menuForm.LoginForm.CurrentUser, CalculateScore(maze, e));
                    MessageBox.Show("Saved score");
                    LogHelper.Log("Saved score");
                }
                else
                {
                    MessageBox.Show("You didn't log in");
                    LogHelper.Log("Login window was closed, login cancelled");
                }
            }
            mazeStatsForm.Dispose();
            ReturnToMenu(this, new EventArgs());

        }

        private int CalculateScore(Maze maze, MazeFinishedEventArgs e)
        {
            if (e.SolutionUsed)
                return 0;
           
            // base score is maximum score for given maze height, width
            int base_score = maze.Height * maze.Width * 1000;
            int extra_moves = Math.Max(maze.Solution.Count - e.MoveCount, 0);
            int seconds = (int)(e.TimeTaken);

            int time_penalty = (base_score / 1000) * seconds;
            int moves_penalty = (base_score / 1000) * extra_moves;

            // Divide the base - penalties by the hintcount + 1
            int final_score = ((base_score) - time_penalty - extra_moves) / (e.HintCount + 1);
            if (final_score < 0)
            {
                final_score = 0;
            }
            return final_score;

        }

        private Form MakeStatsTable(MazeFinishedEventArgs e)
        {
            int MESSAGEBOX_RATIO = 3;

            // Show player stats in message form
            Form mazeStatsForm = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                ShowInTaskbar = false,
                Size = new Size(this.Size.Width / MESSAGEBOX_RATIO, this.Size.Height / MESSAGEBOX_RATIO),
                Text = "Stats",
            };
            // Form table
            TableLayoutPanel layoutTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill
            };
            layoutTable.ColumnStyles.Clear();
            layoutTable.RowStyles.Clear();
            layoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTable.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            layoutTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mazeStatsForm.Controls.Add(layoutTable);

            // Stats table
            TableLayoutPanel statsTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill
            };
            statsTable.ColumnStyles.Clear();
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            statsTable.RowStyles.Clear();
            MyList<(string, string)> Stats = e.GetStats();
            Stats.Add(("Total Score", CalculateScore(_maze, e).ToString()));
            for (int i = 0; i < Stats.Count; i++)
            {
                (string, string) StatPair = Stats[i];
                statsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / Stats.Count));
                Label StatName = new Label() { Text = StatPair.Item1, Dock = DockStyle.Fill };
                statsTable.Controls.Add(StatName, 0, i);

                Label StatValue = new Label() { Text = StatPair.Item2, Dock = DockStyle.Fill };
                statsTable.Controls.Add(StatValue, 1, i);

            }
            layoutTable.Controls.Add(statsTable);
            layoutTable.SetCellPosition(statsTable, new TableLayoutPanelCellPosition(0, 0));
            layoutTable.SetColumnSpan(statsTable, 2);


            // Save and Ok buttons
            Button saveButton = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Save"
            };
            layoutTable.Controls.Add(saveButton);
            layoutTable.SetCellPosition(saveButton, new TableLayoutPanelCellPosition(0, 1));

            Button continueButton = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Continue"
            };
            layoutTable.Controls.Add(continueButton);
            layoutTable.SetCellPosition(continueButton, new TableLayoutPanelCellPosition(1, 1));

            continueButton.Click += new EventHandler((object sender, EventArgs _) => mazeStatsForm.DialogResult = DialogResult.No);
            saveButton.Click += new EventHandler((object sender, EventArgs _) => mazeStatsForm.DialogResult = DialogResult.Yes);
            return mazeStatsForm;
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
