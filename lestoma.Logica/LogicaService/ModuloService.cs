using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class ModuloService : IModuloService
    {
        private readonly ModuloRepository _moduloRepository;
        public ModuloService(ModuloRepository moduloRepository)
        {
            _moduloRepository = moduloRepository;
        }

        public async Task<IEnumerable<EModuloComponente>> GetAll()
        {
            var listado = await _moduloRepository.GetAllAsQueryable().OrderBy(x => x.Nombre).ToListAsync();
            return !listado.Any() ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.") : listado;
        }

        public IQueryable<EModuloComponente> GetAllForPagination()
        {
            var listado = _moduloRepository.GetAllAsQueryable().OrderBy(x => x.Nombre);
            return !listado.Any() ? throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.") : listado;
        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            var modulo = await _moduloRepository.GetById(id);
            return modulo == null
                ? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el modulo requerido.")
                : Responses.SetOkResponse(modulo);
        }
        public async Task<ResponseDTO> Create(EModuloComponente entidad)
        {
            bool existe = await _moduloRepository.ExisteModulo(entidad.Nombre, Guid.Empty);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya esta en uso.");

            await _moduloRepository.Create(entidad);
            return Responses.SetCreatedResponse(entidad);
        }
        public async Task<ResponseDTO> Update(EModuloComponente entidad)
        {
            var response = await GetById(entidad.Id);
            var Modulo = (EModuloComponente)response.Data;
            bool existe = await _moduloRepository.ExisteModulo(entidad.Nombre, Modulo.Id, true);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso.");
            Modulo.Nombre = entidad.Nombre;
            await _moduloRepository.Update(Modulo);
            return Responses.SetOkMessageEditResponse(Modulo);
        }
        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            await _moduloRepository.Delete((EModuloComponente)entidad.Data);
        }

        public async Task<IEnumerable<NameDTO>> GetModulosJustNames()
        {
            return await _moduloRepository.GetModulosJustNames();
        }
    }
}