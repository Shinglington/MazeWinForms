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
        private SettingsForm Settings;
        public MenuForm()
        {
            InitializeComponent();
            Settings = new SettingsForm(this);
            SetupEvents();
        }
        private void SetupEvents()
        {
            btn_start.Click += new EventHandler(GoToSettings);
        }

        private void GoToSettings(object sender, EventArgs e)
        {
            this.Hide();
            Settings.Show();
        }

    }
}
