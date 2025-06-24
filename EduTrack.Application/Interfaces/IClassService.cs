using EduTrack.Application;
using EduTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace EduTrack.Application.Interfaces
{
    public interface IClassService
    {
        Task<ClassRoom?> GetClassByIdAsync(int id);
        Task<IEnumerable<ClassRoom>> GetClassesBySchoolAsync(int schoolId);
        Task AddClassAsync(ClassRoom newClass);
        Task AssignUserToClassAsync(int classId, int userId);
        Task<List<User>> GetUsersInClassAsync(int classId);
    }
}
