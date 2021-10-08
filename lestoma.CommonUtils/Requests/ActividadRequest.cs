using lestoma.CommonUtils.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ActividadRequest : IId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
