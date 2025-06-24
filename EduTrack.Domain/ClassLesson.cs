using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class ClassLesson
    {
        public int ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
       
     

        public ICollection<Note> Notes { get; set; }


        public ICollection<TeacherClassLesson> TeacherClassLessons { get; set; }
    }


}
