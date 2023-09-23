using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.DTO.TransactionsDTO
{
    public class TransactionsRequestDTO
    {
        public Guid NatureReleaseId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public decimal OriginalValue { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime? ReferenceDate { get; set; }
    }
}