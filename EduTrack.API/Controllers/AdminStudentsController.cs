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
    public class AdminStudentsController : ControllerBase
    {
        private readonly EduTrackDbContext _context;

        public AdminStudentsController(EduTrackDbContext context)
        {
            _context = context;
        }

        [HttpGet("all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            // Token'dan SchoolId al
            var schoolIdClaim = User.FindFirst("SchoolId")?.Value;

            if (string.IsNullOrEmpty(schoolIdClaim) || !int.TryParse(schoolIdClaim, out int schoolId))
                return Unauthorized(new { message = "Geçersiz veya eksik SchoolId" });

            var students = await _context.Users
                .Where(u => u.Role == "Student" && u.SchoolId == schoolId) // okul bazlı filtreleme
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.SchoolNumber,
                    SchoolName = u.School != null ? u.School.Name : null,

                    Classes = _context.ClassUsers
                        .Where(cu => cu.UserId == u.Id)
                        .Select(cu => new {
                            cu.ClassRoom.Id,
                            ClassName = cu.ClassRoom.Grade + "-" + cu.ClassRoom.Branch
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(students);
        }


    }
}
