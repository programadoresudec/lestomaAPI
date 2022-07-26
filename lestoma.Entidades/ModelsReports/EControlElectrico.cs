using lestoma.Entidades.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.ModelsReports
{

    [Table("control_electrico", Schema = "reportes")]

    public partial class EControlElectrico : EReporte
    {
    }
}