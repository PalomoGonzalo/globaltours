using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Datos;

namespace Infraestructura.Repositorios
{
    public class CategoriaRepositorio:ICategoriaRepositorio
    {
        private readonly IRepositorioGenerico<Categoria> _categoriaRepo;

        public CategoriaRepositorio(IRepositorioGenerico<Categoria> categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;

        }
        public Task<Categoria> ObtenerCategoria(int id)
        {
            return (_categoriaRepo.ObtenerAsync(id));
        }
    }
}