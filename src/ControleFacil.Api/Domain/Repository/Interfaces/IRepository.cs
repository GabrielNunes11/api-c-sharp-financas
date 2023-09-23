using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    /// <summary>
    /// Interface para criação do Crud da entidade Usuário
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="I"></typeparam>
    public interface IRepository<T, I> where T : class
    {

        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid Id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);
    }
}