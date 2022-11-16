using MazeFormsClasses;
using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace MazeFormsClasses
{
    [Serializable()]
    public struct AppSettings
    {
        public MazeDisplaySettings DisplaySettings { get; set; }
        public MazeControlSettings ControlSettings { get; set; }

    }

    public class AppSettingsManager
    {
        private const string FILENAME = "settings.xml";
        private AppSettings _appSettings = new AppSettings();

        public AppSettings AppSettings
        {
            get { return _appSettings; }
            set { _appSettings = value; }
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
                    Console.WriteLine("Error loading config file, {0}", FILENAME);
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
                try
                {
                    XmlSerializer xml = new XmlSerializer(type);
                    xml.Serialize(streamWriter, _appSettings);
                }
                catch
                {
                    Console.WriteLine("Error while saving settings file, {0}", FILENAME);
                }
            }
            streamWriter.Close();
            Console.WriteLine("Saved Settings File");
        }

        private AppSettings GetDefaultConfig()
        {
            AppSettings DefaultSettings = new AppSettings
            {
                DisplaySettings = MazeDisplaySettings.Default,
                ControlSettings = MazeControlSettings.Default
            };

            return DefaultSettings;
        }
    }

    public struct MazeDisplaySettings
    {
        public Color WallColour { get; set; }
        public Color CellColour { get; set; }
        public Color StartColour { get; set; }
        public Color EndColour { get; set; }
        public Color PlayerColour { get; set; }
        public Color HintColour { get; set; }
        public int MinimumPadding { get; set; }
        public int WallRatio { get; set; }


        public static readonly MazeDisplaySettings Default = new MazeDisplaySettings
        {
            WallColour = Color.Black,
            CellColour = Color.White,
            StartColour = Color.Green,
            EndColour = Color.DarkRed,
            PlayerColour = Color.Blue,
            HintColour = Color.Red,

            MinimumPadding = 5,
            WallRatio = 6
        };
    }

    public struct MazeControlSettings 
    {
        public char[] Movement { get; set; }
        public char Hint { get; set; }

        public static readonly MazeControlSettings Default = new MazeControlSettings
        {
            Movement = new char[] { 'w', 'd', 's', 'a' },
            Hint = 'h'
        };
    }






}
