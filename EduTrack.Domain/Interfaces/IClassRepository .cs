
using EduTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EduTrack.Data.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<ClassRoom?> GetByIdAsync(int id);
        Task<IEnumerable<ClassRoom>> GetAllBySchoolIdAsync(int schoolId);
        Task AddAsync(ClassRoom newClass);
        Task SaveChangesAsync();
    }
}
