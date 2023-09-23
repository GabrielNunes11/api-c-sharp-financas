using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Domain.Models
{
    public class ToReceive : Transactions
    {
        public decimal ReceivedValue { get; set; }

        public DateTime? ReceivedDate { get; set; }
    }
}