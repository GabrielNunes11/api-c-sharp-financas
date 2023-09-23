using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.DTO.UserDTO
{
    public class UserResponseDTO : UserRequestDTO
    {
        public Guid Id { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}