using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface IToReceiveRepository : IRepository<ToReceive, Guid>
    {
        Task<IEnumerable<ToReceive>> GetToReceiveById(Guid userId);
    }
}