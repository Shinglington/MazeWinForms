using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public static class MyFormMethods
    {
        public static void ResizeLabelText(object sender, EventArgs e)
        {
            double SCALING = 0.9;
            // Resizes labels in table layout panel cells to maximise font size
            Label label = sender as Label;
            Size stringSize = TextRenderer.MeasureText(label.Text, label.Font);

            // Find width and height of cell that label is located in
            TableLayoutPanel Container = label.Parent as TableLayoutPanel;
            TableLayoutPanelCellPosition Pos = Container.GetCellPosition(label);
            double cellHeight = Container.GetRowHeights()[Pos.Row] - Container.Padding.Top - Container.Padding.Bottom;
            double cellWidth = Container.GetColumnWidths()[Pos.Column] - Container.Padding.Right - Container.Padding.Left;

            // while available area bigger than required area, make text bigger
            while (stringSize.Height < cellHeight * SCALING && stringSize.Width < cellWidth * SCALING)
            {
                // resize font until text fits
                label.Font = new Font(label.Font.FontFamily, label.Font.Size * 1.05F);
                stringSize = TextRenderer.MeasureText(label.Text, label.Font);
            }

            // while available area smaller than required area, make text smaller
            while (stringSize.Height > cellHeight * SCALING || stringSize.Width > cellWidth * SCALING)
            {
                // resize font until text fits
                label.Font = new Font(label.Font.FontFamily, label.Font.Size * 0.95F);
                stringSize = TextRenderer.MeasureText(label.Text, label.Font);
            }
        }

        public static Padding ComputePadding(TableLayoutPanel table, int minimumPadding)
        {
            int minPad = minimumPadding;
            Rectangle tableArea = table.ClientRectangle;
            tableArea.Inflate(-minPad, -minPad);

            int cellWidth = tableArea.Width / table.ColumnCount;
            int cellHeight = tableArea.Height / table.RowCount;

            int xExcess = tableArea.Width - (cellWidth * table.ColumnCount);
            int yExcess = tableArea.Height - (cellHeight * table.RowCount);

            Padding padding = new Padding(
                minPad + (xExcess / 2),
                minPad + (yExcess / 2),
                minPad + (xExcess / 2) + (xExcess % 2),
                minPad + (yExcess / 2) + (yExcess % 2)
                );
            return padding;
        }


        public static List<Control> GetAllControls(Control container, Type controlType)
        {
            List<Control> ControlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                List<Control> ChildControls = GetAllControls(c, controlType);
                // Add found child controls
                foreach (Control child in ChildControls)
                {
                    ControlList.Add(child);
                }
                // Add current control if it is of the required type
                if (c.GetType() == controlType)
                {
                    ControlList.Add(c);
                }
            }
            return ControlList;
        }
    }
}
