using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Application.Interfaces
{
    public interface ITeacherClassLessonService
    {
        Task AddTeacherToClassLessonAsync(int teacherId, int classLessonId);
    }

}
