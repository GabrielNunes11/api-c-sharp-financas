using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Domain.Services.Interfaces
{
    /// <summary>
    /// Interface para criação de serviços Crud
    /// </summary>
    /// <typeparam name="RQ">DTO do Request</typeparam>
    /// <typeparam name="RS">DTO da Response</typeparam>
    /// <typeparam name="i">Tipo do Id</typeparam>
    public interface IService<RQ, RS, I> where RQ : class
    {
        Task<IEnumerable<RS>> GetAll(I userId);

        Task<RS> GetById(I id, I userId);

        Task<RS> Add(RQ entity, I userId);

        Task<RS> UpdateById(I id, RQ entity, I userId);

        Task Inactivation(I id, I userId);
    }
}