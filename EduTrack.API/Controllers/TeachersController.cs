using EduTrack.Data;
using EduTrack.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class TeachersController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public TeachersController(EduTrackDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _context.Users
                .Where(u => u.Role == "Teacher")
                .Select(t => new
                {
                    t.Id,
                    t.Name
                })
                .ToListAsync();

            return Ok(teachers);
        }
        

     
        [HttpGet("classes")]
        public async Task<IActionResult> GetTeacherClasses()
        {
            var teacherIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(teacherIdStr, out int teacherId))
                return Unauthorized();

            var classes = await _context.ClassLessons
                .Where(cl => cl.TeacherId.HasValue && cl.TeacherId == teacherId)
                .Select(cl => new
                {
                    Id = cl.ClassRoom.Id,
                    ClassName = cl.ClassRoom.Grade + "-" + cl.ClassRoom.Branch
                })
                .Distinct()
                .ToListAsync();

            return Ok(classes);
        }


        // ✅ 2) Seçilen sınıftaki öğrencileri getir
        [HttpGet("students/{classId}")]
        public async Task<IActionResult> GetStudentsInClass(int classId)
        {
            var teacherIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(teacherIdStr, out int teacherId))
                return Unauthorized();

            // Bu öğretmen o sınıfta ders veriyor mu?
            var hasClass = await _context.ClassLessons
                .AnyAsync(cl => cl.ClassRoomId == classId && cl.TeacherId == teacherId);

            if (!hasClass)
                return BadRequest("Bu sınıf için yetkiniz yok.");

            var students = await _context.ClassUsers
                .Where(cu => cu.ClassRoomId == classId && cu.User.Role == "Student")
                .Select(cu => new
                {
                    cu.User.Id,
                    cu.User.Name
                })
                .ToListAsync();

            return Ok(students);
        }

        // ✅ 3) Seçilen sınıftaki dersleri getir (Dropdown için)
        [HttpGet("lessons/{classId}")]
        public async Task<IActionResult> GetLessonsInClass(int classId)
        {
            var teacherIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(teacherIdStr, out int teacherId))
                return Unauthorized();

            var lessons = await _context.ClassLessons
                .Where(cl => cl.ClassRoomId == classId && cl.TeacherId == teacherId)
                .Select(cl => new
                {
                    cl.Lesson.Id,
                    cl.Lesson.Name
                })
                .ToListAsync();

            return Ok(lessons);
        }
        // ✅ Seçilen sınıftaki öğrencilerin notlarını getir (öğretmene göre filtrelenmiş)
        [HttpGet("class-notes/{classId}")]
        public async Task<IActionResult> GetNotesByClass(int classId)
        {
            var teacherIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(teacherIdStr, out int teacherId))
                return Unauthorized();

            var hasClass = await _context.ClassLessons
                .AnyAsync(cl => cl.ClassRoomId == classId && cl.TeacherId == teacherId);

            if (!hasClass)
                return BadRequest("Bu sınıf için yetkiniz yok.");

            var notes = await _context.Notes
                .Where(n => n.ClassLessonClassId == classId &&
                            _context.ClassLessons.Any(cl =>
                                cl.ClassRoomId == classId &&
                                cl.LessonId == n.ClassLessonLessonId &&
                                cl.TeacherId == teacherId))
                .Select(n => new
                {
                    StudentId = n.ClassUserUserId,
                    StudentName = _context.Users.Where(u => u.Id == n.ClassUserUserId).Select(u => u.Name).FirstOrDefault(),
                    LessonName = _context.Lessons.Where(l => l.Id == n.ClassLessonLessonId).Select(l => l.Name).FirstOrDefault(),
                    n.Score
                })
                .ToListAsync();

            return Ok(notes);
        }

        [HttpPost("add-note")]
        public async Task<IActionResult> AddNote([FromBody] AddNoteRequest request)
        {
            var teacherIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(teacherIdStr, out int teacherId))
                return Unauthorized();

            // Bu öğretmen bu sınıfta bu dersi veriyor mu?
            var hasPermission = await _context.ClassLessons
                .AnyAsync(cl => cl.TeacherId == teacherId
                             && cl.ClassRoomId == request.ClassId
                             && cl.LessonId == request.LessonId);

            if (!hasPermission)
                return BadRequest("Bu sınıf ve ders için yetkiniz yok.");

            // Öğrenci gerçekten o sınıfta mı?
            var classUser = await _context.ClassUsers
                .FirstOrDefaultAsync(cu => cu.UserId == request.StudentId && cu.ClassRoomId == request.ClassId);

            if (classUser == null)
                return BadRequest("Bu öğrenci bu sınıfta değil.");

            // Eğer aynı sınıf + ders + öğrenci için daha önce not eklenmişse → sil
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n =>
                n.ClassLessonClassId == request.ClassId &&
                n.ClassLessonLessonId == request.LessonId &&
                n.ClassUserClassId == request.ClassId &&
                n.ClassUserUserId == request.StudentId);

            if (existingNote != null)
            {
                _context.Notes.Remove(existingNote);
            }

            // Yeni notu ekle
            var note = new Note
            {
                Score = request.Score,
                ClassLessonClassId = request.ClassId,
                ClassLessonLessonId = request.LessonId,
                ClassUserClassId = request.ClassId,
                ClassUserUserId = request.StudentId
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Ok("Not başarıyla eklendi (önceki not varsa silindi).");
        }


        // ✅ Not ekleme isteği DTO
        public class AddNoteRequest
        {
            public int StudentId { get; set; }
            public int ClassId { get; set; }
            public int LessonId { get; set; }
            public float Score { get; set; }
        }
    }
}
