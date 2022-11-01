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
            _tbl_mazePanel = SetupMazePanel();
        }


        private TableLayoutPanel SetupMazePanel()
        {
            TableLayoutPanel MazePanel = new TableLayoutPanel() { Dock = DockStyle.Fill, Parent = this };
            Console.WriteLine("Width is {0}", _mazeSettings.Width);
            Console.WriteLine("Height is {0}", _mazeSettings.Height);
            Console.WriteLine("Algorithm is {0}", _mazeSettings.Algorithm);
            Console.Write(_maze.DisplayMaze());
            return MazePanel;
        }
    }
}
