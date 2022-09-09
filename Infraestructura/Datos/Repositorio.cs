using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Especificaciones;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class Repositorio<T> : IRepositorioGenerico<T> where T : class
    {
        public ApplicationDbContext _db { get; }
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            
        }


        public async Task<T> ObtenerAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ObtenerTodosAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> ObtenerEspec(IEspecificacion<T> espec)
        {
            return await AplicarEspeficicacion(espec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ObtenerTodosEspec(IEspecificacion<T> espec)
        {
            return await AplicarEspeficicacion(espec).ToListAsync();
        }

        private IQueryable<T> AplicarEspeficicacion(IEspecificacion<T> espec)
        {
            return EvaluadorEspecificacion<T>.GetQueary(_db.Set<T>().AsQueryable(),espec);
        }

        
    }
}