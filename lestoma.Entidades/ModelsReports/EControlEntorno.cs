using lestoma.Entidades.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.ModelsReports
{

    [Table("control_de_entorno", Schema = "reportes")]

    public partial class EControlEntorno : EReporte
    {
    }

}