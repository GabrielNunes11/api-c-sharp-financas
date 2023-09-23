using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Domain.Models
{
    public class Transactions
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required]
        public Guid NatureReleaseId { get; set; }

        public NatureRelease NatureRelease { get; set; }

        [Required(ErrorMessage = "O campo de descrição é obrigatório.")]
        public string Description { get; set; } = string.Empty;

        public string? Details { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo de valor é obrigatório.")]
        public decimal OriginalValue { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime ExpirationDate { get; set; }

        public DateTime? ReferenceDate { get; set; }

        public DateTime? InactivationDate { get; set; }
    }
}