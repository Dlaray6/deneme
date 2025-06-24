using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class School
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }

        public string AccessCode { get; set; } // Özel kod

        public ICollection<ClassRoom> Classes { get; set; }
        public ICollection<User> Users { get; set; }
    }

}
