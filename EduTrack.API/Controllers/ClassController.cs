using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Giriş yapılmadan hiçbir şey erişilmesin
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var classRoom = await _classService.GetClassByIdAsync(id);
            if (classRoom == null)
                return NotFound("Sınıf bulunamadı.");
            return Ok(classRoom);
        }

        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetAllBySchool(int schoolId)
        {
            var classes = await _classService.GetClassesBySchoolAsync(schoolId);
            return Ok(classes);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddClass([FromBody] ClassRoom newClass)
        {
            await _classService.AddClassAsync(newClass);
            return Ok("Sınıf başarıyla eklendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignUserToClass([FromQuery] int classId, [FromQuery] int userId)
        {
            await _classService.AssignUserToClassAsync(classId, userId);
            return Ok("Kullanıcı sınıfa atandı.");
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("users/{classId}")]
        public async Task<IActionResult> GetUsersInClass(int classId)
        {
            var users = await _classService.GetUsersInClassAsync(classId);
            return Ok(users);
        }
    }
}
