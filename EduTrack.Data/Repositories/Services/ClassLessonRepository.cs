using EduTrack.Data;
using EduTrack.Domain;
using EduTrack.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ClassLessonRepository : IClassLessonRepository
{
    private readonly EduTrackDbContext _context;

    public ClassLessonRepository(EduTrackDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ClassLesson classLesson)
    {
        await _context.ClassLessons.AddAsync(classLesson);
    }

    public async Task<bool> ExistsAsync(int classId, int lessonId)
    {
        return await _context.ClassLessons.AnyAsync(cl =>
            cl.ClassRoomId == classId && cl.LessonId == lessonId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

