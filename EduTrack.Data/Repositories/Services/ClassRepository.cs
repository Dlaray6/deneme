using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;

using EduTrack.Data.Repositories.Interfaces;

namespace EduTrack.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly EduTrackDbContext _context;

        public ClassRepository(EduTrackDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClassRoom newClass)
        {
            await _context.Classes.AddAsync(newClass);
        }

        public async Task<IEnumerable<ClassRoom>> GetAllBySchoolIdAsync(int schoolId)
        {
            return await _context.Classes
                .Where(c => c.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<ClassRoom?> GetByIdAsync(int id)
        {
            return await _context.Classes
                .Include(c => c.School)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
