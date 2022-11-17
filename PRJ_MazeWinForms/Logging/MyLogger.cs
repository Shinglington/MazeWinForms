﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_MazeWinForms.Logging
{
    public enum ErrorLevel
    {
        Information,
        Error
    }
    public abstract class MyLogger
    {

        public abstract void Log(string message, ErrorLevel error);
    }

    public class ConsoleLogger : MyLogger 
    {
        public override void Log(string message, ErrorLevel error)
        {
            ConsoleColor colour = ConsoleColor.White;
            switch (error)
            {
                case ErrorLevel.Information:
                    colour = ConsoleColor.DarkGreen;
                    break;
                case ErrorLevel.Error:
                    colour = ConsoleColor.DarkRed;
                    break;
            }
            Console.ForegroundColor = colour;
            Console.Write(error.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + message + "\n");
        }
    }

    public class FileLogger : MyLogger 
    {
        public string FILE_NAME = "LogData.txt";
        public override void Log(string message, ErrorLevel error)
        {
            using (StreamWriter sWriter = new StreamWriter(FILE_NAME))
            {
                sWriter.WriteLine(error.ToString() + "   " + message);
                sWriter.Close();
            }
        }
    }


    public static class LogHelper 
    {
        private const bool CONSOLE_OUTPUT = true;
        private static ConsoleLogger consoleLogger = new ConsoleLogger();
        private static FileLogger fileLogger = new FileLogger();
        public static void Log(string logMessage)
        {
            WriteLog(logMessage, ErrorLevel.Information);
        }

        public static void ErrorLog(string logMessage)
        {
            WriteLog(logMessage, ErrorLevel.Error);
        }

        private static void WriteLog(string logMessage, ErrorLevel level)
        {
            string date = DateTime.Now.ToString();
            string messageToLog = string.Format("[{0}] : {1}", date, logMessage);
            
            if (CONSOLE_OUTPUT)
            {
                consoleLogger.Log(messageToLog, level);
            }
            fileLogger.Log(messageToLog, level);
        }

    }

}