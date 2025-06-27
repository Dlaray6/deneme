using EduTrack.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Idare")]
    public class AdminNotesController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public AdminNotesController(EduTrackDbContext context)
        {
            _context = context;
        }

        // Bir sınıftaki öğrencileri ve o öğrencilerin notlarını getir
        [HttpGet("admin-class-notes/{classId}")]
        public async Task<IActionResult> GetStudentsWithNotes(int classId)
        {
            var students = await _context.Users
            .Where(u => u.Role == "Student"
                && _context.ClassUsers.Any(cu => cu.UserId == u.Id && cu.ClassRoomId == classId))
            .Select(u => new
            {
                u.Id,
                u.Name,
                Notes = _context.Notes
                    .Where(n => n.ClassUserUserId == u.Id && n.ClassUserClassId == classId)
                    .Select(n => new
                    {
                        n.Id,
                        n.Score,
                        LessonName = n.ClassLesson.Lesson.Name
                    })
                    .ToList()
            })
            .ToListAsync();

            return Ok(students);
        }

    }
}
