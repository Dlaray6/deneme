using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Domain
{
    public class Note
    {
        public int Id { get; set; }

        public int ClassUserClassId { get; set; }
        public int ClassUserUserId { get; set; }
        public ClassUser? ClassUser { get; set; }

        public int ClassLessonClassId { get; set; }
        public int ClassLessonLessonId { get; set; }
        public ClassLesson? ClassLesson { get; set; }

        public float Score { get; set; }
    }

}
