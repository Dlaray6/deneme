using System.ComponentModel.DataAnnotations;

namespace EduTrack.Application.DTOs
{
    public class ClassLessonCreateDto
    {
        [Required]
        public int ClassRoomId { get; set; }

        [Required]
        public int LessonId { get; set; }


        [Required]
        public int TeacherId { get; set; }

    }
}
