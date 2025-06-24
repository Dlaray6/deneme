using EduTrack.Domain;
using EduTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Data.Repositories.Services
{
    public class TeacherClassLessonRepository : ITeacherClassLessonRepository
    {
        private readonly EduTrackDbContext _context;

        public TeacherClassLessonRepository(EduTrackDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TeacherClassLesson teacherClassLesson)
        {
            await _context.TeacherClassLessons.AddAsync(teacherClassLesson);
        }

        public async Task<bool> ExistsAsync(int teacherId, int classLessonId)
        {
            return await _context.TeacherClassLessons.AnyAsync(tcl =>
                tcl.TeacherId == teacherId && tcl.LessonId == classLessonId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
