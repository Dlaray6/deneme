using EduTrack.Application.DTOs;
using EduTrack.Data;
using EduTrack.Domain;
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
    public class AdminTeachersController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public AdminTeachersController(EduTrackDbContext context)
        {
            _context = context;
        }

        [HttpGet("all-teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var userId = int.Parse(User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
            var adminUser = await _context.Users.FindAsync(userId);

            if (adminUser == null || adminUser.Role != "Idare")
                return Unauthorized("Yetkisiz kullanıcı.");

            var teachers = await _context.Users
                .Where(u => u.Role == "Teacher" && u.SchoolId == adminUser.SchoolId)
                .Select(u => new TeacherDto
                {
                    Id = u.Id,
                    Name = u.Name
                })
                .ToListAsync();

            return Ok(teachers);
        }


        [HttpGet("teacher-classes-lessons")]
        public async Task<IActionResult> GetTeacherClassesAndLessons(int teacherId)
        {
            var teacher = await _context.Users
                .Where(u => u.Role == "Teacher" && u.Id == teacherId)
                .FirstOrDefaultAsync();

            if (teacher == null)
                return NotFound();

            var classLessons = await _context.ClassLessons
                .Include(cl => cl.Lesson)
                .Include(cl => cl.ClassRoom)
                .Where(cl => cl.TeacherId == teacherId && cl.Lesson != null && cl.ClassRoom != null)
                .ToListAsync();

            var classesWithLessons = classLessons
                .GroupBy(cl => cl.ClassRoom)
                .Select(g => new ClassWithLessonsDto
                {
                    ClassName = $"{g.Key.Grade} - {g.Key.Branch}",
                    Lessons = g.Select(cl => cl.Lesson.Name).Distinct().ToList()
                })
                .ToList();

            var result = new TeacherDetailsDto
            {
                TeacherId = teacher.Id,
                TeacherName = teacher.Name,
                ClassesWithLessons = classesWithLessons
            };

            return Ok(result);
        }

     


    }
}
