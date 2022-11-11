using MazeConsole;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRJ_MazeWinForms.MazeFormsClasses
{
    class MazeDisplay
    {
        private MazeDisplaySettings _displaySettings;
        private float WALL_RATIO;
        public MazeDisplay(MazeDisplaySettings DisplaySettings, int WallRatio = 6)
        {
            _displaySettings = DisplaySettings;
            WALL_RATIO = WallRatio;
        }

        public void PaintNode(object sender, PaintEventArgs e, Node node, bool IsStartNode = false, bool IsEndNode = false)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(_displaySettings.WallColour);

            // Draw walls
            if (node.NorthNode == null)
            {
                g.FillRectangle(brush, 0, 0, cell.Width, cell.Height / WALL_RATIO);
            }
            if (node.EastNode == null)
            {
                g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, 0, cell.Width / WALL_RATIO, cell.Height);
            }
            if (node.SouthNode == null)
            {
                g.FillRectangle(brush, 0, cell.Height - cell.Height / WALL_RATIO, cell.Width, cell.Height / WALL_RATIO);
            }
            if (node.WestNode == null)
            {
                g.FillRectangle(brush, 0, 0, cell.Width / WALL_RATIO, cell.Height);
            }

            // Draw wall corners
            g.FillRectangle(brush, 0, 0, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);
            g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, 0, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);
            g.FillRectangle(brush, cell.Width - cell.Width / WALL_RATIO, cell.Height - cell.Height / WALL_RATIO, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);
            g.FillRectangle(brush, 0, cell.Height - cell.Height / WALL_RATIO, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO);

            // Colour cell
            brush = new SolidBrush(_displaySettings.CellColour);
            if (IsStartNode)
            {
                Console.WriteLine("Start node at {0},{1}", node.Location.X, node.Location.Y);
                brush = new SolidBrush(_displaySettings.StartColour);
            }
            else if (IsEndNode)
            {
                brush = new SolidBrush(_displaySettings.EndColour);
            }
            g.FillRectangle(brush, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO, cell.Width - (2 * cell.Width / WALL_RATIO), cell.Height - (2 * cell.Height / WALL_RATIO));

        }

        public void PaintPlayer(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(_displaySettings.PlayerColour);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / (WALL_RATIO);
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
        public void PaintHint(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(_displaySettings.HintColour);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / (WALL_RATIO);
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
    }
}
