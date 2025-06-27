using EduTrack.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Student")]
    public class StudentsController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public StudentsController(EduTrackDbContext context)
        {
            _context = context;
        }

        [HttpGet("student-notes")]
        public async Task<IActionResult> GetMyNotes()
        {
            var studentIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(studentIdStr, out int studentId))
                return Unauthorized();

            var notes = await _context.Notes
                .Where(n => n.ClassUserUserId == studentId)
                .Select(n => new
                {
                    n.Score,
                    LessonName = _context.Lessons
                        .Where(l => l.Id == n.ClassLessonLessonId)
                        .Select(l => l.Name)
                        .FirstOrDefault(),
                    ClassName = _context.Classes
                        .Where(c => c.Id == n.ClassLessonClassId)
                        .Select(c => c.Grade + "-" + c.Branch)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(notes);
        }
    }
}
