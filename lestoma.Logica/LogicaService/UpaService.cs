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
    public class UpaService : IUpaService
    {
        private readonly UpaRepository _upaRepository;
        private readonly ProtocoloRepository _protocoloRepository;
        public UpaService(UpaRepository upaRepository, ProtocoloRepository protocoloRepository)
        {
            _protocoloRepository = protocoloRepository;
            _upaRepository = upaRepository;
        }

        public async Task<IEnumerable<EUpa>> GetAll()
        {
            var listado = await _upaRepository.GetAll();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public IQueryable<EUpa> GetAllForPagination()
        {
            var listado = _upaRepository.GetAllAsQueryable().Include(x => x.ProtocolosCOM);
            var lista = listado.ToList();
            if (!listado.Any())
            {
                throw new HttpStatusCodeException(HttpStatusCode.NoContent, "No hay contenido.");
            }
            return listado;
        }

        public async Task<ResponseDTO> GetById(Guid id)
        {
            var query = await _upaRepository.GetById(id);
            if (query == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");

            return Responses.SetOkResponse(query);
        }

        public async Task<ResponseDTO> Create(EUpa entidad)
        {
            bool existe = await _upaRepository.ExisteUpa(entidad.Nombre, entidad.Id);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya esta en uso.");
            entidad.Id = Guid.NewGuid();
            await _upaRepository.Create(entidad);
            return Responses.SetCreatedResponse(entidad);
        }
        public async Task<ResponseDTO> Update(EUpa entidad)
        {
            var response = await GetById(entidad.Id);
            var upa = (EUpa)response.Data;
            bool existe = await _upaRepository.ExisteUpa(entidad.Nombre, upa.Id, true);
            if (existe)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya está en uso.");
            upa.Nombre = entidad.Nombre;
            upa.CantidadActividades = entidad.CantidadActividades;
            upa.Descripcion = entidad.Descripcion;
            await _upaRepository.Update(upa);
            return Responses.SetOkMessageEditResponse(upa);
        }
        public async Task Delete(Guid id)
        {
            var entidad = await GetById(id);
            var protocolos = await _protocoloRepository.GetAllAsQueryable().Where(x => x.UpaId == ((EUpa)entidad.Data).Id).ToListAsync();
            await _protocoloRepository.DeleteByRange(protocolos);
            await _upaRepository.Delete((EUpa)entidad.Data);
        }

        public async Task<IEnumerable<NameDTO>> GetUpasJustNames()
        {
            return await _upaRepository.GetUpasJustNames();
        }

        public async Task<ResponseDTO> UpdateProtocol(EProtocoloCOM protocolo)
        {
            var protocolExist = await _protocoloRepository.FindWithCondition(x => x.Id == protocolo.Id);
            if (protocolExist == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No existe el protocolo.");

            protocolExist.Nombre = protocolo.Nombre;
            protocolExist.PrimerByteTrama = protocolo.PrimerByteTrama;
            protocolExist.Sigla = protocolo.Sigla;
            await _protocoloRepository.Update(protocolExist);
            return Responses.SetOkMessageEditResponse(protocolExist);
        }

        public async Task<short> GetSuperAdminId(int userId)
        {
            var superadmin = await _upaRepository.GetSuperAdmin(userId);
            if (superadmin == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No existe el super administrador.");
            return superadmin.Id;
        }

        public async Task<IEnumerable<NameProtocoloDTO>> GetProtocolsByUpaId(Guid upaId)
        {
            var existeUpa = await _upaRepository.AnyWithCondition(x => x.Id == upaId);
            if (!existeUpa)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No existe la upa.");

            return await _protocoloRepository.GetProtocolsByUpaId(upaId);
        }
    }
}
