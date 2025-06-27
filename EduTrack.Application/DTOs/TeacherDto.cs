using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Application.DTOs
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<ClassWithLessonsDto>? ClassesWithLessons { get; set; }
    }
}

