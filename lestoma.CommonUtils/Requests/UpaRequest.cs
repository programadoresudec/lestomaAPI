using lestoma.CommonUtils.Interfaces;

namespace lestoma.CommonUtils.Requests
{
    public class UpaRequest : IId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public short CantidadActividades { get; set; }
    }
}
