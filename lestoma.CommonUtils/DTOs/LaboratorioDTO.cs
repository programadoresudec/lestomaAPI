using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs

public class LaboratorioDTO : AuditoriaDTO

{
        public int Id { get; set; }
        public int TipoEstadoComponenteId { get; set;}
        public int ComponenteLaboratorioId{ get; set;}
        public int TipoDeComunicacionId{ get; set;}
        public int ActividadId{ get; set;}
        public String TramaEnviada{ get; set;}
        public String ip { get; set;}
        public String Sesion{ get; set;}
        public String TipoAplicacion{ get; set;}
        public DateTime? FechaDeCreacion{ get; set;}
        public Boolean EstadoDelInternet{ get; set;}
}