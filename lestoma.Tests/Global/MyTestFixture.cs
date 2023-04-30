using lestoma.CommonUtils.Helpers;
using System;
using System.IO;
using System.Reflection;

namespace lestoma.Tests.Global
{
    public class MyTestFixture : IDisposable
    {
        public readonly CRCHelper CrcHelper;
        public string RutaArchivo { get; set; }
        public MyTestFixture()
        {
            CrcHelper = new CRCHelper();
            GenerateFile();
        }
        private void GenerateFile()
        {

            string carpetaAplicacion = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            RutaArchivo = Path.Combine(carpetaAplicacion, "tramasCRC.txt");
            if (File.Exists(RutaArchivo))
            {
                File.Delete(RutaArchivo);
            }
        }
        public void Dispose()
        {

        }
    }
}
