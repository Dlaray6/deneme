using EduTrack.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduTrack.Application.Interfaces
{
    public interface ISchoolRepository
    {
        Task<List<School>> GetAllSchoolsAsync();
        Task AddSchoolAsync(School school);
        Task SaveChangesAsync();
    }
}
