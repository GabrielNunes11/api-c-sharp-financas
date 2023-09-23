using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface IToPayRepository : IRepository<ToPay, Guid>
    {
        Task<IEnumerable<ToPay>> GetToPayByUserId(Guid userId);
    }
}