using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entidades;
using Microsoft.Extensions.Logging;

namespace Infraestructura.Datos
{
    public class BaseDatosSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext,ILoggerFactory loggerFactory)
        {
            try
            {
                if(!dbContext.Pais.Any())
                {
                    var paisData=File.ReadAllText("../Infraestructura/Datos/SeedData/paises.json");
                    var paises=JsonSerializer.Deserialize<List<Pais>>(paisData);

                    foreach (var item in paises)
                    {
                        await dbContext.Pais.AddAsync(item);
                    }

                    await dbContext.SaveChangesAsync();
                }

                if(!dbContext.Categoria.Any())
                {
                    var categoriaData=File.ReadAllText("../Infraestructura/Datos/SeedData/categorias.json");
                    var categorias=JsonSerializer.Deserialize<List<Categoria>>(categoriaData);

                    foreach (var item in categorias)
                    {
                        await dbContext.Categoria.AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();

                }

                 if(!dbContext.Lugar.Any())
                {
                    var lugaresData=File.ReadAllText("../Infraestructura/Datos/SeedData/lugares.json");
                    var lugares=JsonSerializer.Deserialize<List<Lugar>>(lugaresData);

                    foreach (var item in lugares)
                    {
                        await dbContext.Lugar.AddAsync(item);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }
            catch (System.Exception ex)
            {
                
                var logger=loggerFactory.CreateLogger<BaseDatosSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}