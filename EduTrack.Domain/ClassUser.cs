using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class ClassUser
    {
        public int ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
     
        public ICollection<Note> Notes { get; set; }
    }

}
