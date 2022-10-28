using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public static class MyFormMethods
    {
        public static void ResizeLabelText(object sender, EventArgs e)
        {
            // Resizes labels in table layout panel cells to maximise font size
            Label label = sender as Label;
            Size stringSize = TextRenderer.MeasureText(label.Text, label.Font);

            // Find width and height of cell that label is located in
            TableLayoutPanel Container = label.Parent as TableLayoutPanel;
            TableLayoutPanelCellPosition Pos = Container.GetCellPosition(label);
            double cellHeight = Container.GetRowHeights()[Pos.Row];
            double cellWidth = Container.GetColumnWidths()[Pos.Column];

            
            double areaAvailable = cellHeight * cellWidth * 0.5F;
            double areaRequired = stringSize.Height * stringSize.Width;

            // while available area bigger than required area, make text bigger
            while (areaAvailable > areaRequired)
            {
                // resize font until text fits
                label.Font = new Font(label.Font.FontFamily, label.Font.Size * 1.05F);
                stringSize = TextRenderer.MeasureText(label.Text, label.Font);
                areaRequired = stringSize.Height * stringSize.Width;
            }

            // while available area smaller than required area, make text smaller
            while (areaAvailable < areaRequired)
            {
                // resize font until text fits
                label.Font = new Font(label.Font.FontFamily, label.Font.Size * 0.95F);
                stringSize = TextRenderer.MeasureText(label.Text, label.Font);
                areaRequired = stringSize.Height * stringSize.Width;
            }
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
