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
        private Color START_COLOUR;
        private Color END_COLOUR;

        private Color WALL_COLOUR;
        private Color BACK_COLOUR;

        private Color SOLUTION_COLOUR;

        private const float WALL_RATIO = 6;

        public MazeDisplay(Color wall_colour, Color back_colour, Color start_colour, Color end_colour)
        {
            WALL_COLOUR = wall_colour;
            BACK_COLOUR = back_colour;

            START_COLOUR = start_colour;
            END_COLOUR = end_colour;
            SOLUTION_COLOUR = Color.Red;
        }

        public void PaintNode(object sender, PaintEventArgs e, Node node, Color cell_colour)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(WALL_COLOUR);

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
            brush = new SolidBrush(cell_colour);
            g.FillRectangle(brush, cell.Width / WALL_RATIO, cell.Height / WALL_RATIO, cell.Width - (2 * cell.Width / WALL_RATIO), cell.Height - (2 * cell.Height / WALL_RATIO));

        }

        public void HighlightCell(object sender, PaintEventArgs e)
        {
            Panel cell = sender as Panel;
            Graphics g = e.Graphics;

            Brush brush = new SolidBrush(SOLUTION_COLOUR);
            Point midpoint = new Point(cell.Width / 2, cell.Height / 2);
            float radius = Math.Min(cell.Width, cell.Height) / (WALL_RATIO);
            g.FillEllipse(brush, midpoint.X - radius, midpoint.Y - radius, radius * 2, radius * 2);
        }
    }
}
