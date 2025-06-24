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
        public int Id { get; set; } // PK
        public string Grade { get; set; } // Örneğin: "10"
        public string Branch { get; set; } // Örneğin: "A"

        public int SchoolId { get; set; }
        public School School { get; set; }


        public ICollection<ClassUser> ClassUsers { get; set; }
        public ICollection<ClassLesson> ClassLessons { get; set; }
    }

}
