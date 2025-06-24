using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain.Interfaces
{
    public interface ITeacherClassLessonRepository
    {
        Task AddAsync(TeacherClassLesson teacherClassLesson);
        Task<bool> ExistsAsync(int teacherId, int classLessonId);
        Task SaveChangesAsync();
    }

}
