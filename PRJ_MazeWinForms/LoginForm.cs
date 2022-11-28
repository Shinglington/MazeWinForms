using MazeClasses;
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
        enum FormMode
        {
            Login,
            Register
        }
        private Form _caller;
        private DatabaseHelper _databaseHelper;
        private TextBox _usernameField;
        private TextBox _passwordField;
        private Button _confirmButton;

        private User _currentUser;

        private FormMode mode;
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
            mode = FormMode.Login;
        }
        private void SetupAttributes()
        {
            CurrentUser = null;
            _usernameField = username_field;
            _passwordField = password_field;
            _confirmButton = btn_confirm;
        }

        private void SetupEvents()
        {
            _confirmButton.Click += new EventHandler(LoginPress);
            btn_back.Click += new EventHandler(BackButtonPress);
            btn_switchMode.Click += new EventHandler(SwapFormMode);
        }

        private void LoginPress(object sender, EventArgs e)
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

        private void RegisterPress(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (confirmpass_field.Text != password_field.Text)
            {
                MessageBox.Show("Passwords don't match");
                LogHelper.Log("Password fields don't match");
                return;
            }

            if (_databaseHelper.UserExists(username_field.Text))
            {
                MessageBox.Show("Username already taken");
                LogHelper.Log(String.Format("Username {0} already exists", username_field.Text));
                return;
            }

            if (_databaseHelper.AddUser(username_field.Text, password_field.Text))
            {
                MessageBox.Show("Successfully registered");
                LogHelper.Log(String.Format("Registered username {0}", username_field.Text));
                return;
            }
            else
            {
                MessageBox.Show("Registration Failed");
            }



        }
        private void BackButtonPress(object sender, EventArgs e)
        {
            this.Hide();
            _caller.Show();
        }

        private void SwapFormMode(object sender, EventArgs e)
        {
            switch (mode)
            {
                case FormMode.Login:
                    SwapToRegister();
                    break;
                case FormMode.Register:
                    SwapToLogin();
                    break;
                default:
                    LogHelper.ErrorLog("FormMode couldn't be identified in Login form");
                    break;
            }

        }

        private void SwapToLogin()
        {
            mode = FormMode.Login;
            lbl_title.Text = "Login";
            MyFormMethods.ResizeLabelText(lbl_title, new EventArgs());
            _confirmButton.Text = "Login";

            btn_switchMode.Text = "Register";

            _confirmButton.Click -= RegisterPress;
            _confirmButton.Click += LoginPress;

            lbl_confirmPass.Visible = false;
            confirmpass_field.Visible = false;
            confirmpass_field.Enabled = false;
            confirmpass_field.Text = "";

            password_field.Text = "";
            username_field.Text = "";

        }
        private void SwapToRegister()
        {
            mode = FormMode.Register;
            lbl_title.Text = "Register";
            MyFormMethods.ResizeLabelText(lbl_title, new EventArgs());
            _confirmButton.Text = "Register";

            btn_switchMode.Text = "Login";

            _confirmButton.Click -= LoginPress;
            _confirmButton.Click += RegisterPress;

            lbl_confirmPass.Visible = true;
            confirmpass_field.Visible = true;
            confirmpass_field.Enabled = true;
            confirmpass_field.Text = "";

            password_field.Text = "";
            username_field.Text = "";

        }



    }
}
