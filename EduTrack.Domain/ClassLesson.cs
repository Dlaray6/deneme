using System.Collections.Generic;

namespace EduTrack.Domain
{
    public class ClassLesson
    {
        public int ClassRoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }

        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        public int? TeacherId { get; set; }  // Öğretmen opsiyonel olabilir
        public User Teacher { get; set; }

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }

}
