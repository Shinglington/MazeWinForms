using MazeFormsClasses;
using PRJ_MazeWinForms.Authentication;
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
    public partial class MenuForm : Form
    {
        private SettingsForm _settingsForm;
        private AppSettingsManager _settingsManager;
        private AppSettings _appSettings;
        public SettingsForm SettingsForm
        {
            get
            {
                if (_settingsForm == null || _settingsForm.IsDisposed)
                    _settingsForm = new SettingsForm(this, _appSettings);
                return _settingsForm;
            }
        }
        public MenuForm()
        {
            InitializeComponent();
            SetupAttributes();
            SetupEvents();
            DatabaseHelper database = new DatabaseHelper();
            database.AddUser("Bob", "Jeff");
            database.AddUser("Jimmy", "Joey");
            database.ShowDatabase();
            database.Authenticate("Bob", "Jeff");
            database.Authenticate("Joey", "Jeff");

        }

        private void SetupAttributes()
        {
            _settingsManager = new AppSettingsManager();
            _settingsManager.LoadConfig();
            _appSettings = _settingsManager.AppSettings;
        }
        private void SetupEvents()
        {
            btn_start.Click += new EventHandler(GoToSettings);
            btn_login.Click += new EventHandler(GoToLogin);
            this.FormClosed += new FormClosedEventHandler(AppClosed);
        }

        private void GoToSettings(object sender, EventArgs e)
        {
            this.Hide();
            SettingsForm.Show();
        }

        private void GoToLogin(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(this);
            loginForm.Show();
        }

        private void AppClosed(object sender, EventArgs e)
        {
            _settingsManager.SaveConfig();
        }

        private void Login_Click(object sender, EventArgs e)
        {

        }
    }
}
