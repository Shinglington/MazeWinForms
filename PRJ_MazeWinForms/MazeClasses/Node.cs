using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeConsole

{
    class Node
    {
        public (int, int) Location { get; private set; }

        public Node SouthNode { get; private set; }
        public Node EastNode { get; private set; }
        public bool Visited { get; private set; }
        public Node(int x, int y)
        {
            Location = (x, y);
            Visited = false;

            SouthNode = null;
            EastNode = null;
        }

        public void UpdateVisited(bool newValue)
        {
            Visited = newValue;
        }

        public bool UpdateSouthEdge(Node node)
        {
            bool success = false;
            if (node != null) // If node exists
            {
                if (node.Location.Item1 == Location.Item1) // If x coord is the same
                {
                    SouthNode = node;
                    success = true;
                }
            }
            else // If node doesnt exist (Updated to remove south edge)
            {
                SouthNode = null;
                success = true;
            }
            return success;
        }
        public bool UpdateEastEdge(Node node)
        {
            bool success = false;
            if (node != null) // If node exists
            {
                if (node.Location.Item2 == Location.Item2) // If y coord is the same
                {
                    EastNode = node;
                    success = true;
                }
            }
            else // If node doesnt exist (Updated to remove south edge)
            {
                EastNode = null;
                success = true;
            }
            return success;
        }

        public void PaintNode(TableLayoutPanel MazePanel)
        {
            Panel PanelNode = new Panel() { Parent = MazePanel, Dock = DockStyle.Fill };
            MazePanel.SetCellPosition(PanelNode, new TableLayoutPanelCellPosition(Location.Item1, Location.Item2));

            Label TestLabel = new Label();
            TestLabel.Location = new Point(10, 10);
            TestLabel.Text = String.Format("{0},{1}", Location.Item1, Location.Item2);

            PanelNode.Controls.Add(TestLabel);



            Graphics g = PanelNode.CreateGraphics();
            Pen p = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Blue);
            g.DrawRectangle(p, 0, 0, 10, 10);
            g.FillRectangle(brush, 0, 0, 10, 10);
            Console.WriteLine("Painted node at {0}, {1}", Location.Item1, Location.Item2);


        }

    }
}
