using EduTrack.Application.DTOs;
using EduTrack.Data;
using EduTrack.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Idare")]
public class ClassesController : ControllerBase
{
    private readonly EduTrackDbContext _context;

    public ClassesController(EduTrackDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClass([FromBody] ClassCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var school = await _context.Schools.FindAsync(dto.SchoolId);
        if (school == null)
            return NotFound("Okul bulunamadı.");

        // ⚠️ Duplicate Kontrolü: Aynı okulda, aynı Grade ve Branch var mı?
        var existingClass = await _context.Classes
            .FirstOrDefaultAsync(c => c.SchoolId == dto.SchoolId && c.Grade == dto.Grade && c.Branch == dto.Branch);

        if (existingClass != null)
            return Conflict($"Bu okulda zaten {dto.Grade}-{dto.Branch} sınıfı var.");

        var newClass = new ClassRoom
        {
            Grade = dto.Grade,
            Branch = dto.Branch,
            SchoolId = dto.SchoolId
        };

        _context.Classes.Add(newClass);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Sınıf başarıyla eklendi.", classId = newClass.Id });
    }


    [HttpGet]
    public async Task<IActionResult> GetClasses([FromQuery] int? schoolId)
    {
        var query = _context.Classes.AsQueryable();

        if (schoolId.HasValue)
        {
            query = query.Where(c => c.SchoolId == schoolId.Value);
        }

        var classes = await query
            .Select(c => new {
                c.Id,
                c.Grade,
                c.Branch,
                c.SchoolId
            })
            .ToListAsync();

        return Ok(classes);
    }


}
