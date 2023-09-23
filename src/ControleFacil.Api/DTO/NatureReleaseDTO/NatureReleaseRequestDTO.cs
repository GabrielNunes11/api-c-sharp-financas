using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.DTO.NatureReleaseDTO
{
    public class NatureReleaseRequestDTO
    {
        public string Description { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
    }
}