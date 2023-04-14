using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Helpers
{
    public class LestomaLog
    {
        private static string logPath { get; }
        private static string logfolderpath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LestomaApp", "Logs");
        static LestomaLog()
        {
            // Create Log file
            Directory.CreateDirectory(logfolderpath);
            int fileCount = Directory.GetFiles(logfolderpath, "*.*", SearchOption.TopDirectoryOnly).Length;
            logPath = Path.Combine(logfolderpath, $"Lestoma_Log_{++fileCount}.txt");
            File.CreateText(logPath).Dispose();
        }

        public static void Normal(string message)
        {
            Debug.WriteLine("Enviando al servidor.");
            WriteToLog("[NORMAL] " + message);
        }
        public static void Error(string message)
        {
            Debug.WriteLine(message);
            WriteToLog("[ERROR] " + message);
        }

        private static void WriteToLog(string message)
        {
            _ = Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(logPath, append: true))
                {
                    sw.WriteLine($"[{DateTime.Now}] : {message}");
                }
            });
        }
    }
}
