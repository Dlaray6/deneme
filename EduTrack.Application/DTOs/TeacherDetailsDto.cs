using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Application.DTOs
{
    public class TeacherDetailsDto
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = "";
        public List<ClassWithLessonsDto> ClassesWithLessons { get; set; } = new();
    }
}
