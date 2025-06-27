using EduTrack.Application.DTOs;
using EduTrack.Data;
using EduTrack.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Idare")]
    public class ClassLessonsController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public ClassLessonsController(EduTrackDbContext context)
        {
            _context = context;
        }


        [HttpGet("lessons")]
        public async Task<IActionResult> GetLessons()
        {
            var lessons = await _context.Lessons
                .Select(l => new { l.Id, l.Name })
                .ToListAsync();
            return Ok(lessons);
        }

      
        [HttpPost]
        public async Task<IActionResult> AssignLessonToClass([FromBody] ClassLessonCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Sınıf kontrolü
            var classExists = await _context.Classes.FindAsync(dto.ClassRoomId);
            if (classExists == null)
                return NotFound("Sınıf bulunamadı.");

            // Ders kontrolü
            var lessonExists = await _context.Lessons.FindAsync(dto.LessonId);
            if (lessonExists == null)
                return NotFound("Ders bulunamadı.");

            // Öğretmen kontrolü
            var teacherExists = await _context.Users
                .FirstOrDefaultAsync(t => t.Id == dto.TeacherId && t.Role == "Teacher");
            if (teacherExists == null)
                return NotFound("Öğretmen bulunamadı.");

            // Aynı sınıf + ders daha önce atanmış mı?
            var exists = await _context.ClassLessons
                .AnyAsync(cl => cl.ClassRoomId == dto.ClassRoomId && cl.LessonId == dto.LessonId);
            if (exists)
                return Conflict("Bu ders zaten sınıfa atanmış.");

            // Atama
            var newClassLesson = new ClassLesson
            {
                ClassRoomId = dto.ClassRoomId,
                LessonId = dto.LessonId,
                TeacherId = dto.TeacherId
            };

            _context.ClassLessons.Add(newClassLesson);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ders ve öğretmen başarıyla sınıfa atandı." });
        }
    }
}
