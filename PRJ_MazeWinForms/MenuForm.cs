using MazeFormsClasses;
using PRJ_MazeWinForms.Authentication;
using System;
using System.Windows.Forms;

namespace PRJ_MazeWinForms
{
    public partial class MenuForm : Form
    {
        private SettingsForm _settingsForm;
        private LoginForm _loginForm;
        private HighscoresForm _highscoreForm;
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
        public LoginForm LoginForm
        {
            get
            {
                if (_loginForm == null || _loginForm.IsDisposed)
                    _loginForm = new LoginForm(this);
                return _loginForm;
            }
        }

        public HighscoresForm HighscoresForm
        {
            get
            {
                if (_highscoreForm == null || _highscoreForm.IsDisposed)
                    _highscoreForm = new HighscoresForm(this);
                return _highscoreForm;
            }
        }

        public MenuForm()
        {
            InitializeComponent();
            SetupAttributes();
            SetupEvents();

            Matrix A = new Matrix(new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix B = new Matrix(new int[,] { { 10, 11 }, { 20, 21 }, { 30, 31 } });

            Matrix C = A * B;
            Console.WriteLine(C.ToString());

            A = new Matrix(new int[,] { { 8, 15 }, { 7, -3 } });
            B = new Matrix(new int[,] { { 2, -3, 1}, { 2, 0, -1 }, { 1, 4, 5 } });
            Console.WriteLine(A.GetDeterminant());
            Console.WriteLine(B.GetDeterminant());

            A = new Matrix(new int[,] { { 6, 24, 1 }, { 13, 16, 10 }, { 20, 17, 15 } });
            Console.WriteLine(A.GetInverse(26).ToString());

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
            btn_highscores.Click += new EventHandler(GoToHighscore);
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
            LoginForm.Show();
        }

        private void GoToHighscore(object sender, EventArgs e)
        {
            if (LoginForm.CurrentUser != null)
            {
                this.Hide();
                HighscoresForm.Show();
            }
            else
            {
                MessageBox.Show("You must be logged in to view this");
            }
        }

        private void AppClosed(object sender, EventArgs e)
        {
            _settingsManager.SaveConfig();
        }



        private void Login_Click(object sender, EventArgs e)
        {

        }

        private void menuLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
