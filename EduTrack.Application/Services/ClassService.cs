using EduTrack.Application.Interfaces;

using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace EduTrack.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly List<ClassRoom> _dummyClasses = new();
        private readonly List<ClassUser> _dummyClassUsers = new();
        private readonly List<User> _dummyUsers = new(); // Öğrencileri temsil ediyor

        public async Task<ClassRoom?> GetClassByIdAsync(int id)
        {
            return await Task.FromResult(_dummyClasses.FirstOrDefault(c => c.Id == id));
        }

        public async Task<IEnumerable<ClassRoom>> GetClassesBySchoolAsync(int schoolId)
        {
            return await Task.FromResult(_dummyClasses.Where(c => c.SchoolId == schoolId));
        }

        public async Task AddClassAsync(ClassRoom newClass)
        {
            _dummyClasses.Add(newClass);
            await Task.CompletedTask;
        }

        public async Task AssignUserToClassAsync(int classId, int userId)
        {
            var exists = _dummyClassUsers.Any(cu => cu.ClassRoomId == classId && cu.UserId == userId);
            if (!exists)
            {
                _dummyClassUsers.Add(new ClassUser { ClassRoomId = classId, UserId = userId });
            }
            await Task.CompletedTask;
        }

        public async Task<List<User>> GetUsersInClassAsync(int classId)
        {
            var userIds = _dummyClassUsers
                .Where(cu => cu.ClassRoomId == classId)
                .Select(cu => cu.UserId)
                .ToList();

            var users = _dummyUsers.Where(u => userIds.Contains(u.Id)).ToList();
            return await Task.FromResult(users);
        }
    }
}
