using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Domain.Models
{
    public class ToPay : Transactions
    {
        public decimal PaidValue { get; set; }

        public DateTime? PaidDate { get; set; }
    }
}