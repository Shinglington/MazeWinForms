using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace MazeFormsClasses
{
    [Serializable()]
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
        private const string FILENAME = "settings.xml";
        private AppSettings _appSettings = new AppSettings();

        public AppSettings AppSettings
        {
            get
            {
                return _appSettings;
            }
            set
            {
                _appSettings = value;
            }
        }

        public void LoadConfig()
        {
            if (File.Exists(FILENAME))
            {

                StreamReader streamReader = File.OpenText(FILENAME);
                try
                {
                    Type type = _appSettings.GetType();
                    XmlSerializer xml = new XmlSerializer(type);
                    object xmlData = xml.Deserialize(streamReader);
                    _appSettings = (AppSettings)xmlData;
                    Console.WriteLine("Loaded Settings File, {0}", FILENAME);
                }
                catch
                {
                    Console.WriteLine("Error loading config file {0}", FILENAME);
                    _appSettings = GetDefaultConfig();
                }
                streamReader.Close();
            }
            else
            {
                _appSettings = GetDefaultConfig();
            }
        }

        public void SaveConfig()
        {
            StreamWriter streamWriter = File.CreateText(FILENAME);
            Type type = _appSettings.GetType();
            if (type.IsSerializable)
            {
                XmlSerializer xml = new XmlSerializer(type);
                xml.Serialize(streamWriter, _appSettings);
                streamWriter.Close();
            }
            Console.WriteLine("Saved Settings File");
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

                    MinimumPadding = 5,
                    WallRatio = 6
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

        public int WallRatio { get; set; }





    }





}
