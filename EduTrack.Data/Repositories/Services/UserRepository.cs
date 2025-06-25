using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using EduTrack.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTrack.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EduTrackDbContext _context;

        public UserRepository(EduTrackDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByTcNoAsync(string tcNo)
        {
            return await _context.Users
                .Include(u => u.School)
                .Include(u => u.ClassUsers)
                .FirstOrDefaultAsync(u => u.TcNo == tcNo);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
