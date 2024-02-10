using CsvHelper;
using CsvHelper.Configuration;
using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.MyException;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Helpers
{
    public interface IWriteToCSVFile
    {
        public Task<byte[]> WriteNewCSV<T>(IEnumerable<T> listado);
        public Task<byte[]> GetFileCSV<T>(string path);
    }
    public class WriteToCSVFile : IWriteToCSVFile
    {
        private readonly ILoggerManager _logger;
        public WriteToCSVFile(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> GetFileCSV<T>(string path)
        {
            if (!File.Exists(path))
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, $"El archivo {path} no existe.");

            byte[] archivo = await File.ReadAllBytesAsync(path);
            var size = archivo.Length;
            if (size == 0)
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.InternalServerError, "El archivo se encuentra vacío.");

            return archivo;

        }

        public async Task<byte[]> WriteNewCSV<T>(IEnumerable<T> listado)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Mode = CsvMode.Escape,
                    Delimiter = "|",
                    IgnoreReferences = true,
                };
                using var memoryStream = new MemoryStream();
                using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
                using var csvOut = new CsvWriter(streamWriter, config);
                await csvOut.WriteRecordsAsync(listado);
                await streamWriter.FlushAsync();

                memoryStream.Position = 0;
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al escribir en el archivo CSV.", ex);
                throw;
            }
        }
    }
}
