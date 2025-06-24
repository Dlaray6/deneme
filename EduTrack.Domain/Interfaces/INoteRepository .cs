using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EduTrack.Domain;


namespace EduTrack.Data.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task AddNoteAsync(Note note);
        Task<IEnumerable<Note>> GetNotesByStudentIdAsync(int studentId);
        Task<IEnumerable<Note>> GetNotesByLessonIdAsync(int lessonId);
        Task SaveChangesAsync();
    }
}
