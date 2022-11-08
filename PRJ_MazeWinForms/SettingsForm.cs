using MazeConsole;
using System;
using System.Drawing;
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

        private bool _showGeneration;


        private const string TEXT_FONT = "Arial";
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
            _showGeneration = false;
            _mode = (SettingMode)0;
            SetupControls();
            SetupEvents();
        }

        private void SetupEvents()
        {
            this.FormClosed += new FormClosedEventHandler(ReturnToMenu);
            btn_back.Click += new EventHandler(ReturnToMenu);

            btn_advSettings.Click += new EventHandler(SwapSettings);


            btn_generate.Click += new EventHandler(GenerateMaze);
        }

        private void SetupControls()
        {
            SetupBasicSettings();
            SetupAdvancedSettings();
            _tbl_advSettings.Hide();

            // Resize labels
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
                Label CurrDifficulty = new Label() { Text = ((Difficulty)(row)).ToString(), Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill };
                CurrDifficulty.Parent = Table;
                Table.SetCellPosition(CurrDifficulty, new TableLayoutPanelCellPosition(0, row + 1));

                // Create RadioButtons
                RadioButton radioButton = new RadioButton() { Dock = DockStyle.Fill };
                radioButton.Parent = Table;
                Table.SetCellPosition(radioButton, new TableLayoutPanelCellPosition(1, row + 1));
            }


            // Header
            Label Header = new Label() { Text = "Difficulty", Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill };
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
            Label Width = new Label() { Text = "Width", Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill, Parent = Table };
            Table.SetCellPosition(Width, new TableLayoutPanelCellPosition(0, 1));
            TextBox WidthField = new TextBox() { Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill, Parent = Table };
            Table.SetCellPosition(WidthField, new TableLayoutPanelCellPosition(1, 1));



            // Height
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.3F));
            Label Height = new Label() { Text = "Height", Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill, Parent = Table };
            Table.SetCellPosition(Height, new TableLayoutPanelCellPosition(0, 2));
            TextBox HeightField = new TextBox() { Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill, Parent = Table };
            Table.SetCellPosition(HeightField, new TableLayoutPanelCellPosition(1, 2));


            // Algorithm Selection
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 0.3F));
            Label Algorithm = new Label() { Text = "Algorithm", Font = new Font(TEXT_FONT, 10), Dock = DockStyle.Fill, Parent = Table };
            Table.SetCellPosition(Algorithm, new TableLayoutPanelCellPosition(0, 3));
            ComboBox AlgSelection = new ComboBox() { Parent = Table, Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            foreach(GenAlgorithm alg in Enum.GetValues(typeof(GenAlgorithm)))
            {
                AlgSelection.Items.Add(alg.ToString());
            }
            Table.SetCellPosition(AlgSelection, new TableLayoutPanelCellPosition(1, 3));

            _tbl_advSettings = Table;
        }

        private MazeSettings GetBasicSettings()
        {
            MazeSettings MazeSettings = null;
            // loop through radio buttons
            for (int row = 1; row <= 3; row++)
            {
                RadioButton radioButton = (RadioButton)_tbl_basicSettings.GetControlFromPosition(1, row);
                Difficulty selectedDifficulty = (Difficulty)(row - 1);
                if (radioButton.Checked)
                {
                    // Use Difficulty overload
                    MazeSettings = new MazeSettings(selectedDifficulty);
                    break;
                }
            }
            return MazeSettings;
        }

        private MazeSettings GetAdvancedSettings()
        {
            MazeSettings MazeSettings = null;
            try
            {
                int width = int.Parse(_tbl_advSettings.GetControlFromPosition(1, 1).Text);
                int height = int.Parse(_tbl_advSettings.GetControlFromPosition(1, 2).Text);
                ComboBox AlgorithmSelector = _tbl_advSettings.GetControlFromPosition(1, 3) as ComboBox;
                GenAlgorithm algorithm = (GenAlgorithm)AlgorithmSelector.SelectedIndex;
                // Defensive programming
                if (AlgorithmSelector.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select an algorithm");
                }
                else if (width < 4 || height < 4)
                {
                    MessageBox.Show("Minimum Maze size is 4*4");
                }
                else
                {
                    MazeSettings = new MazeSettings(width, height, algorithm, _showGeneration);
                } 
            }
            catch
            {
                MessageBox.Show("Error while reading advanced fields");
            }


            return MazeSettings;
        }
        private MazeSettings GetMazeSettings()
        {
            MazeSettings MazeSettings = null;
            switch (_mode)
            {
                case SettingMode.Basic:
                    MazeSettings = GetBasicSettings();
                    break;
                case SettingMode.Advanced:
                    MazeSettings = GetAdvancedSettings();
                    break;
                default:
                    Console.WriteLine("Can't identify Setting mode");
                    break;
            }
            return MazeSettings;
        }

        private void GenerateMaze(object sender, EventArgs e)
        {
            MazeSettings MazeSettings = GetMazeSettings();
            if (MazeSettings != null)
            {
                MazeForm MazeForm = new MazeForm(MazeSettings);
                MazeForm.Show();
                this.Hide();
            }
        }
    }
}
