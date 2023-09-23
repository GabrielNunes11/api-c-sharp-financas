using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.DTO.UserDTO
{
    public class UserRequestDTO : UserLoginRequestDTO
    {
        public DateTime? InactivationDate { get; set; }
    }
}