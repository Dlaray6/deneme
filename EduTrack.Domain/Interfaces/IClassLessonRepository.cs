using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain.Interfaces
{
    public interface IClassLessonRepository
    {
        Task AddAsync(ClassLesson classLesson);
        Task<bool> ExistsAsync(int classId, int lessonId);
        Task SaveChangesAsync();
    }

}
