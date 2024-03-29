﻿using PRJ_MazeWinForms.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace MazeFormsClasses
{
    [Serializable()]
    public struct AppSettings
    {
        // Structure of different application settings
        public MazeDisplaySettings DisplaySettings { get; set; }
        public MazeControlSettings ControlSettings { get; set; }

    }

    public class AppSettingsManager
    {
        // Class to manage loading and saving of settings

        private const string FILENAME = "settings.xml";
        private AppSettings _appSettings = new AppSettings();

        public AppSettings AppSettings
        {
            get { return _appSettings; }
            set { _appSettings = value; }
        }

        // Decided I didn't have time to implement customisable settings so config is always default
        public void LoadConfig()
        {
            /*
            if (File.Exists(FILENAME))
            {
                StreamReader streamReader = File.OpenText(FILENAME);
                try
                {
                    Type type = _appSettings.GetType();
                    XmlSerializer xml = new XmlSerializer(type);
                    object xmlData = xml.Deserialize(streamReader);
                    _appSettings = (AppSettings)xmlData;
                    LogHelper.Log(string.Format("Loaded Settings File, {0}", FILENAME));
                }
                catch
                {
                    LogHelper.Error(string.Format("Error loading config file, {0}", FILENAME));
                    _appSettings = GetDefaultConfig();
                }
                streamReader.Close();
                
            }
            else
            {
                _appSettings = GetDefaultConfig();
            }
            */
            _appSettings = GetDefaultConfig();
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
                    LogHelper.ErrorLog(string.Format("Error saving settings file, {0}", FILENAME));
                }
            }
            streamWriter.Close();
            LogHelper.Log(string.Format("Saved settings in {0}", FILENAME));
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
