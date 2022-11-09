using System;
using System.Windows.Forms;
using MazeConsole;
using PRJ_MazeWinForms.MazeFormsClasses;

namespace PRJ_MazeWinForms
{
    public partial class MazeInterface : UserControl
    {
        private Maze _maze;
        private MazeConsole.MazeSettings _mazeSettings;

        private TableLayoutPanel _mazePanel;
        public MazeInterface(MazeSettings MazeSettings, bool ShowGeneration = false)
        {
            InitializeComponent();

            _mazeSettings = MazeSettings;

            SetupMazePanel();
            GenerateMaze(ShowGeneration);
        }

        private void SetupMazePanel()
        {
            // Create maze panel
            _mazePanel = new TableLayoutPanel()
            {
                Parent = this,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

        }


        private void GenerateMaze(bool ShowGeneration)
        {
            _maze = new WinFormsMaze(_mazeSettings, _mazePanel);
            _maze.DisplayConsole();

        }
    }
}
