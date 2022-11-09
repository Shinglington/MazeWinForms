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
        public SettingsForm SettingsForm
        {
            get
            {
                if (_settingsForm == null)
                {
                    _settingsForm = new SettingsForm(this);
                }
                else if (_settingsForm.IsDisposed)
                {
                    _settingsForm = new SettingsForm(this);
                }
                return _settingsForm;
            }
        }
        public MenuForm()
        {
            InitializeComponent();
            SetupEvents();
        }
        private void SetupEvents()
        {
            btn_start.Click += new EventHandler(GoToSettings);
        }

        private void GoToSettings(object sender, EventArgs e)
        {
            this.Hide();
            SettingsForm.Show();
        }

    }
}
