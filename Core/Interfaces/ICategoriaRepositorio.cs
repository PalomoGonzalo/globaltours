using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Interfaces
{
    public interface ICategoriaRepositorio
    {
        public  Task<Categoria> ObtenerCategoria(int id); 
    }

   
}