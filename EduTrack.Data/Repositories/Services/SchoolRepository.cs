using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Data.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly EduTrackDbContext _context;

        public SchoolRepository(EduTrackDbContext context)
        {
            _context = context;
        }

        public async Task<List<School>> GetAllSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        public async Task AddSchoolAsync(School school)
        {
            await _context.Schools.AddAsync(school);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
