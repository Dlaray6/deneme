using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{

    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? TeacherId { get; set; }
        public User? Teacher { get; set; }

        public int? SchoolId { get; set; }
        public School? School { get; set; }

        public ICollection<ClassLesson> ClassLessons { get; set; } = new List<ClassLesson>();

    }

}
