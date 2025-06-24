using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using EduTrack.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Application.Services
{
    public class TeacherClassLessonService : ITeacherClassLessonService
    {
        private readonly ITeacherClassLessonRepository _repository;

        public TeacherClassLessonService(ITeacherClassLessonRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTeacherToClassLessonAsync(int teacherId, int classLessonId)
        {
            if (await _repository.ExistsAsync(teacherId, classLessonId))
                throw new Exception("Bu öğretmen zaten bu sınıf dersine atanmış.");

            var teacherClassLesson = new TeacherClassLesson
            {
                TeacherId = teacherId,
                LessonId = classLessonId
            };

            await _repository.AddAsync(teacherClassLesson);
            await _repository.SaveChangesAsync();
        }
    }

}
