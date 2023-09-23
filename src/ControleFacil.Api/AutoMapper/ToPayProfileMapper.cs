using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.DTO.ToPayDTO;

namespace ControleFacil.Api.AutoMapper
{
    public class ToPayProfileMapper : Profile
    {
        public ToPayProfileMapper()
        {
            CreateMap<ToPay, ToPayRequestDTO>().ReverseMap();

            CreateMap<ToPay, ToPayResponseDTO>().ReverseMap();
        }
    }
}