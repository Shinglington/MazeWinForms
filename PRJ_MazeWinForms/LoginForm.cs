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

        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            private set
            {
                _currentUser = value;

                if (_currentUser == null)
                {
                    lbl_signedinas.Text = "Signed in as : " + "\n" + "Guest";
                }
                else
                {
                    lbl_signedinas.Text = "Signed in as : " + "\n" + _currentUser.Username;
                }

            }
        }

        public LoginForm(Form Caller)
        {
            InitializeComponent();
            SetupAttributes();
            SetupEvents();
            _databaseHelper = new DatabaseHelper();
            _caller = Caller;
        }
        private void SetupAttributes()
        {
            CurrentUser = null;
            _usernameField = username_field;
            _passwordField = password_field;
            _loginButton = btn_login;
        }

        private void SetupEvents()
        {
            _loginButton.Click += new EventHandler(LoginButtonPress);
            btn_back.Click += new EventHandler(BackButtonPress);
        }

        private void LoginButtonPress(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CurrentUser = _databaseHelper.Authenticate(_usernameField.Text, _passwordField.Text);
            if (CurrentUser != null)
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

        private void BackButtonPress(object sender, EventArgs e)
        {
            this.Hide();
            _caller.Show();
        }






    }
}
