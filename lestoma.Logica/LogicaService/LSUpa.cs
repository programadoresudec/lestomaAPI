using lestoma.CommonUtils.DTOs;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using lestoma.Logica.MyException;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSUpa : IUpaService
    {
        private readonly Response _respuesta = new();
        private readonly DAOUpa _upaRepository;
        public LSUpa(DAOUpa upaRepository)
        {
            _upaRepository = upaRepository;
        }
        public async Task<Response> CrearUpa(EUpa upa)
        {
            bool existe = await _upaRepository.ExisteUpa(upa.Nombre);
            if (!existe)
            {
                await _upaRepository.Create(upa);
                _respuesta.IsExito = true;
                _respuesta.Data = upa;
                _respuesta.Mensaje = "se ha creado satisfactoriamente.";
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El nombre ya esta en uso.");
            }
            return _respuesta;
        }

        public async Task<Response> EditarUpa(EUpa actividad)
        {
            await _upaRepository.Update(actividad);
            _respuesta.IsExito = true;
            _respuesta.Mensaje = "Se ha editado correctamente.";
            return _respuesta;
        }

        public async Task<Response> GetUpa(int id)
        {
            try
            {
                var query = await _upaRepository.GetByIdAsync(id);
                if (query != null)
                {
                    _respuesta.Data = query;
                    _respuesta.IsExito = true;
                    _respuesta.Mensaje = "Encontrado";
                }
                else
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la upa.");
                }
                return _respuesta;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<List<EUpa>> ListaUpas()
        {
            var listado = await _upaRepository.GetAll();
            return listado.ToList();
        }

        public IQueryable<EUpa> ListaUpasPaginado()
        {
            return _upaRepository.GetAllPaginado();
        }
    }
}
