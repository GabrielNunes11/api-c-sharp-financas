using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.DTO
{
    public class ModelErrorDTO
    {
        public int StatusCode { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime DateTimeError { get; set; }
    }
}