using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Domain.Models
{
    public class NatureRelease
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Description { get; set; } = string.Empty;

        public string? Details { get; set; } = string.Empty;

        [Required]
        public DateTime RegisterDate { get; set; }
        public DateTime? InactivationDate { get; set; }
    }
}