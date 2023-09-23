using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.DTO.NatureReleaseDTO;

namespace ControleFacil.Api.AutoMapper
{
    public class NatureReleaseProfileMapper : Profile
    {
        public NatureReleaseProfileMapper()
        {
            CreateMap<NatureRelease, NatureReleaseRequestDTO>().ReverseMap();

            CreateMap<NatureRelease, NatureReleaseResponseDTO>().ReverseMap();
        }
    }
}