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
    public partial class SettingsForm : Form
    {
        private MenuForm ParentMenu;
        public SettingsForm(MenuForm M)
        {
            InitializeComponent();
            // Indicates which form originally called the settings form
            ParentMenu = M;

            SetupEvents();
        }

        private void SetupEvents()
        {
            this.FormClosed += new FormClosedEventHandler(ReturnToMenu);
            btn_back.Click += new EventHandler(ReturnToMenu);
        }

        private void ReturnToMenu(object sender, EventArgs e)
        {
            this.Hide();
            ParentMenu.Show();
        }
    }

    public partial class ButtonState
    {
        private Button _button;
        private bool _state;
        public ButtonState(Button b, bool initialState = false)
        {
            _button = b;
            _state = initialState;
        }

        public void OnClick()
        {
            _state = !_state;
        }

        private void ShowBasicSettings()
        {

        }

        private void ShowAdvancedSettings()
        {

        }
    }

}
