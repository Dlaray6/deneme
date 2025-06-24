using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using EduTrack.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


public class ClassLessonService : IClassLessonService
{
    private readonly IClassLessonRepository _repository;

    public ClassLessonService(IClassLessonRepository repository)
    {
        _repository = repository;
    }

    public async Task AddClassLessonAsync(int classId, int lessonId)
    {
        if (await _repository.ExistsAsync(classId, lessonId))
            throw new Exception("Bu ders zaten bu sınıfa atanmış.");

        var classLesson = new ClassLesson
        {
            ClassRoomId = classId,
            LessonId = lessonId
        };

        await _repository.AddAsync(classLesson);
        await _repository.SaveChangesAsync();
    }
}
