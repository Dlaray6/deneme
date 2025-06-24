using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Data.Repositories.Interfaces;
using EduTrack.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Data.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly EduTrackDbContext _context;

        public NoteRepository(EduTrackDbContext context)
        {
            _context = context;
        }

        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
        }

        public async Task<IEnumerable<Note>> GetNotesByStudentIdAsync(int studentId)
        {
            return await _context.Notes
                .Where(n => n.ClassUserUserId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesByLessonIdAsync(int lessonId)
        {
            return await _context.Notes
                .Where(n => n.ClassLessonLessonId == lessonId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
