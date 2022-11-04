using System;
using System.Windows.Forms;
using MazeConsole;

namespace PRJ_MazeWinForms
{
    public partial class MazeInterface : UserControl
    {
        private Maze _maze;
        private Settings _mazeSettings;

        private TableLayoutPanel _mazePanel;
        public MazeInterface(Settings MazeSettings, bool ShowGeneration = false)
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
            if (!ShowGeneration)
            {
                _maze = new Maze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.Algorithm);
                _maze.FormsDisplay(TableContainer);
                Console.Write(_maze.ConsoleDisplay());
            }

        }
    }
}
