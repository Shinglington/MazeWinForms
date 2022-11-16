using MazeFormsClasses;
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
                    _settingsForm = new SettingsForm(this);
                return _settingsForm;
            }
        }
        public MenuForm()
        {
            InitializeComponent();
            SetupAttributes();
            SetupEvents();
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
            this.FormClosed += new FormClosedEventHandler(AppClosed);
        }

        private void GoToSettings(object sender, EventArgs e)
        {
            this.Hide();
            SettingsForm.Show();
        }

        private void AppClosed(object sender, EventArgs e)
        {
            _settingsManager.SaveConfig();
        }

    }
}
