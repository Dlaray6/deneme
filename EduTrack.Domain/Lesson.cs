using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class Lesson
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }

        public ICollection<ClassLesson> ClassLessons { get; set; }
    }

}
