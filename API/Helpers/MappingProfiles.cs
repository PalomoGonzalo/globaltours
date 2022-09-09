using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entidades;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

             //CreateMap<LugarDTOS,Lugar>();
            CreateMap<LugarPostDTOS,Lugar>();
                                

            CreateMap<Lugar,LugarDTOS>()
                                .ForMember(d=>d.Pais,o=>o.MapFrom(s=>s.Pais.Nombre))
                                .ForMember(d=>d.Categoria,o=>o.MapFrom(s=>s.Categoria.Nombre));
                                


        }

    }
}