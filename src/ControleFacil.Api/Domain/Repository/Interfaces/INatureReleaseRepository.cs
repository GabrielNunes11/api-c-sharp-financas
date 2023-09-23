using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface INatureReleaseRepository : IRepository<NatureRelease, Guid>
    {
        Task<IEnumerable<NatureRelease>> GetReleaseByUserId(Guid userId);
    }
}