using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Helpers
{
    public class LestomaLog
    {
        private static string LogPath { get; }
        private static string Logfolderpath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LestomaApp", "Logs");
        static LestomaLog()
        {
            // Create Log file
            Directory.CreateDirectory(Logfolderpath);
            int fileCount = Directory.GetFiles(Logfolderpath, "*.*", SearchOption.TopDirectoryOnly).Length;
            LogPath = Path.Combine(Logfolderpath, $"Lestoma_Log_{++fileCount}.txt");
            File.CreateText(LogPath).Dispose();
        }

        public static void Normal(string message)
        {
            Debug.WriteLine(message);
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
                using (StreamWriter sw = new StreamWriter(LogPath, append: true))
                {
                    sw.WriteLine($"[{DateTime.Now}] : {message}");
                }
            });
        }
    }
}
