using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.DTO.TransactionsDTO;

namespace ControleFacil.Api.DTO.ToPayDTO
{
    public class ToPayRequestDTO : TransactionsRequestDTO
    {
        public decimal? PaidValue { get; set; }

        public DateTime? PaidDate { get; set; }
    }
}