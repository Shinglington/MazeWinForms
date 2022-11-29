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
    public partial class HighscoresForm : Form
    {
        enum viewMode
        {
            global,
            personal
        }


        private MenuForm _menu;
        private DatabaseHelper _dbHelper;
        private viewMode databaseVisibility;
        public HighscoresForm(MenuForm menu)
        {
            InitializeComponent();
            _menu = menu;
            _dbHelper = new DatabaseHelper();
            SetupEvents();
            ShowAllScores();
            databaseVisibility = viewMode.global;
        }

        private void SetupEvents()
        {
            this.FormClosed += new FormClosedEventHandler(ReturnToMenu);
            this.btn_back.Click += new EventHandler(ReturnToMenu);
            this.btn_toggleGlobal.Click += new EventHandler((sender, e) => SwitchVisibilityMode());
        }

        private void ReturnToMenu(object sender, EventArgs e)
        {
            this.Hide();
            _menu.Show();
        }

        private void ShowAllScores()
        {
            ShowScores(_dbHelper.GetAllScores());
        }
        private void ShowPersonalScores()
        {
            ShowScores(_dbHelper.GetUserScores(_menu.LoginForm.CurrentUser));
        }

        private void ShowScores(DataSet ds)
        {
            highscoresGrid.DataSource = null;
            highscoresGrid.DataSource = ds.Tables[0];
        }



        private void SwitchVisibilityMode()
        {
            switch (databaseVisibility) 
            {
                case viewMode.global:
                    SwitchToPersonalView();
                    break;
                case viewMode.personal:
                    SwitchToGlobalView();
                    break;
                default:
                    LogHelper.ErrorLog("Could not identify viewmode in Highscore form");
                    break;
            }

        }

        private void SwitchToGlobalView()
        {
            databaseVisibility = viewMode.global;
            btn_toggleGlobal.Text = "Switch to personal view";
            ShowAllScores();
        }

        private void SwitchToPersonalView()
        {
            databaseVisibility = viewMode.personal;
            btn_toggleGlobal.Text = "Switch to global view";
            ShowPersonalScores();
        }
    }
}
