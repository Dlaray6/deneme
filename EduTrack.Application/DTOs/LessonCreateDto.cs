using System.ComponentModel.DataAnnotations;

namespace EduTrack.Application.DTOs
{
    public class LessonCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int TeacherId { get; set; }
    }
}
