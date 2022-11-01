using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {
        private Settings _mazeSettings;
        private TableLayoutPanel _tbl_mazePanel;

        public MazeForm(Settings MazeSettings)
        {
            InitializeComponent();

            _mazeSettings = MazeSettings;
            _tbl_mazePanel = SetupMazePanel();
        }


        private TableLayoutPanel SetupMazePanel()
        {
            TableLayoutPanel MazePanel = new TableLayoutPanel() { Dock = DockStyle.Fill, Parent = this };
            Console.WriteLine("Width is {0}", _mazeSettings.Width);
            Console.WriteLine("Height is {0}", _mazeSettings.Height);
            Console.WriteLine("Algorithm is {0}", _mazeSettings.Algorithm);

            return MazePanel;
        }
    }
}
