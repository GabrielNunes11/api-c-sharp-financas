using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.DTO.UserDTO;

namespace ControleFacil.Api.AutoMapper
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<User, UserRequestDTO>().ReverseMap();

            CreateMap<User, UserResponseDTO>().ReverseMap();
        }
    }
}