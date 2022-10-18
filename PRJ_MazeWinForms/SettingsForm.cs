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
            TableLayoutPanel Table = new TableLayoutPanel();
            // Set parent and location
            Table.Parent = _formPanel;
            _formPanel.SetCellPosition(Table, new TableLayoutPanelCellPosition(0, 1));
            // Setup RowCount and ColumnCount
            Table.RowCount = Enum.GetValues(typeof(Difficulty)).Length;
            Table.ColumnCount = 2;
            

            _tbl_basicSettings = Table;
        }

        private void SetupAdvancedSettings()
        {
            TableLayoutPanel Table = new TableLayoutPanel();
            // Set parent and location
            Table.Parent = _formPanel;
            _formPanel.SetCellPosition(Table, new TableLayoutPanelCellPosition(0, 1));
            Table.RowCount = 3;
            Table.ColumnCount = 2;


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
