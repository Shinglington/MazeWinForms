using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static PRJ_MazeWinForms.MyFormMethods;
namespace PRJ_MazeWinForms
{
    public partial class SettingsForm : Form
    {
        private MenuForm _menu;
        private Label _title;
        private TableLayoutPanel _formPanel;
        private TableLayoutPanel _tbl_basicSettings;
        private TableLayoutPanel _tbl_advSettings;
        private SettingMode _mode;
        enum SettingMode 
        {
            Basic,
            Advanced
        }


        public SettingsForm(MenuForm M)
        {
            InitializeComponent();
            // Indicates which form originally called the settings form
            _menu = M;
            _title = lbl_settings;
            _formPanel = tbl_settingPanel;
            _mode = (SettingMode)0;
            SetupControls();
            SetupEvents();
        }

        private void SetupEvents()
        {
            this.FormClosed += new FormClosedEventHandler(ReturnToMenu);
            btn_back.Click += new EventHandler(ReturnToMenu);
            btn_advSettings.Click += new EventHandler(SwapSettings);
        }

        private void SetupControls()
        {
            SetupBasicSettings();
            SetupAdvancedSettings();
            _tbl_advSettings.Hide();

            foreach (Label l in MyFormMethods.GetAllControls(_formPanel, typeof(Label)))
            {
                MyFormMethods.ResizeLabelText(l, new EventArgs());
            }
        }

        private void ReturnToMenu(object sender, EventArgs e)
        {
            this.Hide();
            _menu.Show();
        }

        private void SwapSettings(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int CurrentMode = (int)_mode;
            button.Text = _mode.ToString() + " Settings";
            _mode = (SettingMode)((CurrentMode + 1) % 2);// go to next mode (mod 2 since enum has 2 values)
            // set button text
            _title.Text = _mode.ToString() + " Settings";

            switch (_mode)
            {
                case SettingMode.Basic:
                    _tbl_advSettings.Hide();
                    _tbl_basicSettings.Show();
                    break;
                case SettingMode.Advanced:
                    _tbl_basicSettings.Hide();
                    _tbl_advSettings.Show();
                    break;
                default:
                    Console.WriteLine("Can't identify Setting mode");
                    break;
            } 

        }





        private void SetupBasicSettings()
        {
            TableLayoutPanel Table = new TableLayoutPanel() { Dock = DockStyle.Fill };
            // Set parent and location
            Table.Parent = _formPanel;
            _formPanel.SetCellPosition(Table, new TableLayoutPanelCellPosition(0, 1));
            // Setup Columns
            Table.ColumnStyles.Clear();
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.9F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.1F));

            // Setup Rows
            Table.RowStyles.Clear();
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.1F));
            int rowCount = Enum.GetValues(typeof(Difficulty)).Length;
            for (int row = 0; row < rowCount; row++)
            {
                Table.RowStyles.Add(new RowStyle(SizeType.Percent, (0.9F / rowCount)));
                // Create label
                Label CurrDifficulty = new Label() { Text = ((Difficulty)(row)).ToString(), Font = new Font("Arial", 10), Dock = DockStyle.Fill };
                CurrDifficulty.Parent = Table;
                Table.SetCellPosition(CurrDifficulty, new TableLayoutPanelCellPosition(0, row + 1));

                // Create RadioButtons
                RadioButton radioButton = new RadioButton() { Dock = DockStyle.Fill };
                radioButton.Parent = Table;
                Table.SetCellPosition(radioButton, new TableLayoutPanelCellPosition(1, row + 1));
            }


            // Header
            Label Header = new Label() { Text = "Difficulty", Font = new Font("Arial", 15), Dock = DockStyle.Fill };
            Header.Parent = Table;
            Table.SetCellPosition(Header, new TableLayoutPanelCellPosition(0, 0));

            _tbl_basicSettings = Table;
        }

        private void SetupAdvancedSettings()
        {
            TableLayoutPanel Table = new TableLayoutPanel() { Dock = DockStyle.Fill };
            // Set parent and location
            Table.Parent = _formPanel;
            _formPanel.SetCellPosition(Table, new TableLayoutPanelCellPosition(0, 1));

            // Setup columns
            Table.ColumnStyles.Clear();
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.7F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0.3F));

            // Setup rows
            Table.RowStyles.Clear();
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.1F));

            // Width
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.3F));
            Label Width = new Label() { Text = "Width", Font = new Font("Arial", 10), Dock = DockStyle.Fill };
            Width.Parent = Table;
            Table.SetCellPosition(Width, new TableLayoutPanelCellPosition(0, 1));


            // Height
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.3F));
            Label Height = new Label() { Text = "Height", Font = new Font("Arial", 10), Dock = DockStyle.Fill };
            Height.Parent = Table;
            Table.SetCellPosition(Height, new TableLayoutPanelCellPosition(0, 2));

            // Algorithm Selection
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.3F));
            Label Algorithm = new Label() { Text = "Algorithm", Font = new Font("Arial", 10), Dock = DockStyle.Fill };
            Algorithm.Parent = Table;
            Table.SetCellPosition(Algorithm, new TableLayoutPanelCellPosition(0, 3));

            _tbl_advSettings = Table;
        }
    }

    public class Settings 
    {
        private int _width;
        private int _height;
        private string _algorithm;

        
        // Different constructors for difficulty parameters and advanced parameters
        public Settings(int width, int height, string algorithm)
        {
            // Advanced
            _width = width;
            _height = height;
            _algorithm = algorithm;

        }

        public Settings(string Difficulty)
        {
            // Basic

        }
            
    }

    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}
