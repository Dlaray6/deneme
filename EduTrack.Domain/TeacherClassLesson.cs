using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


        namespace EduTrack.Domain
    {
        public class TeacherClassLesson
        {
            public int TeacherId { get; set; }
            public int ClassRoomId { get; set; }
            public int LessonId { get; set; }

            // İsteğe bağlı: navigation property'ler
            public User Teacher { get; set; }  // Eğer User öğretmense
            public ClassRoom Class { get; set; }
            public Lesson Lesson { get; set; }
        }
    }
