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
    public class ToPayRepository : IToPayRepository
    {
        private readonly ApplicationContext _context;

        public ToPayRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ToPay> Add(ToPay entity)
        {
            await _context.ToPay.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(ToPay entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToPay>> GetAll()
        {
            return await _context.ToPay.AsNoTracking()
                        .OrderBy(n => n.Id)
                        .ToListAsync();
        }

        public async Task<ToPay?> GetById(Guid Id)
        {
            return await _context.ToPay.AsNoTracking()
                        .Where(n => n.Id == Id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ToPay>> GetToPayByUserId(Guid userId)
        {
            return await _context.ToPay.AsNoTracking()
                        .Where(n => n.UserId == userId)
                        .OrderBy(n => n.Id)
                        .ToListAsync();
        }

        public async Task<ToPay> Update(ToPay entity)
        {
            ToPay updatedToPay = _context.ToPay
                                                .Where(n => n.Id == entity.Id)
                                                .FirstOrDefault();

            _context.Entry(updatedToPay).CurrentValues.SetValues(entity);
            _context.Update<ToPay>(updatedToPay);

            await _context.SaveChangesAsync();

            return updatedToPay;
        }
    }
}