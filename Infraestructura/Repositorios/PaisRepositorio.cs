using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;

namespace Infraestructura.Repositorios
{
    public class PaisRepositorio:IPaisRepositorio
    {
       
        private readonly IRepositorioGenerico<Pais> _paisRepo;
        public PaisRepositorio(IRepositorioGenerico<Pais> paisRepo)
        {
            _paisRepo = paisRepo;
        }
        

        public Task<Pais> ObtenerPaisPorId(int id)
        {
           return (_paisRepo.ObtenerAsync(id));
        }
    }
}