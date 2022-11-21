using PRJ_MazeWinForms.Authentication;
using PRJ_MazeWinForms.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public partial class LoginForm : Form
    {
        private Form _caller;
        private DatabaseHelper _databaseHelper;
        private TextBox _usernameField;
        private TextBox _passwordField;
        private Button _loginButton;
        public LoginForm(object Caller)
        {
            InitializeComponent();
            SetupControls();
            SetupEvents();
            _databaseHelper = new DatabaseHelper();
            _caller = (Form) Caller;
        }

        private void SetupControls()
        {
            //
            // panel setup
            //
            TableLayoutPanel panel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                Parent = this
            };
            panel.RowStyles.Clear();
            panel.ColumnStyles.Clear();

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 5; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            }
            //
            // Label title setup
            //
            Label title = new Label()
            {
                Dock = DockStyle.Bottom,
                Text = "Login",
                Parent = panel
            };
            panel.SetCellPosition(title, new TableLayoutPanelCellPosition(0, 1));


            //
            // Textbox setups
            //
            _usernameField = new TextBox()
            {
                Dock = DockStyle.Bottom,
                Parent = panel
                
            };
            panel.SetCellPosition(_usernameField, new TableLayoutPanelCellPosition(0, 2));

            _passwordField = new TextBox()
            {
                Dock = DockStyle.Top,
                Parent = panel
            };
            panel.SetCellPosition(_passwordField, new TableLayoutPanelCellPosition(0, 3));

            //
            // Other buttons
            //
            _loginButton = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Login",
                Parent = panel
            };
            panel.SetCellPosition(_loginButton, new TableLayoutPanelCellPosition(0, 4));
        }

        private void SetupEvents()
        {
            _loginButton.Click += new EventHandler(LoginButtonPress);
        }

        private void LoginButtonPress(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (_databaseHelper.Authenticate(_usernameField.Text, _passwordField.Text))
            {
                MessageBox.Show("Success");
                LogHelper.Log("Login form success login");
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
                LogHelper.Log("Login form unsuccessful login");
            }
        }




    }
}
