using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EduTrack.Domain;


namespace EduTrack.Application.Interfaces
{
    public interface INoteService
    {
        Task AddNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task<List<Note>> GetNotesByStudentIdAsync(int studentId);
        Task<List<Note>> GetNotesByLessonIdAsync(int lessonId);  // ekstra
    }
}
