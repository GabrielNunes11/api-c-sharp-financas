using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.DTO.ToReceiveDTO;

namespace ControleFacil.Api.AutoMapper
{
    public class ToReceiveProfileMapper : Profile
    {
        public ToReceiveProfileMapper()
        {
            CreateMap<ToReceive, ToReceiveRequestDTO>().ReverseMap();

            CreateMap<ToReceive, ToReceiveResponseDTO>().ReverseMap();
        }
    }
}