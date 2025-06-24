using EduTrack.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.API.Controllers
{
  

        [ApiController]
        [Route("api/[controller]")]
        [Authorize(Roles = "Idare")]
        public class TeacherClassLessonController : ControllerBase
        {
            private readonly ITeacherClassLessonService _service;

            public TeacherClassLessonController(ITeacherClassLessonService service)
            {
                _service = service;
            }

            [HttpPost]
            public async Task<IActionResult> AddTeacherClassLesson([FromBody] TeacherClassLessonDto dto)
            {
                await _service.AddTeacherToClassLessonAsync(dto.TeacherId, dto.ClassLessonId);
                return Ok("Öğretmen başarıyla sınıf dersine atandı.");
            }
        }

        public class TeacherClassLessonDto
        {
            public int TeacherId { get; set; }
            public int ClassLessonId { get; set; }
        }

    
}
