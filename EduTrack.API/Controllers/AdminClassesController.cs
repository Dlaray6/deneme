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
    [Authorize(Roles = "Idare")]
    public class AdminClassesController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public AdminClassesController(EduTrackDbContext context)
        {
            _context = context;
        }

        // ✅ 1. Okul id’si token’dan alınarak sadece ilgili okulun sınıfları listelenir
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            int schoolId = GetSchoolIdFromToken();
            var classes = await _context.Classes
                .Where(c => c.SchoolId == schoolId)
                .Select(c => new
                {
                    c.Id,
                    c.Grade,
                    c.Branch
                })
                .ToListAsync();

            return Ok(classes);
        }

        [HttpPost("assign-student-to-class")]
        public async Task<IActionResult> AssignStudentToClass([FromBody] AssignStudentRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.StudentId && u.Role == "Student");
            var classRoom = await _context.Classes
                .FirstOrDefaultAsync(c => c.Id == request.ClassId);

            if (user == null || classRoom == null)
                return BadRequest("Geçersiz öğrenci veya sınıf.");

            // Bu öğrenci zaten bir sınıfa kayıtlı mı?
            var existingClassUser = await _context.ClassUsers
                .FirstOrDefaultAsync(cu => cu.UserId == request.StudentId);

            if (existingClassUser != null)
            {
                return BadRequest("Bu öğrenci zaten başka bir sınıfa kayıtlı.");
            }

            var newClassUser = new ClassUser
            {
                UserId = request.StudentId,
                ClassRoomId = request.ClassId
            };

            _context.ClassUsers.Add(newClassUser);
            await _context.SaveChangesAsync();

            return Ok("Öğrenci başarıyla sınıfa eklendi.");
        }



        // ✅ 3. Öğrenciyi sınıftan çıkar
        [HttpDelete("remove-student-from-class")]
        public async Task<IActionResult> RemoveStudentFromClass([FromBody] AssignStudentRequest request)
        {
            var classUser = await _context.ClassUsers
                .FirstOrDefaultAsync(cu => cu.UserId == request.StudentId && cu.ClassRoomId == request.ClassId);

            if (classUser == null)
                return NotFound("Öğrenci bu sınıfta bulunamadı.");

            _context.ClassUsers.Remove(classUser);
            await _context.SaveChangesAsync();

            return Ok("Öğrenci sınıftan çıkarıldı.");
        }

        // ✅ Yardımcı method: Token içinden SchoolId çek
        private int GetSchoolIdFromToken()
        {
            var schoolIdClaim = User.Claims.FirstOrDefault(c => c.Type == "SchoolId");
            return schoolIdClaim != null ? int.Parse(schoolIdClaim.Value) : 0;
        }

        // ✅ DTO
        public class AssignStudentRequest
        {
            public int StudentId { get; set; }
            public int ClassId { get; set; }
        }
    }
}
