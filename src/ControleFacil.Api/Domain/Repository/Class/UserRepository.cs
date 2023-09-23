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
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User entity)
        {
            await _context.User.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(User entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.AsNoTracking()
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.User.AsNoTracking()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetById(Guid Id)
        {
            return await _context.User.AsNoTracking()
            .Where(u => u.Id == Id)
            .FirstOrDefaultAsync();
        }

        public async Task<User> Update(User entity)
        {
            User updatedUser = _context.User
                .Where(u => u.Id == entity.Id)
                .FirstOrDefault();

            _context.Entry(updatedUser).CurrentValues.SetValues(entity);
            _context.Update<User>(updatedUser);

            await _context.SaveChangesAsync();

            return updatedUser;
        }
    }
}