using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class ClassRoom
    {
        public int Id { get; set; }

        public string Grade { get; set; } = string.Empty;  // Örn: "10"
        public string Branch { get; set; } = string.Empty; // Örn: "A"

        public int SchoolId { get; set; }
        public School? School { get; set; }

        public ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();
        public ICollection<ClassLesson> ClassLessons { get; set; } = new List<ClassLesson>();
    }

}
