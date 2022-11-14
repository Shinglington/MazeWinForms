using System.Drawing;

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
            try
            {
                _appSettings = GetDefaultConfig();
            }
            catch
            {
                _appSettings = GetDefaultConfig();
            }

        }

        public void SaveConfig()
        {

        }

        private AppSettings GetDefaultConfig()
        {
            AppSettings DefaultSettings = new AppSettings
            {
                DisplaySettings = new MazeDisplaySettings
                {
                    WallColour = Color.Black,
                    CellColour = Color.White,
                    StartColour = Color.Green,
                    EndColour = Color.DarkRed,
                    PlayerColour = Color.Blue,
                    HintColour = Color.Red,

                    MinimumPadding = 5
                }
            };
            return DefaultSettings;
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
