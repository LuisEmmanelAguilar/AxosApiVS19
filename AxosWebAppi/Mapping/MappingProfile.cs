using AutoMapper;
using AxosWebAppi.Dto;
using AxosWebAppi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxosWebAppi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
        }

    }
}
