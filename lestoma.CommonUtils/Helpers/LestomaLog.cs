using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lestoma.CommonUtils.Helpers
{
    public class LestomaLog
    {
        private static string logPath { get; }
        private static string logfolderpath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LestomaApp", "Logs");
        static LestomaLog()
        {
            // Create Log file
            Directory.CreateDirectory(logfolderpath);
            int fileCount = Directory.GetFiles(logfolderpath, "*.*", SearchOption.TopDirectoryOnly).Length;
            logPath = Path.Combine(logfolderpath, $"Assist_Log_{++fileCount}.txt");
            File.CreateText(logPath).Dispose();
        }

        public static void Normal(string message)
        {
            WriteToLog("[NORMAL] " + message);
        }
        public static void Error(string message)
        {
            WriteToLog("[ERROR] " + message);
        }

        public static void Debug(string message)
        {
            WriteToLog("[DEBUG] " + message);
        }

        private static void WriteToLog(string message)
        {
            using (StreamWriter sw = new StreamWriter(logPath, append: true))
            {
                sw.WriteLine($"[{DateTime.Now}] : {message}");
            }
        }
    }
}
