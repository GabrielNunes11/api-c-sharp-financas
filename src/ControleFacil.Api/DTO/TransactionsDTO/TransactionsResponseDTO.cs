using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.DTO.TransactionsDTO
{
    public class TransactionsResponseDTO : TransactionsRequestDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime? InactivationDate { get; set; }
    }
}