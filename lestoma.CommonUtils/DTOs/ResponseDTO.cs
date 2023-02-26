using Newtonsoft.Json;
using System.Collections.Generic;

namespace lestoma.CommonUtils.DTOs
{
    public class ResponseDTO
    {
        public string MensajeHttp { get; set; }
        public bool IsExito { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public IEnumerable<ErrorEntryDTO> ErrorsEntries { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, formatting: Formatting.Indented);
        }
    }
    public class ErrorEntryDTO
    {
        public string Source { get; set; }
        public string TitleError { get; set; }   
    }
}
