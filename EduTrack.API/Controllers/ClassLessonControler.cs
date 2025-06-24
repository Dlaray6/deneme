using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EduTrack.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Idare")]
public class ClassLessonController : ControllerBase
{
    private readonly IClassLessonService _service;

    public ClassLessonController(IClassLessonService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddClassLesson([FromBody] ClassLessonDto dto)
    {
        await _service.AddClassLessonAsync(dto.ClassId, dto.LessonId);
        return Ok("Ders başarıyla sınıfa atandı.");
    }
}

public class ClassLessonDto
{
    public int ClassId { get; set; }
    public int LessonId { get; set; }
}
