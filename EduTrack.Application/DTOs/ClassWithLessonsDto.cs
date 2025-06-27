using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Application.DTOs
{
    public class ClassWithLessonsDto
    {
        public string ClassName { get; set; } = "";
        public List<string> Lessons { get; set; } = new();
    }
}
