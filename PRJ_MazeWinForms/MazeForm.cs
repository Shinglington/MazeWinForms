using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using MazeConsole;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {
        private TableLayoutPanel _tbl_mazePanel;

        private Settings _mazeSettings;
        private Maze _maze;


        public MazeForm(Settings MazeSettings)
        {
            InitializeComponent();

            _mazeSettings = MazeSettings;
            _maze = new Maze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.Algorithm);
            _tbl_mazePanel = GenerateMaze();
        }


        private TableLayoutPanel GenerateMaze()
        {
            TableLayoutPanel MazePanel = new TableLayoutPanel() { Parent = this, Dock = DockStyle.Fill };
            _maze.FormsDisplay(MazePanel);
            Console.Write(_maze.ConsoleDisplay());
            return MazePanel;
        }
    }
}
