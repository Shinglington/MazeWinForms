using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_MazeWinForms.MazeFormsClasses
{
    public class AppSettings
    {
        public MazeDisplaySettings DisplaySettings { get; set; }

        public AppSettings()
        {
            DisplaySettings = new MazeDisplaySettings();
        }

    }

    public class AppSettingsManager
    {
        private const string DEFAULT_FILENAME = "settings.json";
        private AppSettings _appSettings = null;

        public AppSettings AppSettings
        {
            get
            {
                if (_appSettings == null)
                {
                    LoadConfig();
                }
                return _appSettings;
            }
            set
            {
                _appSettings = value;
            }
        }

        public void LoadConfig()
        {
            _appSettings = new AppSettings();
            DefaultConfig();
        }

        public void SaveConfig()
        {

        }

        private void DefaultConfig()
        {
            _appSettings.DisplaySettings.WallColour = Color.Black;
            _appSettings.DisplaySettings.CellColour = Color.White;
            _appSettings.DisplaySettings.StartColour = Color.Green;
            _appSettings.DisplaySettings.EndColour = Color.DarkRed;
            _appSettings.DisplaySettings.PlayerColour = Color.Blue;
            _appSettings.DisplaySettings.HintColour = Color.Red;

            _appSettings.DisplaySettings.MinimumPadding = 5;



        }
    }

    public class MazeDisplaySettings 
    {
        public Color WallColour { get; set; }
        public Color CellColour { get; set; }
        public Color StartColour { get; set; }
        public Color EndColour { get; set; }
        public Color PlayerColour { get; set; }
        public Color HintColour { get; set; }

        public int MinimumPadding { get; set; }


        

        
    }





}
