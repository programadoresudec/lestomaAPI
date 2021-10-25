﻿using lestoma.CommonUtils.DTOs;
using lestoma.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IDetalleUpaActividadService
    {
        Task<List<EUpaActividad>> GetAll();
        Task<Response> CrearEnCascada(EUpaActividad entidad);
        Task<Response> ActualizarEnCascada(EUpaActividad entidad);
        Task EliminarEnCascada(int IdUsuario);  
    }
}
