using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entidades;

namespace API.Helpers
{
    public class LugarUrlResolver : IValueResolver<Lugar, LugarDTOS, string>
    {
        private readonly IConfiguration _config;
        public LugarUrlResolver(IConfiguration config)
        {
            _config = config;
            
        }

        public string Resolve(Lugar source, LugarDTOS destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImagenUrl))
            {
                return _config["ApiUrl"]+source.ImagenUrl;
            }
            return null;
        }
    }
}