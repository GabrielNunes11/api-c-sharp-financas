using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.DTO.UserDTO;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    public interface IUserService : IService<UserRequestDTO, UserResponseDTO, Guid>
    {
        Task<UserLoginResponseDTO> AuthUserLogin(UserLoginRequestDTO authLogin);

        Task<UserResponseDTO> GetByEmail(string email);
    }
}