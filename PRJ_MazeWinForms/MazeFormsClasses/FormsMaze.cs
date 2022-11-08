﻿using MazeConsole;
using System.Drawing;
using System.Windows.Forms;

namespace PRJ_MazeWinForms.MazeFormsClasses
{
    class FormsMaze : Maze
    {
        private MazeDisplay _display;
        private TableLayoutPanel _container;

        public FormsMaze(MazeSettings Settings, TableLayoutPanel Container) : base(Settings)
        {
            SetupAttributes(Container);
        }

        public FormsMaze(int height, int width, string algorithm, TableLayoutPanel Container, bool ShowGeneration = false)
            : base(height, width, algorithm, ShowGeneration)
        {
            SetupAttributes(Container);
        }


        private void SetupAttributes(TableLayoutPanel Container)
        {
            _display = new MazeDisplay(Color.Black, Color.White, Color.Green, Color.Red);
            _container = Container;
        }

        public void GetDisplay()
        {

            _container.RowStyles.Clear();
            _container.ColumnStyles.Clear();
            for (int row = 0; row < Height; row++)
            {
                _container.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / Height));
            }
            for (int col = 0; col < Width; col++)
            {
                _container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / Width));
            }

            Node[,] nodes = _graph.GetNodes();
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Node node = nodes[col, row];
                    Panel Cell = new Panel() { Parent = _container, Dock = DockStyle.Fill, Margin = new Padding(0) };
                    _container.SetCellPosition(Cell, new TableLayoutPanelCellPosition(col, row));
                    Color cell_colour = Color.White;
                    if (node == StartNode)
                    {
                        cell_colour = Color.Green;
                    }
                    else if (node == EndNode)
                    {
                        cell_colour = Color.Red;
                    }
                    Cell.Paint += new PaintEventHandler((sender, e) => _display.PaintNode(sender, e, node, cell_colour));
                }
            }
        }
    }
}


