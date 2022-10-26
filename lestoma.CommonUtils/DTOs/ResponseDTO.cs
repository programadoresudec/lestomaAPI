using Newtonsoft.Json;

namespace lestoma.CommonUtils.DTOs
{
    public class ResponseDTO
    {
        public string MensajeHttp { get; set; }
        public bool IsExito { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
