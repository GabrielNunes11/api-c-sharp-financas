using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Class
{
    public class ToReceiveRepository : IToReceiveRepository
    {
        private readonly ApplicationContext _context;

        public ToReceiveRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ToReceive> Add(ToReceive entity)
        {
            await _context.ToReceive.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(ToReceive entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToReceive>> GetAll()
        {
            return await _context.ToReceive.AsNoTracking()
                        .OrderBy(n => n.Id)
                        .ToListAsync();
        }

        public async Task<ToReceive?> GetById(Guid Id)
        {
            return await _context.ToReceive.AsNoTracking()
                        .Where(n => n.Id == Id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ToReceive>> GetToReceiveById(Guid userId)
        {
            return await _context.ToReceive.AsNoTracking()
                        .Where(n => n.UserId == userId)
                        .OrderBy(n => n.Id)
                        .ToListAsync();
        }

        public async Task<ToReceive> Update(ToReceive entity)
        {
            ToReceive updatedToReceive = _context.ToReceive
                                                .Where(n => n.Id == entity.Id)
                                                .FirstOrDefault();

            _context.Entry(updatedToReceive).CurrentValues.SetValues(entity);
            _context.Update<ToReceive>(updatedToReceive);

            await _context.SaveChangesAsync();

            return updatedToReceive;
        }
    }
}