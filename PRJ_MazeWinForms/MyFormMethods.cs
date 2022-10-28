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
            Label label = sender as Label;
            Size stringSize = TextRenderer.MeasureText(label.Text, label.Font);
            int rows = (int)(label.Height / stringSize.Height);

            double areaAvailable = rows * stringSize.Height * label.Width;
            double areaRequired = stringSize.Width * stringSize.Height;

            // if current area bigger than required area, make text bigger
            if (areaAvailable > areaRequired * 1.3)
            {
                while (areaAvailable > areaRequired * 1.3)
                {
                    Console.WriteLine(areaAvailable);
                    // resize font until text fits
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size * 1.1F);
                    stringSize = TextRenderer.MeasureText(label.Text, label.Font);
                    areaRequired = rows * stringSize.Height * label.Width;
                }
            }
            else
            {
                while (areaAvailable < areaRequired * 1.3)
                {
                    // resize font until text fits
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size * 0.9F);
                    stringSize = TextRenderer.MeasureText(label.Text, label.Font);
                    areaRequired = rows * stringSize.Height * label.Width;
                }
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
