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
    public class NatureReleaseRepository : INatureReleaseRepository
    {
        private readonly ApplicationContext _context;

        public NatureReleaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<NatureRelease> Add(NatureRelease entity)
        {
            await _context.NatureRelease.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(NatureRelease entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NatureRelease>> GetAll()
        {
            return await _context.NatureRelease.AsNoTracking()
                        .OrderBy(n => n.Id)
                        .ToListAsync();
        }

        public async Task<NatureRelease?> GetById(Guid Id)
        {
            return await _context.NatureRelease.AsNoTracking()
                        .Where(n => n.Id == Id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NatureRelease>> GetReleaseByUserId(Guid userId)
        {
            return await _context.NatureRelease.AsNoTracking()
                        .Where(n => n.UserId == userId)
                        .OrderBy(n => n.Id)
                        .ToListAsync();
        }

        public async Task<NatureRelease> Update(NatureRelease entity)
        {
            NatureRelease updatedNatureRelease = _context.NatureRelease
                                                .Where(n => n.Id == entity.Id)
                                                .FirstOrDefault();

            _context.Entry(updatedNatureRelease).CurrentValues.SetValues(entity);
            _context.Update<NatureRelease>(updatedNatureRelease);

            await _context.SaveChangesAsync();

            return updatedNatureRelease;
        }
    }
}