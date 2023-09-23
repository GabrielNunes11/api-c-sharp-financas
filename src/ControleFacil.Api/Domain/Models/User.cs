using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo de Email é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string Password { get; set; } = string.Empty;

        public DateTime RegisterDate { get; set; }

        public DateTime? InactivationDate { get; set; }
    }
}