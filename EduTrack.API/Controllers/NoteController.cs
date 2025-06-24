using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [Authorize(Roles = "Student")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyNotes()
        {
            // JWT’den user ID’yi al
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            int userId = int.Parse(userIdStr);

            // Notları getir
            var notes = await _noteService.GetNotesByStudentIdAsync(userId);
            return Ok(notes);
        }

        // Diğer endpoint'ler burada durabilir, mesela Admin veya Teacher için:
        [Authorize(Roles = "Teacher")]
        [HttpGet("by-lesson/{lessonId}")]
        public async Task<IActionResult> GetByLesson(int lessonId)
        {
            var notes = await _noteService.GetNotesByLessonIdAsync(lessonId);
            return Ok(notes);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("add")]
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            await _noteService.AddNoteAsync(note);
            return Ok("Not eklendi.");
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateNote([FromBody] Note note)
        {
            await _noteService.UpdateNoteAsync(note);
            return Ok("Not güncellendi.");
        }
    }

}

