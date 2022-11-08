
using System;
using System.Drawing;
using System.Windows.Forms;

using MazeConsole;
using MazeConsole.MyDataStructures;
using PRJ_MazeWinForms.MazeFormsClasses;

namespace PRJ_MazeWinForms
{
    public partial class MazeForm : Form
    {

        // Controls
        private TableLayoutPanel _tbl_formPanel;
        private TableLayoutPanel _tbl_mazePanel;
        private MenuStrip _menuStrip;

        // Classes
        private FormsMazeInterface _interface;


        public MazeForm(MazeSettings MazeSettings)
        {
            InitializeComponent();
            CreateControls();
            _interface = new FormsMazeInterface(MazeSettings, _tbl_mazePanel, _menuStrip);

        }
        private void CreateControls()
        {
            // Create form table panel
            _tbl_formPanel = new TableLayoutPanel()
            {
                Parent = this,
                Dock = DockStyle.Fill
            };
            _tbl_formPanel.RowStyles.Clear();
            _tbl_formPanel.ColumnStyles.Clear();

            _tbl_formPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            _tbl_formPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));

            // Create maze panel
            _tbl_mazePanel = new TableLayoutPanel()
            {
                Parent = _tbl_formPanel,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)

            };
            _tbl_formPanel.SetCellPosition(_tbl_mazePanel, new TableLayoutPanelCellPosition(0, 1));

            _menuStrip = new MenuStrip()
            {
                Parent = _tbl_formPanel,
                Dock = DockStyle.Fill,
            };
            _tbl_formPanel.SetCellPosition(_menuStrip, new TableLayoutPanelCellPosition(0, 0));
            SetupMenuStrip(_menuStrip);


        }

        private void SetupMenuStrip(MenuStrip menuStrip)
        {
            // Populate menu strip
            foreach(object item in Enum.GetValues(typeof(MyMenuItem)))
            {
                menuStrip.Items.Add(new ToolStripMenuItem(item.ToString()));
            }
        }

    }

    public enum MyMenuItem
    {
        File,
        Hint,
        Help
    }
}
