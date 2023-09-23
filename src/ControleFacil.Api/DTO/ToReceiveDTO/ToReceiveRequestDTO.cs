using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.DTO.TransactionsDTO;

namespace ControleFacil.Api.DTO.ToReceiveDTO
{
    public class ToReceiveRequestDTO : TransactionsRequestDTO
    {
        public decimal? ReceivedValue { get; set; }

        public DateTime? ReceivedDate { get; set; }
    }
}