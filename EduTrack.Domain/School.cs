using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string AccessCode { get; set; } = string.Empty;

        public ICollection<ClassRoom> Classes { get; set; } = new List<ClassRoom>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }


}
