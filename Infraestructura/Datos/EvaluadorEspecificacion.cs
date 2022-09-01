using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Especificaciones;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos
{
    public class EvaluadorEspecificacion<T> where T:class
    {
        public static IQueryable<T> GetQueary(IQueryable<T> inputQuerary, IEspecificacion<T> espec)
        {
            var queary=inputQuerary;

            if(espec.Filtro!=null)
            {
                queary=queary.Where(espec.Filtro);
            }

            queary=espec.Includes.Aggregate(queary,(current,include)=>current.Include(include));

            return queary;
        }
    }
}